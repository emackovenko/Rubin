using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Admission.View.Converters
{
	[ValueConversion(typeof(DateTime), typeof(string))]
	public class DateConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value != null)
			{
				return ((DateTime)value).ToString("dd.MM.yyyy");
			}
			else
			{
				return "Нет даты";
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			DateTime date;
			if (DateTime.TryParse((string)value, out date))
			{
				return date;
			}
			else
			{
				return null;
			}
		}
	}
}
