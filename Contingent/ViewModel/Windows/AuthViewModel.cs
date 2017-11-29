using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Astu;
using Contingent.Properties;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

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

        bool _isProcessed = false;

        public bool IsProcessed
        {
            get => _isProcessed;
            set
            {
                _isProcessed = value;
                RaisePropertyChanged("IsProcessed");
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
            //var sw = new System.Diagnostics.Stopwatch();
            //sw.Start();
            // если получилось
            if (Session.GetInstance().Auth(Username, Password))
            {
                //sw.Stop();
                //var ts = sw.Elapsed;
                //System.Windows.MessageBox.Show(string.Format("MIN: {0:00}\nSEC: {1:00}\nMILLSEC: {2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10));
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
            return !(string.IsNullOrWhiteSpace(Username) && string.IsNullOrWhiteSpace(_password));
        }
        
        
    }
}
