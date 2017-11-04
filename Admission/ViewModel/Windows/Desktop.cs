using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Model.Admission;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Deployment.Application;

namespace Admission.ViewModel.Windows
{
	public class Desktop: ViewModelBase
	{	 
		public User CurrentUser
		{
			get
			{
				return Session.CurrentUser;
			}
		}

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

		public RelayCommand ShowVersionHistoryCommand
		{
			get
			{
				return new RelayCommand(ShowVersionHistory);
			}
		}

		void ShowVersionHistory()
		{
			Admission.DialogService.DialogLayer.ShowInfoBox(InfoContent.VersionHistory,
				new Admission.ViewModel.InfoBoxes.VersionHistoryViewModel());
		}
	}
}
