using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Admission;
using Admission.DialogService;

namespace Admission.ViewModel.Editors
{
	public class EducationOrganizationEditorViewModel: ViewModelBase
	{

		#region Constructors

		public EducationOrganizationEditorViewModel(EducationOrganization org)
		{
			Organization = org;
		}

		#endregion

		#region Parents

		EducationOrganization _org;

		public EducationOrganization Organization
		{
			get
			{
				return _org;
			}
			set
			{
				_org = value;
				RaisePropertyChanged("Organization");
			}
		}

		#endregion

		#region Collections

		ObservableCollection<EducationOrganizationType> _orgTypes;

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

		#endregion

		#region AddressCommands

		#region Commands

		public RelayCommand SelectAddressCommand
		{
			get
			{
				return new RelayCommand(SelectAddress);
			}
		}

		#endregion

		#region Methods

		void SelectAddress()
		{
			if (DialogLayer.ShowEditor(EditingContent.AddressSelector,
				new AddressSelectorViewModel(_org.Address)))
			{
				RaisePropertyChanged("Organization");
			}
		}

		#endregion

		#endregion	 

	}
}
