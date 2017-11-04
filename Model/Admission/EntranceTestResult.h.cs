using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using CommonMethods.TypeExtensions.exDateTime;

namespace Model.Admission
{
	public partial class EntranceTestResult
	{
		public EntranceTestResult()
		{
			Value = 0;
		}

		public ObservableCollection<EntranceTest> AvailableTests
		{
			get
			{
				//грузим все экзамены и фильтруем по условиям приёма
				var tests = new ObservableCollection<EntranceTest>();
				foreach (var condition in Claim.ClaimConditions)
				{
					var compGroup = condition.CompetitiveGroup;
					foreach (var cet in compGroup.CompetitionEntranceTests)
					{
						if (!tests.Contains(cet.EntranceTest))
						{
							//отсеиваем те экзамены, по которым заявление не попадает в период регистрации
							if (((DateTime)Claim.RegistrationDate).IsInRange((DateTime)cet.EntranceTest.RegistrationDateRangeBegin,
								(DateTime)cet.EntranceTest.RegistrationDateRangeEnd))
							{
								tests.Add(cet.EntranceTest);
							}  							
						}
					}
				}

				/************************************************************************************************
				 * 
				 * Злоебучий костыль:
				 * Если абитуриент принес доки до периода, когда у него идет регистрация на экзамен, то херачим 
				 * без проверки даты регистрации
				 * 
				 ***********************************************************************************************/
				if (tests.Count == 0)
				{
					foreach (var condition in Claim.ClaimConditions)
					{
						var compGroup = condition.CompetitiveGroup;
						foreach (var cet in compGroup.CompetitionEntranceTests)
						{
							if (!tests.Contains(cet.EntranceTest))
							{	
								tests.Add(cet.EntranceTest);
							}
						}
					}
				}

				//удаляем уже имеющиеся экзамены
				foreach (var etr in Claim.EntranceTestResults)
				{
					if (tests.Contains(etr.EntranceTest) && etr.EntranceTest != EntranceTest)
					{
						tests.Remove(etr.EntranceTest);
					}
				}		

				return tests;
			}
		} 

	}
}
