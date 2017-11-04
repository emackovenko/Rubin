using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Admission;

namespace Admission.ViewModel.Workspaces.ContractForming.Editors
{
	public class EntrantContractEditorViewModel: ViewModelBase
	{
		public EntrantContractEditorViewModel(EntrantContract contract)
		{
			Contract = contract;
		}

		EntrantContract _contract;

		public EntrantContract Contract
		{
			get
			{
				return _contract;
			}

			set
			{
				_contract = value;
				RaisePropertyChanged("Contract");
			}
		}

		public ObservableCollection<ContragentType> ContragentTypes
		{
			get
			{
				return new ObservableCollection<ContragentType>(Session.DataModel.ContragentTypes);
			}
		}

		public RelayCommand CreateContragentCommand
		{
			get
			{
				return new RelayCommand(CreateContragent, CreateContragentCanExecute);
			}
		}

		void CreateContragent()
		{
			if (Contract.ContragentType.Id == 1)
			{
				var agent = new ContragentPerson
				{
					LastName = Contract.Entrant.LastName,
					FirstName = Contract.Entrant.FirstName,
					Patronymic = Contract.Entrant.Patronymic,
					Address = Contract.Entrant.Address,
					PhoneNumber = Contract.Entrant.Phone,
					Email = Contract.Entrant.Email,
					DocumentSeries = Contract.Entrant.Claim.IdentityDocuments.First().Series,
					DocumentNumber = Contract.Entrant.Claim.IdentityDocuments.First().Number,
					DocumentDate = Contract.Entrant.Claim.IdentityDocuments.First().Date,
					DocumentOrganization = Contract.Entrant.Claim.IdentityDocuments.First().Organization,
					BirthDate = Contract.Entrant.Claim.IdentityDocuments.First().BirthDate,
					IdentityDocumentType = Contract.Entrant.Claim.IdentityDocuments.First().IdentityDocumentType
				};
				Contract.ContragentPerson = agent;
				RaisePropertyChanged("Contract");
			}
			if (Contract.ContragentType.Id == 2)
			{
				var agent = new ContragentPerson();
				var vm = new ContragentPersonEditorViewModel(agent);
				if (DialogService.DialogLayer.ShowEditor(EditingContent.ContragentPerson, vm))
				{
					Contract.ContragentPerson = agent;
					RaisePropertyChanged("Contract");
				}
			}
			if (Contract.ContragentType.Id == 3)
			{
				var agent = new ContragentOrganization();
				var vm = new ContragentOrganizationEditorViewModel(agent);
				if (DialogService.DialogLayer.ShowEditor(EditingContent.ContragentOrganization, vm))
				{
					Contract.ContragentOrganization = agent;
					RaisePropertyChanged("Contract");
				}
			}
		}
		
		bool CreateContragentCanExecute()
		{
			return Contract.ContragentType != null;
		}


		public RelayCommand FullPriceCalculateCommand
		{
			get
			{
				return new RelayCommand(FullPriceCalculate);
			}
		}


		/// <summary>
		/// Вычисление полной стоимости обучения
		/// </summary>
		void FullPriceCalculate()
		{
			if (Contract != null)
			{
				if (Contract.YearPrice > 0 && Convert.ToDouble(Contract.TrainingPeriod) > 0)
				{
					try
					{
						// Округляем срок до наибольшего целого, если он не целый
						var trainingPeriod = Math.Ceiling(Convert.ToDouble(Contract.TrainingPeriod));

						// Вычисляем стоимость обучения за весь период равной [Стоимость за год]*[Срок обучения]
						Contract.FullPrice = Math.Round((double)(Contract.YearPrice * trainingPeriod), 2);
						RaisePropertyChanged("Contract");
					}
					catch (Exception)
					{

					}
				}
			}
		}
	}
}
