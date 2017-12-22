using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Astu = Model.Astu;
using WorkOk = Model.WorkOk;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MySql.Data.MySqlClient;
using CommonMethods.TypeExtensions.exDateTime;
using System.Threading;
using System.Data;

namespace Contingent.ViewModel.Workspaces.Service.DataReinstatement
{
    public class DataReinstatementViewModel: ViewModelBase
    {
        StringBuilder _consoleText = new StringBuilder();

        public string ConsoleText
        {
            get
            {
                return _consoleText.ToString();
            }
            set
            {
                _consoleText.AppendLine(value);
                RaisePropertyChanged("ConsoleText");
            }
        }

        public RelayCommand ReinstatementRunCommand {  get => new RelayCommand(DoIt); }
        
        void DoIt()
        {
            var thread = new Thread(CourseTransfer);
            thread.Start();            
        }

        void CourseTransfer()
        {
            _consoleText.Clear();
            var sbQuery = new StringBuilder();

            var selectedStatuses = new string[] { "0001", "0002", "0004", "0007", "0008" };

            var studentCollection = Astu.Astu.Students.Where(s => selectedStatuses.Contains(s.StatusId) && s.Course != 1);

            foreach (var astudent in studentCollection)
            {
                if (!astudent.Course.HasValue)
                {
                    var classmateStudent = astudent.Group.Students.FirstOrDefault(s => s.Course.HasValue && s.StatusId == "0001");
                    if (classmateStudent == null)
                    {
                        ConsoleText = string.Format("{0}: не указан курс вообще. Пропускаем.", astudent.FullName);
                        continue;
                    }
                    astudent.Course = classmateStudent.Course;
                    sbQuery.AppendLine(astudent.GetSaveQuery());
                    ConsoleText = string.Format("{0}: установил курс {1}.", astudent.FullName, astudent.Course);
                }

                if (astudent.Course == 1)
                {
                    continue;
                }

                ConsoleText = string.Format("{0}:", astudent.FullName);
                for (int i = 1; i < astudent.Course; i++)
                {
                    // Если приказ о переводе с i-того курса на i+1 курс если, то переходим к следующему
                    if (astudent.NextCourseTransferOrders.Count(o => o.OldCourse == i && o.NewCourse == i + 1) > 0)
                    {
                        ConsoleText = string.Format("\tПеревод с {0} на {1} выполнен. Идем дальше.", i, i + 1);
                        continue;
                    }

                    var newOrder = CreateNextCourseTransferOrder(astudent, i, i + 1);
                    sbQuery.AppendLine(newOrder.GetSaveQuery());
                    ConsoleText = string.Format("\tДобавлен перевод с {0} на {1} от {2}.", i, i + 1, newOrder.Date.Format());
                }
            }

            ConsoleText = "\n\n\n\n";
            ConsoleText = sbQuery.ToString();
        }

        void ReinstatementRun()
        {
            _consoleText.Clear();
            var sbQuery = new StringBuilder();

            if (AuthInWorkOk())
            {
                ConsoleText = "Авторизация и загрузка данных из work_ok прошла успешно.";
            }

            var selectedWorkOkFaculties = new int?[] { 57, 65, 98 };

            var studentCollection = Astu.Astu.Students.Where(s => s.StatusId != "0006" && s.StatusId != "0009");//.Where(s => selectedGroups.Contains(s.GroupId));

            int studentCount = 0;
            int ordersCount = 0;
            int deleted = 0;
            int statusChangings = 0;

            foreach (var astudent in studentCollection)
            {
                ConsoleText = string.Format("\n\nСтудент {0}; ID: {1}.", astudent.FullName, astudent.Id);
                studentCount++;
                var wstudent = GetWorkOkStudent(astudent);
                if (wstudent == null)
                {
                    ConsoleText = "\tСтудент не был найден в work_ok";
                    continue;
                }

                ConsoleText = string.Format("\tСтудент найден в work_ok, ID: {0}.", wstudent.Id);
                
                ConsoleText = "Сверка приказов:";
                foreach (var worder in wstudent.Orders)
                {
                    if (!selectedWorkOkFaculties.Contains(worder.FacultyId))
                    {
                        continue;
                    }

                    ConsoleText = string.Format("\tПриказ № {0} от {1} г. - {2}:", worder.Number, worder.Date.Format(), worder.OrderType?.Name);

                    var aorder = FindOrderHistory(astudent, worder);
                    if (aorder == null)
                    {
                        ConsoleText = "\t\tПриказ не был найден у студента в Astu. Добавляю новый.";
                        try
                        {
                            var newOrder = worder.ToAstu();

                            if (newOrder == null)
                            {
                                ConsoleText = "\t\tОбработка этого типа приказов еще не реализована.\n";
                                continue;
                            }

                            newOrder.StudentId = astudent.Id;
                            ConsoleText = string.Format("\t\t\tСоздан новый приказ: № {0} от {1} г. - {2} (Комментарий: {3});\n",
                                newOrder.Number, newOrder.Date.Format(), newOrder.OrderType.Name, newOrder.Comment);
                            sbQuery.AppendLine(newOrder.GetSaveQuery());

                        }
                        catch (Exception e)
                        {
                            ConsoleText = string.Format("\t\t\tПри обработке приказа произошла ошибка: {0}\n.", e.Message);
                        }
                    }
                    else
                    {
                        ConsoleText = string.Format("\t\tПриказ найден в Astu, ID:{0}", aorder.Id);
                        try
                        {
                            var convertedOrder = worder.ToAstu();
                            if (convertedOrder == null)
                            {
                                ConsoleText = "\t\tОбработка этого типа приказов еще не реализована.\n";
                                continue;
                            }

                            ConsoleText = aorder.Reinstate(convertedOrder);
                            sbQuery.AppendLine(aorder.GetSaveQuery());
                            ordersCount++;
                        }
                        catch (Exception e)
                        {
                            ConsoleText = string.Format("\t\t\tПри обработке приказа произошла ошибка: {0}\n.", e.Message);
                        }
                    }
                }

                // ОБРАТНАЯ ПРОВЕРКА
                ConsoleText = "\nОбратная проверка\n";
                foreach (var order in astudent.Orders)
                {
                    if (!OrderIsExistsInWorkOk(wstudent, order))
                    {
                        order.Delete();
                        ConsoleText = string.Format("\tПриказ № {0} от {1} г. - {2} ({3}) не был найден в WorkOk. Удаление.",
                            order.Number, order.Date.Format(), order.OrderType.Name, order.Comment);
                        sbQuery.AppendLine(order.GetSaveQuery());
                        deleted++;
                    }
                }

                // Проверка статуса студента
                if (astudent.StatusId != wstudent.Status.AstuId)
                {
                    ConsoleText = string.Format("Статус студента {0} изменен с {1} на {2}.",
                        astudent.FullName, astudent.Status.Name, Astu.Astu.StudentStatuses.FirstOrDefault(s => s.Id == wstudent.Status.AstuId).Name);
                    astudent.StatusId = wstudent.Status.AstuId;
                    sbQuery.AppendLine(astudent.GetSaveQuery());
                    statusChangings++;
                }

                // Проверка документов
                if (astudent.Documents.Count() == 0)
                {
                    var doc = CreateIdentityDocument(astudent);
                    if (doc != null)
                    {
                        sbQuery.AppendLine(doc.GetSaveQuery());
                        ConsoleText = string.Format("Студенту {0} добавлен паспорт.", astudent.FullName);
                    }
                }

            }

            ConsoleText = string.Format("\n\n\nОбработано студентов: {0}.", studentCount);
            ConsoleText = string.Format("Обработано приказов: {0}.", ordersCount);
            ConsoleText = string.Format("Удалено приказов: {0}.", deleted);
            ConsoleText = string.Format("Изменено статусов у студентов: {0}.", statusChangings);
            ConsoleText = "\n\n\nЗапрос для сохранения:\n\n";
            ConsoleText = sbQuery.ToString();

            // сохраняем
            using (var transaction = Astu.Astu.DbConnection.BeginTransaction())
            {
                using (var command = Astu.Astu.DbConnection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandText = sbQuery.ToString();
                    try
                    {
                        command.ExecuteNonQuery();
                        ConsoleText = "\n\n\n\nИзменения успешно сохранены.";
                    }
                    catch (Exception e)
                    {
                        ConsoleText = string.Format("\n\n\nНе удалось сохранить изменения. Текст ошибки:\n\n{0}", e.Message);
                    }
                    finally
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        void StudentWork()
        {
            _consoleText.Clear();
            var sbQuery = new StringBuilder();

            if (AuthInWorkOk())
            {
                ConsoleText = "Авторизация и загрузка данных из work_ok прошла успешно.";
            }

            var selectedStatuses = new string[] { "0001", "0002", "0004", "0007", "0008" };

            var studentCollection = Astu.Astu.Students.Where(s => selectedStatuses.Contains(s.StatusId));

            foreach (var astudent in studentCollection)
            {
                var wstudent = GetWorkOkStudent(astudent);
                if (wstudent == null)
                {
                    ConsoleText = string.Format("Студент {0} не найден в WorkOk.", astudent.FullName);
                    continue;
                }

                // Проверка группы
                if (!string.IsNullOrWhiteSpace(wstudent.Group.AstuId))
                {
                    if (astudent.Group?.Id != wstudent.Group.AstuId)
                    {
                        ConsoleText = string.Format("У студента {0} была изменена группа с {1} на {2}.",
                            astudent.FullName, astudent.Group?.Name, wstudent.Group.Name);
                        astudent.GroupId = wstudent.Group.AstuId;
                    }
                }

                // Проверка направления подготовки
                if (!string.IsNullOrWhiteSpace(wstudent.Direction.AstuId))
                {
                    if (astudent.Direction?.Id != wstudent.Direction.AstuId && astudent.Direction?.Id != "0535")
                    {
                        ConsoleText = string.Format("У студента {0} было изменено направление с {1} на {2} ({3}).",
                            astudent.FullName, astudent.Direction?.Name, wstudent.Direction.Name, wstudent.Direction.AstuId);
                        astudent.DirectionId = wstudent.Direction.AstuId;
                    }
                }

                // Проверка даты рождения
                if (astudent.BirthDate == null)
                {
                    ConsoleText = string.Format("Студенту {0} была установлена дата рождения: {1}.", astudent.FullName, wstudent.BirthDate.Format());
                    astudent.BirthDate = wstudent.BirthDate;
                }

                // Если проведены изменения, то кэшируем запрос
                if (astudent.EntityState == Astu.EntityState.Changed)
                {
                    sbQuery.AppendLine(astudent.GetSaveQuery());
                }
            }
            ConsoleText = "\n\n\n\n\n";
            ConsoleText = sbQuery.ToString();
        }

        bool AuthInWorkOk()
        {
            var csb = new MySqlConnectionStringBuilder
            {
                Server = "192.168.0.48",
                Port = 3306,
                Database = "work_ok",
                UserID = "emackovenko",
                Password = "trustno1",
                CharacterSet = "cp1251",
                AllowZeroDateTime = false,
                ConvertZeroDateTime = true
            };

            var connection = new MySqlConnection(csb.ToString());
            return WorkOk.Context.Auth(connection);
        }


        WorkOk.Student GetWorkOkStudent(Astu.Student astuStudent)
        {
            var filteredCollection = WorkOk.Context.Students.Where(s => s.StatusId != null && s.StatusId != 8);
            // Фильтруем по ФИО
            filteredCollection = filteredCollection.Where(s => s.FullName == astuStudent.FullName);
            if (filteredCollection.Count() == 1)
            {
                return filteredCollection.First();
            }

            // фильтр по году приёма
            filteredCollection = filteredCollection.Where(s => s.AdmissionYear == astuStudent.AdmissionYear);
            if (filteredCollection.Count() > 0)
            {
                return filteredCollection.First();
            }
            

            //// фильтруем по дате рождения
            //filteredCollection = filteredCollection.Where(s => s.BirthDate == astuStudent.BirthDate);
            //if (filteredCollection.Count() > 0)
            //{
            //    return filteredCollection.First();
            //}
            return null;
        }

        Astu.Orders.History.StudentOrderBase FindOrderHistory(Astu.Student astudent, WorkOk.Order worder)
        {
            return astudent.Orders.FirstOrDefault(o => o.Number == worder.Number && 
                o.Date?.Year == worder.Date?.Year && o.OrderTypeId == worder.OrderType?.AstuId);
        }

        bool OrderIsExistsInWorkOk(WorkOk.Student wstudent, Astu.Orders.History.StudentOrderBase aorder)
        {
            return wstudent.Orders.Where(o => o.Number == aorder.Number && 
                o.Date?.Year == aorder.Date?.Year && o.OrderType?.AstuId == aorder.OrderTypeId).Count() > 0;
        }

        Astu.IdentityDocument CreateIdentityDocument(Astu.Student astudent)
        { 
            var wstudent = GetWorkOkStudent(astudent);
            if (wstudent == null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(wstudent.IdentityDocumentNumber))
            {
                return null;
            }

            var doc = new Astu.IdentityDocument();
            doc.StudentId = astudent.Id;
            doc.FirstName = astudent.FirstName;
            doc.LastName = astudent.LastName;
            doc.Patronimyc = astudent.Patronimyc;
            doc.Series = wstudent.IdentityDocumentSeries;
            doc.Number = wstudent.IdentityDocumentNumber;
            doc.Organization = wstudent.IdentityDocumentOrganization;
            doc.Date = wstudent.IdentityDocumentDate;
            doc.CitizenshipId = wstudent.Citizenship?.AstuId;
            doc.IdentityDocumentTypeId = wstudent.IdentityDocumentType?.FisCode;
            return doc;
        }

        Astu.Orders.History.NextCourseTransferOrder CreateNextCourseTransferOrder(Astu.Student astudent, int oldCourse, int newCourse)
        {
            int orderYear = DateTime.Now.Year - (astudent.Course.Value - newCourse);

            var order = new Astu.Orders.History.NextCourseTransferOrder();
            order.Number = "ЖП-999";
            order.StudentId = astudent.Id;
            order.Date = DateTime.Parse(string.Format("{0}-10-15", orderYear));
            order.StartDate = DateTime.Parse(string.Format("{0}-09-01", orderYear));
            order.FacultyId = astudent.FacultyId;
            order.OldCourse = oldCourse;
            order.NewCourse = newCourse;
            order.Comment = string.Format("Переведен на {0} курс", newCourse);
            return order;
        }
    }
}
