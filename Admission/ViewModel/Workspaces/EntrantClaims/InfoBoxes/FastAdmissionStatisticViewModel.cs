using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace Admission.ViewModel.Workspaces.EntrantClaims.InfoBoxes
{
	public class FastAdmissionStatisticViewModel: ViewModelBase
	{
		#region Обрабатываемые сущности и коллекции

		ObservableCollection<Claim> _claims;

		public ObservableCollection<Claim> Claims
		{
			get
			{
				if (_claims == null)
				{
					_claims = new ObservableCollection<Claim>(Session.DataModel.Claims.OrderByDescending(c => c.RegistrationDate));
				}
				return _claims;
			}
			set
			{
				_claims = value;
				RaisePropertyChanged("Claims");
			}
		}

		#endregion
	}
}
