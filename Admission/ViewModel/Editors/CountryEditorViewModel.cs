using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Model.Admission;

namespace Admission.ViewModel.Editors
{
	public class CountryEditorViewModel: ViewModelBase
	{
		public CountryEditorViewModel(Country country)
		{
			_country = country;
		}

		Country _country;

		public Country Country
		{
			get
			{
				return _country;
			}
			set
			{
				_country = value;
				RaisePropertyChanged("Country");
			}
		}

	}
}
