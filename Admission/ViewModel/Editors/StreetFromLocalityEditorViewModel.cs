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
	public class StreetFromLocalityEditorViewModel : ViewModelBase
	{

		#region Constructors

		public StreetFromLocalityEditorViewModel(Street street)
		{
			Street = street;
			Street.ParentLevel = 4;

			Locality = (from local in Session.DataModel.Localities
						 where local.Id == _street.ParentID
						 select local).FirstOrDefault();

			District = (from district in Session.DataModel.Districts
						 where district.Id == _locality.DistrictId
						 select district).FirstOrDefault();

			Region = (from region in Session.DataModel.Regions
					   where region.Id == _district.RegionId
					   select region).FirstOrDefault();

			Country = (from country in Session.DataModel.Countries
						where country.Id == _region.CountryId
						select country).FirstOrDefault();
		}

		#endregion

		#region ParentEntities

		Street _street;

		public Street Street
		{
			get
			{
				return _street;
			}
			set
			{
				_street = value;
				RaisePropertyChanged("Street");
			}
		}

		#endregion

		#region FilterEntities

		Country _country;
		Region _region;
		District _district;
		Locality _locality;

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

				RefreshLocalities(); 
													
				RaisePropertyChanged("District");
			}
		}

		public Locality Locality
		{
			get
			{
				return _locality;
			}
			set
			{
				_locality = value;

				if (value != null)
				{
					_street.ParentID = value.Id;
				}

				RaisePropertyChanged("Locality");
			}
		}

		#endregion

		#region FilteredCollections

		ObservableCollection<Country> _countries;
		ObservableCollection<Region> _regions;
		ObservableCollection<District> _districts;
		ObservableCollection<Locality> _localities;

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
					_regions = new ObservableCollection<Region>();
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
					_districts = new ObservableCollection<District>();
				}
				return _districts;
			}

			set
			{
				_districts = value;
				RaisePropertyChanged("Districts");
			}
		}

		public ObservableCollection<Locality> Localities
		{
			get
			{
				if (_localities == null)
				{
					_localities = new ObservableCollection<Locality>();
				}
				return _localities;
			}

			set
			{
				_localities = value;
				RaisePropertyChanged("Localities");
			}
		}

		#endregion

		#region FilterMethods

		void RefreshRegions()
		{
			if (Country != null)
			{
				Regions = new ObservableCollection<Region>(
				(from region in Session.DataModel.Regions
				 where region.CountryId == Country.Id
				 orderby region.Name
				 select region));
			}
			else
			{
				Regions = new ObservableCollection<Region>();
			}

			if (!Regions.Contains(Region))
			{
				Region = null;
			}
		}	  

		void RefreshDistricts()
		{
			if (Region != null)
			{
				Districts = new ObservableCollection<District>(
					(from district in Session.DataModel.Districts.Local
					 where district.RegionId == Region.Id
					 orderby district.Name
					 select district));
			}
			else
			{
				Districts = new ObservableCollection<District>();
			}

			if (!Districts.Contains(District))
			{
				District = null;
			}
		}

		void RefreshLocalities()
		{
			if (District != null)
			{
				Localities = new ObservableCollection<Locality>(
					(from local in Session.DataModel.Localities
					 where local.DistrictId == District.Id
					 orderby local.Name
					 select local));
			}
			else
			{
				Localities = new ObservableCollection<Locality>();
			}

			if (!Localities.Contains(Locality))
			{
				Locality = null;
			}
		}

		#endregion

	}
}
