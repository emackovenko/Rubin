using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Model.Admission;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Admission.DialogService;

namespace Admission.ViewModel.Editors
{
	public class EducationOrganizationSelectorViewModel: ViewModelBase
	{

		#region Constructors 

		public EducationOrganizationSelectorViewModel(EducationOrganization organization)
		{
			_organization = organization;
			Country = _organization.Address.Country;
			Region = _organization.Address.Region;
			District = _organization.Address.District;
			Locality = _organization.Address.Locality;
			Town = _organization.Address.Town;
			OrgType = _organization.EducationOrganizationType;	 
		}

		#endregion
					  
		#region ParentEntities

		EducationOrganization _organization;

		public EducationOrganization Organization
		{
			get
			{
				return _organization;
			}
			set
			{
				_organization = value;
				RaisePropertyChanged("Organization");
			}
		}
		 
		#endregion
					   
		#region FilterEntities			 

		Country _country;
		Region _region;
		District _district;
		Locality _locality;
		Town _town;	  
		EducationOrganizationType _orgType;


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
				RefreshOrganizations();

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
				RefreshTowns();
				RefreshOrganizations();

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
				RefreshOrganizations();

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

				RefreshOrganizations();

				RaisePropertyChanged("Locality");
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

				RefreshOrganizations();

				RaisePropertyChanged("Town");
			}
		}

		public EducationOrganizationType OrgType
		{
			get
			{
				if (_orgType == null)
				{
					_orgType = Session.DataModel.EducationOrganizationTypes.FirstOrDefault();
				}
				return _orgType;
			}
			set
			{
				_orgType = value;

				RefreshOrganizations();

				RaisePropertyChanged("OrgType");
			}
		}

		#endregion		  
					   	
		#region FilteredCollections

		ObservableCollection<Country> _countries;
		ObservableCollection<Region> _regions;
		ObservableCollection<District> _districts;
		ObservableCollection<Locality> _localities;
		ObservableCollection<Town> _towns;
		ObservableCollection<EducationOrganizationType> _orgTypes;
		ObservableCollection<EducationOrganization> _orgs;
						   

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

		public ObservableCollection<EducationOrganizationType> OrgTypes
		{
			get
			{
				_orgTypes = new ObservableCollection<EducationOrganizationType>(Session.DataModel.EducationOrganizationTypes);
				return _orgTypes;
			}
			set
			{
				_orgTypes = value;
				RaisePropertyChanged("OrgTypes");
			}
		}

		public ObservableCollection<EducationOrganization> Orgs
		{
			get
			{
				if (_orgs == null)
				{
					_orgs = new ObservableCollection<EducationOrganization>();
				}
				return _orgs;
			}
			set
			{
				_orgs = value;
				RaisePropertyChanged("Orgs");
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

		void RefreshOrganizations()
		{
			if (Town != null)
			{
				Orgs = new ObservableCollection<EducationOrganization>(
					(from org in Session.DataModel.EducationOrganizations
					 where org.Address.TownId == Town.Id &&
						org.EducationOrganizationTypeId == OrgType.Id
					 orderby org.Name
					 select org));			 
			}
			else
			{
				if (Locality != null)
				{
					Orgs = new ObservableCollection<EducationOrganization>(
						(from org in Session.DataModel.EducationOrganizations
						 where org.Address.LocalityId == Locality.Id &&
							org.EducationOrganizationTypeId == OrgType.Id
						 orderby org.Name
						 select org));	 
				}
				else
				{
					Orgs = new ObservableCollection<EducationOrganization>();
				}
			}
		}  	

		#endregion	 

		#region AddCommands

		#region Commands

		public RelayCommand AddEducationOrganizationCommand
		{
			get
			{
				return new RelayCommand(AddEducationOrganization);
			}
		}


		#endregion

		#region Methods

		void AddEducationOrganization()
		{
			var address = new Address()
			{
				Country = Session.DataModel.Countries.FirstOrDefault()
			};

			
			var eduOrg = new EducationOrganization()
			{
				Address = address,
				EducationOrganizationType = OrgType
			};

			var vm = new EducationOrganizationEditorViewModel(eduOrg);

			if (DialogLayer.ShowEditor(EditingContent.EducationOrganizationEditor, vm))
			{
				using (var context = new AdmissionDatabase())
				{
					context.Addresses.Add(address);
					context.EducationOrganizations.Add(eduOrg);
					context.SaveChanges();
				}
				RefreshOrganizations();
			}	 
		}

		#endregion

		#endregion

	}
}
