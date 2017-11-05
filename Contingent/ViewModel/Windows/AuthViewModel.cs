using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Astu;
using Contingent.Properties;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Devart.Data.Oracle;

namespace Contingent.ViewModel.Windows
{
    public class AuthViewModel: ViewModelBase
    {
        string _password;

        public string Username
        {
            get
            {
                return Settings.Default.DbUsername;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Settings.Default.DbUsername = value;
                }
                RaisePropertyChanged("Username");
            }
        }

        public string Password
        {
            get
            {
                return _password ?? string.Empty;
            }
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }

        public RelayCommand TryAuthCommand
        {
            get
            {
                return new RelayCommand(TryAuth, TryAuthCanExecute);
            }

        }
        public RelayCommand ExitCommand
        {
            get
            {
                return new RelayCommand(Exit);
            }
        }

        void TryAuth()
        {
            // загружаем параметры из настроек
            string dbHost = Settings.Default.DbHost;
            int dbPort = Settings.Default.DbPort;
            string dbServiceName = Settings.Default.DbServiceName;

            var connectionString = new OracleConnectionStringBuilder
            {
                Direct = true,
                Server = dbHost,
                Port = dbPort,
                ServiceName = dbServiceName,
                Sid = dbServiceName,
                UserId = Username,
                Password = _password
            };

            connectionString.UserId = "mackovenko_e";
            connectionString.Password = "orhuvei0";

            // создаем коннект и пытаемся инициализировать модель
            var connection = new OracleConnection(connectionString.ToString());
            connection.Open();

            // если получилось
            if (Astu.Auth(connection))
            {
                // сохраняем настройки
                Settings.Default.Save();
                // шлем сообщение об успешной авторизации
                Messenger.Default.Send("AuthGOOD");
            }
            // если не получилось
            else
            {
                // шлем сообщение о хреновой авторизации
                Messenger.Default.Send("AuthFUCK");
            }
        }

        void Exit()
        {
            Messenger.Default.Send("AuthEXIT");
        }

        bool TryAuthCanExecute()
        {
            return true;// !(string.IsNullOrWhiteSpace(Username) && string.IsNullOrWhiteSpace(_password));
        }
    }
}
