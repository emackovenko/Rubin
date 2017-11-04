using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Admission.ViewModel.ValidationRules
{
	public class NotEmptyOnlyRusLetterString: ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if (value == null || ((string)value).Length == 0)
			{
				return new ValidationResult(false, "Поле должно быть заполнено");
			}

			if (!RegularExpressionProvider.OnlyRusLetterString.IsMatch((string)value))
			{
				return new ValidationResult(false, "Значение поля не соответствует формату.");
			}	 

			return ValidationResult.ValidResult;
		}
	}
}
