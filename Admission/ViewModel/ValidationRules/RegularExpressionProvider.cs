using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Admission.ViewModel.ValidationRules
{
	public static class RegularExpressionProvider
	{
		/// <summary>
		/// Электронная почта
		/// </summary>
		public static Regex Email
		{
			get
			{
				return new Regex(@"([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}",
					RegexOptions.Compiled | RegexOptions.IgnoreCase);
			}
		}

		/// <summary>
		/// Шаблон для именования на русском языке
		/// </summary>
		public static Regex OnlyRusLetterString
		{
			get
			{
				return new Regex(@"([А-ЯЁ])+([а-яё`'-]{0,})", RegexOptions.Compiled);
			}
		}

		/// <summary>
		/// Логин
		/// </summary>
		public static Regex Username
		{
			get
			{
				return new Regex(@"(([A-z]{1,})+([0-9]{0,}))\w", RegexOptions.Compiled);
			}
		}

		/// <summary>
		/// Пароль
		/// </summary>
		public static Regex Password
		{
			get
			{
				return new Regex(@"([A-z0-9]\w)", RegexOptions.Compiled);
			}
		}

		/// <summary>
		/// Только цифры от 0 до 9
		/// </summary>
		public static Regex OnlyDigits
		{
			get
			{
				return new Regex(@"([0-9])\w", RegexOptions.Compiled);
			}
		}

		/// <summary>
		/// Русский текст
		/// </summary>
		public static Regex RussianText
		{
			get
			{
				return new Regex(@"([А-яё .()\-+,!№@\\|&`'<>\[\]\{\}#$^;:%?*_/0-9\t\n\r])", RegexOptions.Compiled);
			}
		}
	}
}
