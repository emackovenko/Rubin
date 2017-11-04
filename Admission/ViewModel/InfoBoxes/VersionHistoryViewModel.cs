using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using Model.Admission;

namespace Admission.ViewModel.InfoBoxes
{
	public class VersionHistoryViewModel
	{
		ObservableCollection<BuildVersion> _buildVersions = new ObservableCollection<BuildVersion>(
			(from build in Session.DataModel.BuildVersions
			 orderby build.Date
			 select build).ToList()
			);
		public ObservableCollection<BuildVersion> BuildVersions
		{
			get
			{
				return _buildVersions;
			}
		}
	}
}
