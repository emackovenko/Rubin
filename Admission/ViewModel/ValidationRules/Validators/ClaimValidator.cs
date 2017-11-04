using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;

namespace Admission.ViewModel.ValidationRules.Validators
{
	public class ClaimValidator : IEntityValidator
	{	   
		public ClaimValidator(Claim claim)
		{
			_claim = claim;
		}
		 
		Claim _claim;  
		List<string> _errorList = new List<string>();

		public bool IsValid
		{
			get
			{
				return CheckClaim();
			}
		}

		public List<string> ErrorList
		{
			get
			{
				return _errorList;
			}

			set
			{
				_errorList = value;
			}
		}	
			

		bool CheckClaim()
		{
			ErrorList.Clear();
			bool checkResult = true;

			// вычисляем результат последовательно, чтобы определить все ошибки сразу	   
			checkResult = CheckClaimConditions() && checkResult;
			checkResult = CheckEtrantLastName() && checkResult;
			checkResult = CheckEntrantFirstName() && checkResult;
			checkResult = CheckEntrantBirthDate() && checkResult;
			checkResult = CheckEntrantEmail() && checkResult;
			checkResult = CheckAddress() && checkResult;
			checkResult = CheckPhone() && checkResult;
			checkResult = CheckIdentityDocumentType() && checkResult;
			checkResult = CheckIdentityDocumentNumber() && checkResult;
			checkResult = CheckIdentityDocumentDate() && checkResult;
			checkResult = CheckIdentityDocumentOrganization() && checkResult;
			checkResult = CheckCitizenship() && checkResult;
			checkResult = CheckEducationDocumentExisting() && checkResult;	

			return checkResult;
		}

		bool CheckClaimConditions()
		{
			var conditions = _claim.ClaimConditions;

			// проверить количество условий приёма
			if (conditions.Count < 1 || conditions.Count > 3)
			{
				_errorList.Add("Количество выбранных условий приема не соответствует правилам приёма.");
				return false;
			}

			// проверить наличие первого приоритета	 

			var firstPriotityConditions = (from cond in conditions
										   where cond.Priority == 1
										   select cond);
			if (firstPriotityConditions.Count() != 1)
			{
				_errorList.Add("Количество условий приёма с первым приоритетом не равно 1.");
				return false;
			}

			//проверить уникальность приоритетов
			List<int> priorities = new List<int>();
			foreach (var condition in conditions)
			{
				if (priorities.Contains((int)condition.Priority))
				{		
					_errorList.Add("Приоритеты условий приёма должны быть уникальными в разрезе одного заявления.");
					return false;
				}
				priorities.Add((int)condition.Priority);
			}
			return true;
		}

		bool CheckEtrantLastName()
		{
			var entrant = _claim.Entrants.First();
			//Проверяем наличие
			if (entrant.LastName == null)
			{
				_errorList.Add("Фамилия абитуриента должна быть задана.");
				return false;
			}

			//проверяем формат
			if (!RegularExpressionProvider.OnlyRusLetterString.IsMatch(entrant.LastName))
			{
				_errorList.Add("Фамилия абитуриента не соответствует установленному формату.");
				return false;
			}

			return true;
		}

		bool CheckEntrantFirstName()
		{
			var entrant = _claim.Entrants.First();
			//Проверяем наличие
			if (entrant.FirstName == null)
			{
				_errorList.Add("Имя абитуриента должно быть задано.");
				return false;
			}

			//проверяем формат
			if (!RegularExpressionProvider.OnlyRusLetterString.IsMatch(entrant.FirstName))
			{
				_errorList.Add("Имя абитуриента не соответствует установленному формату.");
				return false;
			}

			return true;
		}

		bool CheckEntrantBirthDate()
		{
			DateTime birthdate = (DateTime)_claim.IdentityDocuments.First().BirthDate;
			if (birthdate < DateTime.Parse("1950-01-01") && birthdate > DateTime.Parse("2005-01-01"))
			{
				_errorList.Add("Дата рождения не входит в диапазон от 01.01.1950 до 01.01.2005.");
				return false;
			}
			else
			{
				return true;
			}
		}

		bool CheckEntrantEmail()
		{					
			string email = _claim.Entrants.First().Email;

			// наличие
			if (email == null)
			{
				return true;
			}

			// формат
			if (!RegularExpressionProvider.Email.IsMatch(email))
			{
				_errorList.Add("Поле e-mail не соответствует заданному формату адреса электронной почты.");
				return false;
			}

			return true;
		}

		bool CheckAddress()
		{
			bool res = true;
			var address = _claim.Entrants.First().Address;

			if (address.Country == null)
			{
				_errorList.Add("В поле Адрес не указана страна.");
				res = false;
			}

			if (address.Region == null)
			{
				_errorList.Add("В поле Адрес не указан регион.");
				res = false;
			}

			if (address.Town == null && address.District == null && address.Locality == null)
			{  
				_errorList.Add("В поле Адрес не указан город или населенный пункт.");
				res = false;
			}

			if (address.Street == null)
			{
				_errorList.Add("В поле Адрес не указана улица.");
				res = false;
			}

			if (address.BuildingNumber == null)
			{
				_errorList.Add("В поле Адрес не указан номер дома.");
				res = false;
			}

			return res;
		}

		bool CheckPhone()
		{
			if (_claim.Entrants.First().Phone == null)
			{
				_errorList.Add("У абитуриента должен быть указан номер телефона.");
				return false;
			}
			else
			{
				return true;
			}
		}

		bool CheckIdentityDocumentType()
		{
			var idDoc = _claim.IdentityDocuments.First();
			if (idDoc.IdentityDocumentType == null)
			{
				_errorList.Add("У абитуриента должен быть указан тип документа, удостоверяющего личность.");
				return false;
			}
			else
			{
				return true;
			}
		}		   					 

		bool CheckIdentityDocumentNumber()
		{
			var idDoc = _claim.IdentityDocuments.First();
			if (idDoc.Number == null)
			{
				_errorList.Add("У абитуриента должен быть указан номер документа, удостоверяющего личность.");
				return false;
			}
			else
			{
				return true;
			}
		}

		bool CheckIdentityDocumentDate()
		{
			var idDoc = _claim.IdentityDocuments.First();
			if (idDoc.Date == null)
			{
				_errorList.Add("У абитуриента должен быть указан тип документа, удостоверяющего личность.");
				return false;
			}
			else
			{
				if (idDoc.Date < DateTime.Parse("1950-01-01") || idDoc.Date > DateTime.Now)
				{
					_errorList.Add("Дата выдачи документа, удостоверяющего личность, не входит заданный диапазон.");
					return false;
				}
				else
				{
					return true;
				}
			}
		}

		bool CheckIdentityDocumentOrganization()
		{
			var idDoc = _claim.IdentityDocuments.First();

			if (idDoc.Organization == null)
			{
				_errorList.Add("Название организации, выдавшей документ, удостоверяющий личность, не соответствует заданному формату.");
				return false;
			}

			if (!RegularExpressionProvider.RussianText.IsMatch(idDoc.Organization))
			{
				_errorList.Add("Название организации, выдавшей документ, удостоверяющий личность, не соответствует заданному формату.");
				return false;
			}
			else
			{
				return true;
			}
		}

		bool CheckCitizenship()
		{
			var idDoc = _claim.IdentityDocuments.First();
			if (idDoc.Citizenship == null)
			{
				_errorList.Add("У абитуриента должно быть указано гражданство.");
				return false;
			}
			else
			{
				return true;
			}
		}

		bool CheckEducationDocumentExisting()
		{
			if (_claim.SchoolCertificateDocuments.Count == 0 
					&& _claim.MiddleEducationDiplomaDocuments.Count == 0 
					&& _claim.HighEducationDiplomaDocuments.Count == 0)
			{
				_errorList.Add("У абитуриента должен быть указан хотя бы один документ об образовании.");
				return false;
			}
			else
			{
				return true;
			}
		}

		bool CheckExamResultsExisting()
		{
			if (_claim.EgeDocuments.Count == 0 && _claim.EntranceTestResults.Count == 0)
			{
				_errorList.Add("У абитуриента должны быть указаны результаты ЕГЭ или он должен быть записан на внутренние вступительные испытания.");
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}
