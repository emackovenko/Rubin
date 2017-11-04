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
	public class TownEditorViewModel : ViewModelBase
	{

		#region Constructors

		public TownEditorViewModel(Town town)
		{
			_town = town;
			_region = (from region in Session.DataModel.Regions
					   where region.Id == town.RegionId
					   select region).FirstOrDefault();
			_country = (from country in Session.DataModel.Countries
						where country.Id == _region.CountryId
						select country).FirstOrDefault();
		}

		#endregion

		#region ParentEntities

		Town _town;

		public Town Town
		{
			get
			{
				return _town;
			}
			set
			{
				_town = value;
				RaisePropertyChanged("Town");
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
				if (value != null)
				{
					_town.RegionId = value.Id;
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

		public RelayCommand RefreshRegionsCommand
		{
			get
			{
				return new RelayCommand(RefreshRegions);
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
		}

		#endregion

		#endregion

	}
}
