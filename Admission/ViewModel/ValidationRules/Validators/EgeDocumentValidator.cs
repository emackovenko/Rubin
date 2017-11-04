using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;

namespace Admission.ViewModel.ValidationRules.Validators
{
	public class EgeDocumentValidator : IEntityValidator
	{

		public EgeDocumentValidator(Claim claim)
		{
			_claim = claim;
		}

		Claim _claim;

		public bool IsValid
		{
			get
			{
				return Check();
			}
		}

		List<string> _errorList = new List<string>();

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

		bool Check()
		{
			_errorList.Clear();
			return CheckDocuments();
		}

		bool CheckDocuments()
		{
			bool result = true;

			foreach (var doc in _claim.EgeDocuments)
			{
				if (doc.Year == null)
				{
					_errorList.Add("У свидетельства о сдаче ЕГЭ не указан год.");
					result = false;
				}
				if (doc.EgeResults.Count == 0)
				{
					_errorList.Add("У свидетельства о сдаче ЕГЭ не указаны результаты.");
					result = false;
				}

				foreach (var res in doc.EgeResults)
				{
					CheckResult(res);
				}
			}

			return result;
		}

		bool CheckResult(EgeResult egeResult)
		{
			var _res = egeResult;

			if (_res.ExamSubject == null)
			{
				_errorList.Add("Не указана экзаменационная дисциплина.");
				return false;
			}
								 
			if (_res.Value == null || _res.Value <= 0 || _res.Value > 100)
			{
				_errorList.Add(string.Format("По предмету \"{0}\" выставлен некорректный балл.", _res.ExamSubject.Name));
				return false;
			}

			return true;
		}

	}
}
