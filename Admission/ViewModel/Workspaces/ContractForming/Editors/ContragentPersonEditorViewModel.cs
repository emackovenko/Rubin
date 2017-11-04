using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Admission;
using MoreLinq;


namespace Admission.ViewModel.Workspaces.ContractForming.Editors
{
	public class ContragentPersonEditorViewModel: ViewModelBase
	{

		public ContragentPersonEditorViewModel(ContragentPerson agent)
		{
			_agent = agent;
			if (_agent.Address == null)
			{
				_agent.Address = new Address { Country = Session.DataModel.Countries.First()};
			}
		}

		ContragentPerson _agent;

		public ContragentPerson Agent
		{
			get
			{
				return _agent;
			}

			set
			{
				_agent = value;
				RaisePropertyChanged("Agent");
			}
		}

		public ObservableCollection<string> DocOrganizations
		{
			get
			{
				var collection = new ObservableCollection<string>();
				foreach (var doc in Session.DataModel.IdentityDocuments.ToList().Select(id => id.Organization).Distinct())
				{
					collection.Add(doc);
				}
				foreach (var doc in Session.DataModel.ContragentPersons.Select(a => a.DocumentOrganization).Distinct())
				{
					if (!collection.Contains(doc))
					{
						collection.Add(doc);
					}
				}
				return collection;
			}
		}

		public ObservableCollection<IdentityDocumentType> DocumentTypes
		{
			get
			{
				return new ObservableCollection<IdentityDocumentType>(Session.DataModel.IdentityDocumentTypes);
			}
		}

		public Address Address
		{
			get
			{
				return Agent.Address;
			}
			set
			{
				Agent.Address = value;
				RaisePropertyChanged("Address");
			}
		}

		public RelayCommand GenerateAddressCommand
		{
			get
			{
				return new RelayCommand(GenerateAddress);
			}
		}

		void GenerateAddress()
		{
			var vm = new Admission.ViewModel.Editors.AddressSelectorViewModel(Address);
			DialogService.DialogLayer.ShowEditor(EditingContent.AddressSelector, vm);
			RaisePropertyChanged("Address");
		}

	}
}
