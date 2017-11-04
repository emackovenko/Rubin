using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Admission.ViewModel.Workspaces.ContractForming.Editors
{
	public class ContractIndividualPlanAgreementViewModel: ViewModelBase
	{
		ContractIndividualPlanAuxAgreement _agreement;

		public ContractIndividualPlanAgreementViewModel(ContractIndividualPlanAuxAgreement agreement)
		{
			_agreement = agreement ?? throw new ArgumentNullException();
		}

		public ContractIndividualPlanAuxAgreement Agreement
		{
			get
			{
				return _agreement;
			}
			set
			{
				_agreement = value;
				RaisePropertyChanged("Agreement");
			}
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
			if (Agreement != null)
			{
				if (Agreement.YearPrice > 0 && Convert.ToDouble(Agreement.TrainingPeriod) > 0)
				{
					try
					{
						// Округляем срок до наибольшего целого, если он не целый
						var trainingPeriod = Math.Ceiling(Convert.ToDouble(Agreement.TrainingPeriod));

						// Вычисляем стоимость обучения за весь период равной [Стоимость за год]*[Срок обучения]
						Agreement.FullPrice = Math.Round((double)(Agreement.YearPrice * trainingPeriod), 2);
						RaisePropertyChanged("Agreement");
					}
					catch (Exception)
					{

					}
				}
			}
		}
	}
}
