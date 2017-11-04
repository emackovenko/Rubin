using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Contingent.Properties;

namespace Contingent.ViewModel.Windows
{
    public class ConnectionSettingsViewModel: ViewModelBase
    {

        public string Hostname
        {
            get
            {
                return Settings.Default.DbHost;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Settings.Default.DbHost = value;
                }
                RaisePropertyChanged("Hostname");
            }
        }

        public int Port
        {
            get
            {
                return Settings.Default.DbPort;
            }
            set
            {
                if (value > 0)
                {
                    Settings.Default.DbPort = value;
                }
                RaisePropertyChanged("Port");
            }
        }

        public string ServiceName
        {
            get
            {
                return Settings.Default.DbServiceName;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Settings.Default.DbServiceName = value;
                }
                RaisePropertyChanged("ServiceName");
            }
        }

        public RelayCommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        public RelayCommand CloseCommand
        {
            get
            {
                return new RelayCommand(Close);
            }
        }

        void Save()
        {
            Settings.Default.Save();
            Close();
        }

        void Close()
        {
            Messenger.Default.Send("Close_ConnectionSettings");
        }

    }
}
