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
	public class StreetFromTownEditorViewModel : ViewModelBase
	{

		#region Constructors

		public StreetFromTownEditorViewModel(Street street)
		{
			_street = street;	  
			_street.ParentLevel = 3;

			_town = (from town in Session.DataModel.Towns
					 where town.Id == _street.ParentID
					 select town).FirstOrDefault();
			_region = (from region in Session.DataModel.Regions
					   where region.Id == _town.RegionId
					   select region).FirstOrDefault();
			_country = (from country in Session.DataModel.Countries
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
		Town _town;

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

				RefreshTowns();

				RaisePropertyChanged("Region");
			}
		}

		public Town Town
		{
			get
			{
				return _town;
			}
			set
			{
				_town = value;

				if (value != null)
				{
					_street.ParentID = value.Id;
				}

				RaisePropertyChanged("Town");
			}
		}

		#endregion

		#region FilteredCollections

		ObservableCollection<Country> _countries;
		ObservableCollection<Region> _regions;
		ObservableCollection<Town> _towns;


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

		public ObservableCollection<Town> Towns
		{
			get
			{
				if (_towns == null)
				{
					_towns = new ObservableCollection<Town>();
				}
				return _towns;
			}
			set
			{
				_towns = value;
				RaisePropertyChanged("Towns");
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

		void RefreshTowns()
		{
			if (Region != null)
			{
				Towns = new ObservableCollection<Town>(
					(from town in Session.DataModel.Towns
					 where town.RegionId == Region.Id
					 orderby town.Name
					 select town));
			}
			else
			{
				Towns = new ObservableCollection<Town>();
			}

			if (!Towns.Contains(Town))
			{
				Town = null;
			}
		}

		#endregion

	}
}
