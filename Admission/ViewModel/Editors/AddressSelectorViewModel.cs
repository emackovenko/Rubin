using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Admission;
using Admission.DialogService;

namespace Admission.ViewModel.Editors
{
	public class AddressSelectorViewModel: ViewModelBase
	{

		#region	Constructors
					 
		public AddressSelectorViewModel(Address address)
		{
			_currentAddress = address;
			RefreshRegions();
			RefreshDistricts();
			RefreshTowns();
			RefreshLocalities();
			RefreshStreetsFromLocality();
			RefreshStreetsFromTown();
		}

		#endregion

		#region Parents

		Address _currentAddress;  
				  

		public Country Country
		{
			get
			{
				return _currentAddress.Country;
			}
			set
			{
				_currentAddress.Country = value;
				RefreshRegions();
				RaisePropertyChanged("Country");
			}
		}

		public Region Region
		{
			get
			{
				return _currentAddress.Region;
			}
			set
			{
				_currentAddress.Region = value;
				RefreshDistricts();
				RefreshTowns();
				RaisePropertyChanged("Region");
			}
		}

		public District District
		{
			get
			{
				return _currentAddress.District;
			}
			set
			{
				_currentAddress.District = value;
				RefreshLocalities();

				if (value != null)
				{
					Town = null;
				}

				RaisePropertyChanged("District");
			}
		}	 

		public Locality Locality
		{
			get
			{
				return _currentAddress.Locality;
			}
			set
			{
				_currentAddress.Locality = value;
				RefreshStreetsFromLocality();

				if (value != null)
				{
					Town = null;
				}

				RaisePropertyChanged("Locality");
			}
		}

		public Town Town
		{
			get
			{
				return _currentAddress.Town;
			}
			set
			{
				_currentAddress.Town = value;
				RefreshStreetsFromTown();

				if (value != null)
				{
					District = null;
					Locality = null;
				}

				RaisePropertyChanged("Town");
			}
		}

		public Street Street
		{
			get
			{
				return _currentAddress.Street;
			}
			set
			{
				_currentAddress.Street = value;
				RaisePropertyChanged("Street");
			}	
		}

		public string Building
		{
			get
			{
				return _currentAddress.BuildingNumber;
			}
			set
			{
				_currentAddress.BuildingNumber = value;
				RaisePropertyChanged("Building");
			}
		}

		public string Flat
		{
			get
			{
				return _currentAddress.FlatNumber;
			}
			set
			{
				_currentAddress.FlatNumber = value;
				RaisePropertyChanged("Flat");
			}
		}

		#endregion

		#region Collections	 

		/******************************************
		 * Был большой баг (сожрал 3 Гб памяти), 
		 * поэтому коллекции подгружаются так
		 *****************************************/

		ObservableCollection<Country> _countries;
		ObservableCollection<Region> _regions;
		ObservableCollection<District> _districts;
		ObservableCollection<Locality> _localities;
		ObservableCollection<Town> _towns;
		ObservableCollection<Street> _streets;

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

		public ObservableCollection<Street> Streets
		{
			get
			{
				if (_streets == null)
				{
					_streets = new ObservableCollection<Street>();
				}
				return _streets;
			}

			set
			{
				_streets = value;
				RaisePropertyChanged("Streets");
			}
		}

		#endregion

		#region RefreshMethods
				
		#region Methods

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

		void RefreshDistricts()
		{
			if (Region != null)
			{					
				Districts = new ObservableCollection<District>(
					(from district in Session.DataModel.Districts
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

		void RefreshStreetsFromTown()
		{
			if (Town != null)
			{
				Streets = new ObservableCollection<Street>(
					(from street in Session.DataModel.Streets
					 where street.ParentID == Town.Id
						&& street.ParentLevel == 3
					 orderby street.Name
					 select street));	   
			}

			if (!Streets.Contains(Street))
			{
				Street = null;
			}
		}

		void RefreshStreetsFromLocality()
		{
			if (Locality != null)
			{
				Streets = new ObservableCollection<Street>(
					(from street in Session.DataModel.Streets
					 where street.ParentID == Locality.Id
						&& street.ParentLevel == 4
					 orderby street.Name
					 select street));
			}

			if (!Streets.Contains(Street))
			{
				Street = null;
			}
		}

		#endregion

		#endregion

		#region SetNullCommands


		#region Commands

		public RelayCommand SetNullRegionCommand
		{
			get
			{
				return new RelayCommand(SetNullRegion, SetNullRegionCanExecute);
			}
		}

		public RelayCommand SetNullDistrictCommand
		{
			get
			{
				return new RelayCommand(SetNullDistrict, SetNullDistrictCanExecute);
			}
		}

		public RelayCommand SetNullLocalityCommand
		{
			get
			{
				return new RelayCommand(SetNullLocality, SetNullLocalityCanExecute);
			}
		}

		public RelayCommand SetNullTownCommand
		{
			get
			{
				return new RelayCommand(SetNullTown, SetNullTownCanExecute);
			}
		}

		public RelayCommand SetNullStreetCommand
		{
			get
			{
				return new RelayCommand(SetNullStreet, SetNullStreetCanExecute);
			}
		}

		public RelayCommand SetNullBuildingCommand
		{
			get
			{
				return new RelayCommand(SetNullBuilding, SetNullBuildingCanExecute);
			}
		}

		public RelayCommand SetNullFlatCommand
		{
			get
			{
				return new RelayCommand(SetNullFlat, SetNullFlatCanExecute);
			}
		}

		#endregion


		#region Methods

		void SetNullRegion()
		{
			Region = null;		   
		}

		void SetNullDistrict()
		{
			District = null;  
		}

		void SetNullLocality()
		{
			Locality = null;					  
		}

		void SetNullTown()
		{
			Town = null;						   
		}

		void SetNullStreet()
		{
			Street = null;						   
		}

		void SetNullBuilding()
		{
			Building = null;					   
		}

		void SetNullFlat()
		{
			Flat = null;	
		}

		#endregion


		#region CheckMethods

		bool SetNullRegionCanExecute()
		{
			return Region != null;
		}

		bool SetNullDistrictCanExecute()
		{
			return District != null;
		}

		bool SetNullLocalityCanExecute()
		{
			return Locality != null;
		}

		bool SetNullTownCanExecute()
		{
			return Town != null;
		}

		bool SetNullStreetCanExecute()
		{
			return Street != null;
		}

		bool SetNullBuildingCanExecute()
		{
			return Building != null;
		}

		bool SetNullFlatCanExecute()
		{
			return Flat != null;
		}

		#endregion


		#endregion

		#region AddCommands


		#region Commands


		public RelayCommand AddCountryCommand
		{
			get
			{
				return new RelayCommand(AddCountry);
			}
		}

		public RelayCommand AddRegionCommand
		{
			get
			{
				return new RelayCommand(AddRegion, AddRegionCanExecute);
			}
		}

		public RelayCommand AddDistrictCommand
		{
			get
			{
				return new RelayCommand(AddDistrict);
			}
		}

		public RelayCommand AddLocalityCommand
		{
			get
			{
				return new RelayCommand(AddLocality, AddLocalityCanExecute);
			}
		}

		public RelayCommand AddTownCommand
		{
			get
			{
				return new RelayCommand(AddTown, AddTownCanExecute);
			}
		}

		public RelayCommand AddStreetCommand
		{
			get
			{
				return new RelayCommand(AddStreet, AddStreetCanExecute);
			}
		}

		#endregion


		#region Methods


		void AddCountry()
		{
			var country = new Country();
			var vm = new CountryEditorViewModel(country);
			if (DialogLayer.ShowEditor(EditingContent.CountryEditor, vm))
			{
				using (var context = new AdmissionDatabase())
				{
					context.Countries.Add(country);
					context.SaveChanges();
				}
				RaisePropertyChanged("Countries");
			}
		}

		void AddRegion()
		{
			var region = new Region();
			region.CountryId = Country.Id;
			var vm = new RegionEditorViewModel(region);

			if (DialogLayer.ShowEditor(EditingContent.RegionEditor, vm))
			{
				using (var context = new AdmissionDatabase())
				{
					context.Regions.Add(region);  
					context.SaveChanges();
					Region = region;
				}
				RefreshRegions();		 
			} 
		}

		void AddDistrict()
		{
			var district = new District();
			district.RegionId = Region.Id;

			var vm = new DistrictEditorViewModel(district);

			if (DialogLayer.ShowEditor(EditingContent.DistrictEditor, vm))
			{
				using (var context = new AdmissionDatabase())
				{
					context.Districts.Add(district);
					context.SaveChanges();
					District = district;
				}
				RefreshDistricts();		  
			}

		}

		void AddLocality()
		{
			var locality = new Locality();
			locality.DistrictId = District.Id;

			var vm = new LocalityEditorViewModel(locality);

			if (DialogLayer.ShowEditor(EditingContent.LocalityEditor, vm))
			{
				using (var context = new AdmissionDatabase())
				{
					context.Localities.Add(locality);
					context.SaveChanges();
					Locality = locality;
				}
				RefreshLocalities();  		  
			}
		}

		void AddTown()
		{
			var town = new Town();
			town.RegionId = Region.Id;

			var vm = new TownEditorViewModel(town);

			if (DialogLayer.ShowEditor(EditingContent.TownEditor, vm))
			{
				using (var context = new AdmissionDatabase())
				{
					context.Towns.Add(town);
					context.SaveChanges();
					District = null;
					Locality = null;
					Street = null; 
					RefreshTowns();
					Town = town;
				}					  					
			}  
		}

		void AddStreet()
		{
			var street = new Street();

			if (Town != null)
			{
				street.ParentID = Town.Id; 

				var vm = new StreetFromTownEditorViewModel(street);

				if (DialogLayer.ShowEditor(EditingContent.StreetFromTownEditor, vm))
				{
					using (var context = new AdmissionDatabase())
					{
						context.Streets.Add(street);
						context.SaveChanges();
						RefreshStreetsFromTown();
						Street = street;
					}
				} 
			}
			else
			{
				if (Locality != null)
				{
					street.ParentID = Locality.Id; 

					var vm = new StreetFromLocalityEditorViewModel(street);

					if (DialogLayer.ShowEditor(EditingContent.StreetFromLocalityEditor, vm))
					{
						using (var context = new AdmissionDatabase())
						{
							context.Streets.Add(street);
							context.SaveChanges();
							RefreshStreetsFromLocality();
							Street = street;
						}
					}
				}
			}
		}

		#endregion


		#region CheckMethods

		bool AddRegionCanExecute()
		{
			return Country != null;
		}

		bool AddDistrictCanExecute()
		{
			return Region != null;
		}

		bool AddLocalityCanExecute()
		{
			return District != null;
		}

		bool AddTownCanExecute()
		{
			return Region != null;
		}

		bool AddStreetCanExecute()
		{
			return Locality != null || Town != null;
		}

		#endregion

		#endregion		

	}
}
