using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Model.Admission;		

namespace Admission.ViewModel.Editors
{
	public class RegionEditorViewModel: ViewModelBase
	{
		public RegionEditorViewModel(Region region)
		{
			_region = region; 
		}

		Region _region;

		public Region Region
		{
			get
			{
				return _region;
			}
			set
			{
				_region = value;
				RaisePropertyChanged("Region");
			}
		}


		Country _country;

		public Country Country
		{
			get
			{
				_country = (from country in Session.DataModel.Countries
							where country.Id == _region.CountryId
							select country).FirstOrDefault();
				return _country;
			}
			set
			{
				_country = value;
				_region.CountryId = _country.Id;
				RaisePropertyChanged("Country");
			}
		}

		ObservableCollection<Country> _countries;
		public ObservableCollection<Country> Countries
		{
			get
			{
				_countries = new ObservableCollection<Country>(Session.DataModel.Countries.OrderBy(c => c.Name));
				return _countries;
			}  
			set
			{
				_countries = value;
				RaisePropertyChanged("Countries");
			}
		}

	}
}
