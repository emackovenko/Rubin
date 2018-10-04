using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Admission;
using Admission.DialogService;
using Admission.ViewModel.Workspaces.ContractForming.Editors;
using CommonMethods.TypeExtensions.exString;	 
using Admission.ViewModel.Documents;

namespace Admission.ViewModel.Workspaces.ContractForming
{
	public class ContractListViewModel: ViewModelBase
	{

		ObservableCollection<EntrantContract> _contracts;

		public ObservableCollection<EntrantContract> Contracts
		{
			get
			{
				if (_contracts == null)
				{
					var contracts = Session.DataModel.EntrantContracts.ToList();
                    contracts = contracts.Where(c => c.Entrant.Claim.Campaign.CampaignStatusId == 2).ToList();
                    _contracts = new ObservableCollection<EntrantContract>(contracts);
				}
				return _contracts;
			}
			set
			{
				_contracts = value;
				RaisePropertyChanged("Contracts");
			}
		}

		EntrantContract _selectedContract;
		
		public EntrantContract SelectedContract
		{
			get
			{
				if (_selectedContract == null)
				{
					_selectedContract = Contracts.FirstOrDefault();
				}
				return _selectedContract;
			}

			set
			{
				_selectedContract = value;
				RaisePropertyChanged("SelectedContract");
			}
		}

		public RelayCommand PrintContractCommand
		{
			get
			{
				return new RelayCommand(PrintContract, PrintContractCanExecute);
			}
		}

		void PrintContract()
		{
			var doc = new EntrantContractDocument(SelectedContract);
			DialogLayer.ShowDocument(doc);
		}

		bool PrintContractCanExecute()
		{
			return SelectedContract != null;
		}

		public RelayCommand RefreshCommand
		{
			get
			{
				return new RelayCommand(Refresh);
			}
		}

		void Refresh()
		{
			Contracts = null;
		}

		public RelayCommand RemoveContractCommand
		{
			get
			{
				return new RelayCommand(RemoveContract);
			}
		}

		void RemoveContract()
		{
			if (Messenger.RemoveQuestion())
			{
				Session.DataModel.EntrantContracts.Remove(SelectedContract);
				Session.DataModel.SaveChanges();
				Refresh();
			}
		}


		public RelayCommand AgreementCommand
		{
			get
			{
				return new RelayCommand(Agreement);
			}
		}

		void Agreement()
		{																							
			if (SelectedContract.ContractIndividualPlanAuxAgreements.Count > 0)
			{
				var agreement = SelectedContract.ContractIndividualPlanAuxAgreements.First();
				var vm = new ContractIndividualPlanAgreementViewModel(agreement);
				if (DialogLayer.ShowEditor(EditingContent.ContractIndividualPlanAgreement, vm))
				{
					Session.DataModel.SaveChanges();
				}
			}
			else
			{
				var agreement = new ContractIndividualPlanAuxAgreement();
				agreement.EntrantContract = SelectedContract;
				var vm = new ContractIndividualPlanAgreementViewModel(agreement);
				if (DialogLayer.ShowEditor(EditingContent.ContractIndividualPlanAgreement, vm))
				{
					Session.DataModel.ContractIndividualPlanAuxAgreements.Add(agreement);
					Session.DataModel.SaveChanges();
				}
			}
		}



		void SearchClaimByEntrantName(string entrantName)
		{
			if (entrantName.Length > 0)
			{
				var searchingResult = GetClaimByEntrantName(entrantName);
				if (searchingResult != null)
				{
					SelectedContract = searchingResult;
					RaisePropertyChanged("SelectedContract");
				}
			}
		}


		/// <summary>
		/// Возвращает заявление, в котором Ф.И.О. абитуриента содержит входной параметр (регистронезависимо)
		/// </summary>
		/// <param name="entrantName">Ф.И.О. абитуриента</param>
		/// <returns></returns>
		EntrantContract GetClaimByEntrantName(string entrantName)
		{
			string searchedValue = entrantName.ToLower();

			EntrantContract searchResult = null;

			foreach (var contract in Contracts)
			{
				var claim = contract.Entrant.Claim;
				var entrant = claim.Entrants.First();

				string currentValue = string.Format("{0} {1} {2}",
					entrant.LastName, entrant.FirstName, entrant.Patronymic);
				currentValue = currentValue.ToLower();

				if (currentValue.Contains(searchedValue))
				{
					searchResult = contract;
					break;
				}
			}

			return searchResult;
		}


		public RelayCommand<string> SearchClaimByEntrantNameCommand
		{
			get
			{
				return new RelayCommand<string>(SearchClaimByEntrantName);
			}
		}


		public RelayCommand AgreementPrintCommand
		{
			get
			{
				return new RelayCommand(AgreementPrint, AgreementPrintCanExecute);
			}
		}

		void AgreementPrint()
		{
			var doc = new ContractIndividualPlanAgreementDocument(SelectedContract.ContractIndividualPlanAuxAgreements.First());
			DialogLayer.ShowDocument(doc);
		}

		bool AgreementPrintCanExecute()
		{
			return SelectedContract?.ContractIndividualPlanAuxAgreements.Count > 0;
		}

	}
}
