using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Model.Admission;

namespace Admission.View.Theme.Converters
{
	[ValueConversion(typeof(bool), typeof(string))]
	public class IsOriginalToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return "копия";
			}
			return (bool)value ? "оригинал" : "копия"; 
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ((string)value == "оригинал")
			{
				return true;
			}
			return false;
		}
	}
}
