using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Admission.ViewModel.Documents;


namespace Admission.ViewModel.Workspaces.ReportGeneration
{
    public class CampaignMonitoringViewModel: ViewModelBase
    {
        public CampaignMonitoringViewModel()
        {
            CampaignMonitoring = new CampaignMonitoring();
        }

        public CampaignMonitoring CampaignMonitoring { get; set; }
    }
}
