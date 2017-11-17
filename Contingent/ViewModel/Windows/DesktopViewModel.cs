using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Deployment.Application;
using System.Threading;
using System.Windows;

namespace Contingent.ViewModel.Windows
{
    public class DesktopViewModel: ViewModelBase
    {
        public string BuildVersion
        {
            get
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    return ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                }
                else
                {
                    return "разработчика";
                }
            }
        }

        private bool _isExistsNewVersion = false;

        public bool IsExistsNewVersion
        {
            get
            {
                return _isExistsNewVersion;
            }
            set
            {
                _isExistsNewVersion = value;
                RaisePropertyChanged("IsExistsNewVersion");
            }
        }

        public RelayCommand CheckUpdatesCommand
        {
            get
            {
                return new RelayCommand(CheckUpdates, CheckUpdatesCanExecute);
            }
        }

        void CheckUpdates()
        {
            if(!IsExistsNewVersion)
            {
                if (ApplicationDeployment.CurrentDeployment.CheckForUpdate())
                {
                    ApplicationDeployment.CurrentDeployment.UpdateCompleted += CurrentDeployment_UpdateCompleted;
                    ApplicationDeployment.CurrentDeployment.UpdateAsync();
                }
            }
        }

        private void CurrentDeployment_UpdateCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            IsExistsNewVersion = true;
        }

        bool CheckUpdatesCanExecute()
        {
            return ApplicationDeployment.IsNetworkDeployed;
        }
    }
}
