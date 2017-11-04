using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonMethods.TypeExtensions.exNumeric;

namespace CommonMethods.TypeExtensions.exString
{
    public static class StringExensions
    {
        /// <summary>
        /// Возвращает строку, в которой первая буква заглавная, а все остальные строчные
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string BeginWithUpper(this string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
		}

		/// <summary>
		/// Возвращает копию этой строки, в которой все буквы удалены
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string WithoutLetters(this string str)
		{
			string originalStr = str;
			string resultStr = string.Empty;
			foreach (var c in originalStr)
			{
				if (Char.IsDigit(c))
				{
					resultStr += c;
				}
			}
			return resultStr;
		}

		/// <summary>
		/// Возвращает копию этой строки, в которой все цифры удалены
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string WithoutDigits(this string str)
		{
			string originalStr = str;
			string resultStr = string.Empty;
			foreach (var c in originalStr)
			{
				if (!Char.IsDigit(c))
				{
					resultStr += c;
				}
			}
			return resultStr;
		}

		/// <summary>
		/// Возвращает строковое представление числа как период времени в формате [кол-во лет кол-во месяцев]
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string AsPeriod(this string value)
		{
			var str = string.Empty;

			// разбираем на составляющие
			var periodParts = value.Split('.');

			// по целой части бьем по падежам
			str += string.Format("{0} {1}",
				periodParts[0], RusNumber.Case(int.Parse(periodParts[0]), "год", "года", "лет"));

			// по дробной части бьем по падежам, если таковая имеется
			if (periodParts.Length > 1)
			{
				str += string.Format(" {0} {1}",
					periodParts[1], RusNumber.Case(int.Parse(periodParts[1]), "месяц", "месяца", "месяцев"));
			}

			return str;
		}
	}
}
