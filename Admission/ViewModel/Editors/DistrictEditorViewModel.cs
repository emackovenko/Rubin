using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Admission;

namespace Admission.ViewModel.Editors
{
	public class DistrictEditorViewModel: ViewModelBase
	{

		#region Constructors

		public DistrictEditorViewModel(District district)
		{
			_district = district;
			_region = (from region in Session.DataModel.Regions
					   where region.Id == district.RegionId
					   select region).FirstOrDefault();
			_country = (from country in Session.DataModel.Countries
						where country.Id == _region.CountryId
						select country).FirstOrDefault();
		}

		#endregion

		#region ParentEntities

		District _district;

		public District District
		{
			get
			{
				return _district;
			}
			set
			{
				_district = value;
				RaisePropertyChanged("District");
			}
		}

		#endregion

		#region FilterEntities

		Country _country;
		Region _region;

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

		public Region Region
		{
			get
			{
				return _region;
			}

			set
			{
				_region = value;
				if (value != null)
				{
					_district.RegionId = value.Id;
				}
				RaisePropertyChanged("Region");
			}
		}

		#endregion

		#region FilteredCollections

		ObservableCollection<Country> _countries;
		ObservableCollection<Region> _regions;


		public ObservableCollection<Country> Countries
		{
			get
			{
				_countries = new ObservableCollection<Country>(Session.DataModel.Countries);
				return _countries;
			}

			set
			{
				_countries = value;
				RaisePropertyChanged("Countries");
			}
		}

		public ObservableCollection<Region> Regions
		{
			get
			{
				if (_regions == null)
				{
					if (Country != null)
					{
						_regions = new ObservableCollection<Region>(
							(from region in Session.DataModel.Regions
							 where region.CountryId == Country.Id
							 orderby region.Name
							 select region));
					}
					else
					{
						_regions = new ObservableCollection<Region>();
					}
				}
				return _regions;
			}

			set
			{
				_regions = value;
				RaisePropertyChanged("Regions");
			}
		}

		#endregion

		#region FilterCommands

		#region Commands

		public RelayCommand UpdateRegionsCommand
		{
			get
			{
				return new RelayCommand(UpdateRegions);
			}
		}

		#endregion

		#region Methods

		void UpdateRegions()
		{
			Regions = new ObservableCollection<Region>(
				(from region in Session.DataModel.Regions
				 where region.CountryId == Country.Id
				 orderby region.Name
				 select region));	 
		}

		#endregion

		#endregion

	}
}
