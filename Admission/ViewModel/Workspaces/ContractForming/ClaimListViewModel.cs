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

namespace Admission.ViewModel.Workspaces.ContractForming
{
    public class ClaimListViewModel: ViewModelBase
    {
        #region Родительские сущности
        
        public ObservableCollection<Claim> Claims
        {
            get
            {
                return new ObservableCollection<Claim>(Session.DataModel.Claims.ToList().Where(c => c.FinanceSource.Id == 2).Where(c => c.Campaign.CampaignStatusId == 2));
            }
        }

        Claim _selectedClaim;

        public Claim SelectedClaim
        {
            get
            {
                if (_selectedClaim == null)
                {
                    _selectedClaim = Claims.FirstOrDefault();
                }
                return _selectedClaim;
            }
            set
            {
                _selectedClaim = value;
                RaisePropertyChanged("SelectedClaim");
            }
        }

        #endregion

        #region Внешняя логика

        #region Команды

        public RelayCommand AddContractCommand
        {
            get
            {
                return new RelayCommand(AddContract, AddContractCanExecute);
            }
        }


		public RelayCommand<string> SearchClaimByEntrantNameCommand
		{
			get
			{
				return new RelayCommand<string>(SearchClaimByEntrantName);
			}
		}

		#endregion

		#region Методы

		void AddContract()
        {
			// Получаем номер
			int num = 0;
            var col = Session.DataModel.EntrantContracts.ToList().Where(ec => ec.Entrant.Claim.Campaign.CampaignStatusId == 2);
			if (col.Count() > 0)
			{
				num = (from c in col
					   select int.Parse(c.Number.WithoutLetters())).Max();
			}

			// Создаем договор
			var contract = new EntrantContract
			{
				Date = DateTime.Now,
				Entrant = SelectedClaim.Person,
				Number = string.Format("{0}{1}", num + 1, SelectedClaim.FirstDirection.ContractNumberPart)
			};
            if (SelectedClaim.EducationForm.Id == 2)
            {
                contract.Number += SelectedClaim.EducationForm.Name[0];
            }

			// Создаем ВМ и редактор
			var vm = new EntrantContractEditorViewModel(contract);
			if (DialogLayer.ShowEditor(EditingContent.EntrantContract, vm))
			{
				SelectedClaim.Entrants.First().EntrantContracts.Add(contract);
				Session.DataModel.SaveChanges();
			}			
        }

		void SearchClaimByEntrantName(string entrantName)
		{
			if (entrantName.Length > 0)
			{
				var searchingResult = GetClaimByEntrantName(entrantName);
				if (searchingResult != null)
				{
					SelectedClaim = searchingResult;
				}
			}
		}


		/// <summary>
		/// Возвращает заявление, в котором Ф.И.О. абитуриента содержит входной параметр (регистронезависимо)
		/// </summary>
		/// <param name="entrantName">Ф.И.О. абитуриента</param>
		/// <returns></returns>
		Claim GetClaimByEntrantName(string entrantName)
		{
			string searchedValue = entrantName.ToLower();

			Claim searchResult = null;

			foreach (var claim in Claims)
			{
				var entrant = claim.Entrants.First();

				string currentValue = string.Format("{0} {1} {2}",
					entrant.LastName, entrant.FirstName, entrant.Patronymic);
				currentValue = currentValue.ToLower();

				if (currentValue.Contains(searchedValue))
				{
					searchResult = claim;
					break;
				}
			}

			return searchResult;
		}

		#endregion

		#region Проверки

		bool AddContractCanExecute()
        {
            return SelectedClaim != null;
        }

        #endregion

        #endregion
    }
}
