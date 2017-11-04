using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MoreLinq;
using Model.Admission;

namespace Admission.ViewModel.Workspaces.ContractForming.Editors
{
	public class ContragentOrganizationEditorViewModel: ViewModelBase
	{
		public ContragentOrganizationEditorViewModel(ContragentOrganization agent)
		{
			Agent = agent;
			if (Agent.Address == null)
			{
				Agent.Address = new Address { Country = Session.DataModel.Countries.First() };
			}
		}

		ContragentOrganization _agent;

		public ContragentOrganization Agent
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

		public ObservableCollection<string> Banks
		{
			get
			{
				var collection = new ObservableCollection<string>();
				foreach (var bank in Session.DataModel.ContragentOrganizations.ToList().Select(o => o.BankName).Distinct())
				{
					collection.Add(bank);

				}
				return collection;
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
			var vm = new Admission.ViewModel.Editors.AddressSelectorViewModel(Agent.Address);
			DialogService.DialogLayer.ShowEditor(EditingContent.AddressSelector, vm);
			RaisePropertyChanged("Agent");
		}

	}
}
