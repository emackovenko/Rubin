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
	public class LocalityEditorViewModel : ViewModelBase
	{

		#region Constructors

		public LocalityEditorViewModel(Locality locality)
		{
			_locality = locality;
			_district = (from district in Session.DataModel.Districts
						 where district.Id == _locality.DistrictId
						 select district).FirstOrDefault();
			_region = (from region in Session.DataModel.Regions
					   where region.Id == _district.RegionId
					   select region).FirstOrDefault();	 
			_country = (from country in Session.DataModel.Countries
						where country.Id == _region.CountryId
						select country).FirstOrDefault();
		}

		#endregion

		#region ParentEntities

		Locality _locality;

		public Locality Locality
		{
			get
			{
				return _locality;
			}
			set
			{
				_locality = value;
				RaisePropertyChanged("Locality");
			}
		}

		#endregion

		#region FilterEntities

		Country _country;
		Region _region;
		District _district;

		public Country Country
		{
			get
			{
				return _country;
			}

			set
			{
				_country = value;
				RefreshRegions();
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
				RefreshDistricts();
				RaisePropertyChanged("Region");
			}
		}

		public District District
		{
			get
			{
				return _district;
			}
			set
			{
				_district = value;
				if (value != null)
				{
					_locality.DistrictId = value.Id;
				}
				RaisePropertyChanged("District");
			}
		}

		#endregion

		#region FilteredCollections

		ObservableCollection<Country> _countries;
		ObservableCollection<Region> _regions;
		ObservableCollection<District> _districts;


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

		public ObservableCollection<District> Districts
		{
			get
			{
				if (_districts == null)
				{
					if (Region != null)
					{
						_districts = new ObservableCollection<District>(
							from district in Session.DataModel.Districts
							where district.RegionId == Region.Id
							orderby district.Name
							select district);
					}
					else
					{
						_districts = new ObservableCollection<District>();
					}
				}
				return _districts;
			}
			set
			{
				_districts = value;
				RaisePropertyChanged("Districts");
			}
		}

		#endregion

		#region FilterCommands

		#region Commands

		public RelayCommand RefreshRegionsCommand
		{
			get
			{
				return new RelayCommand(RefreshRegions);
			}
		}
		
		public RelayCommand RefreshDistrictsCommand
		{
			get
			{
				return new RelayCommand(RefreshDistricts);
			}
		}

		#endregion

		#region Methods

		void RefreshRegions()
		{
			Regions = new ObservableCollection<Region>(
				(from region in Session.DataModel.Regions
				 where region.CountryId == Country.Id
				 orderby region.Name
				 select region));
			if (!Regions.Contains(Region))
			{
				Region = null;
			}
		}

		void RefreshDistricts()
		{
			Districts = new ObservableCollection<District>(
				(from district in Session.DataModel.Districts
				 where district.RegionId == Region.Id
				 orderby district.Name
				 select district));
			if (!Districts.Contains(District))
			{
				District = null;
			}
		}

		#endregion

		#endregion

	}
}
