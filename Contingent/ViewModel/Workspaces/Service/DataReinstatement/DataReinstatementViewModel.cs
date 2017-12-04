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
            var thread = new Thread(ReinstatementRun);
            thread.Start();
        }

        void ReinstatementRun()
        {
            _consoleText.Clear();
            if (AuthInWorkOk())
            {
                ConsoleText = "Авторизация и загрузка данных из work_ok прошла успешно.";
            }
            var studentCollection = Astu.Astu.Students.Where(s => s.StatusId != "0006" && s.StatusId != "0009");//.Where(s => s.GroupId == "6200000519");

            int studentCount = 0;
            int ordersCount = 0;

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
                    ConsoleText = string.Format("\tПриказ № {0} от {1} г. - {2}:", worder.Number, worder.Date.Format(), worder.OrderType?.Name);

                    var aorder = FindOrderHistory(astudent, worder);
                    if (aorder == null)
                    {
                        ConsoleText = "\t\tПриказ не был найден у студента в Astu.";
                        continue;
                    }

                    ConsoleText = string.Format("\t\tПриказ найден в Astu, ID:{0}", aorder.Id);
                    try
                    {
                        var convertedOrder = worder.ToAstu();
                        if (convertedOrder == null)
                        {
                            ConsoleText = "\t\tОбработка этого типа приказов еще не реализована.";
                            continue;
                        }

                        ConsoleText = aorder.Reinstate(convertedOrder);
                        ordersCount++;
                    }
                    catch (Exception e)
                    {
                        ConsoleText = string.Format("\t\t\tПри обработке приказа произошла ошибка: {0}", e.Message);
                    }
                }
            }

            ConsoleText = string.Format("\n\n\nОбработано студентов: {0}.", studentCount);
            ConsoleText = string.Format("Обработано приказов: {0}.", ordersCount);
        }

        bool AuthInWorkOk()
        {
            var csb = new MySqlConnectionStringBuilder
            {
                Server = "192.168.0.48",
                Port = 3306,
                Database = "work_ok",
                UserID = "emackovenko",
                Password = "trustno1"
            };

            var connection = new MySqlConnection(csb.ToString());
            return WorkOk.Context.Auth(connection);
        }

        WorkOk.Student GetWorkOkStudent(Astu.Student astuStudent)
        {
            // Фильтруем по ФИО
            var filteredCollection = WorkOk.Context.Students.Where(s => s.FullName == astuStudent.FullName);
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
            return astudent.Orders.FirstOrDefault(o => o.Number == worder.Number && o.Date?.Year == worder.Date?.Year && o.OrderTypeId == worder.OrderType?.AstuId);
        }
    }
}
