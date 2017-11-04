using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonMethods.TypeExtensions.exNumeric
{
	public static class IntExtensions
	{
		/// <summary>
		/// Возвращает строку "столько-то баллов"
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string ScoreString(this int value)
		{
			return RusCurrency.Str((double)value, "SCORE");
		}
	}
}
