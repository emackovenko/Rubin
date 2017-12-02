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

namespace Contingent.ViewModel.Workspaces.Service.DataReinstatement
{
    public class DataReinstatementViewModel: ViewModelBase
    {
        string _consoleText;

        public string ConsoleText
        {
            get
            {
                return _consoleText;
            }
            set
            {
                _consoleText += value;
                _consoleText += "\n";
                RaisePropertyChanged("ConsoleText");
            }
        }

        public RelayCommand ReinstatementRunCommand {  get => new RelayCommand(ReinstatementRun); }

        void ReinstatementRun()
        {
            _consoleText = string.Empty;
            if (AuthInWorkOk())
            {
                ConsoleText = "Авторизация и загрузка данных из work_ok прошла успешно.";
            }

            var astudent = Astu.Astu.Students.First(s => s.FullName == "Маковенко Евгений Владимирович");

            ConsoleText = string.Format("\n\nСтудент {0}; ID: {1}.", astudent.FullName, astudent.Id);

            var wstudent = GetWorkOkStudent(astudent);
            if (wstudent == null)
            {
                ConsoleText = "\tСтудент не был найден в work_ok";
                return;
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

                var convertedOrder = worder.ToAstu();
                if (convertedOrder == null)
                {
                    ConsoleText = "\t\tПриказ не обработан.";
                    continue;
                }

                ConsoleText = aorder.Reinstate(convertedOrder);

            }
            
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
            return astudent.Orders.FirstOrDefault(o => o.Number == worder.Number && o.Date == worder.Date && o.OrderTypeId == worder.OrderType?.AstuId);
        }
    }
}
