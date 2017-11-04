using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Admission.ViewModel.ValidationRules
{
	public class Email : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{								
			if (value == null)
			{
				return new ValidationResult(false, "Поле E-mail должно быть заполнено");
			}

			string str = (string)value;
			if (!RegularExpressionProvider.Email.IsMatch(str))
			{
				return new ValidationResult(false, "Введенное значение не соответствует шаблону");
			}

			return ValidationResult.ValidResult;
		}
	}
}
