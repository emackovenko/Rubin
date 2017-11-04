using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.ExportData
{
	/// <summary>
	/// Блок авторизации
	/// </summary>
	[Serializable]
	public class AuthData
	{
		/// <summary>
		/// Пароль
		/// </summary>
		public string Login { get; set; }

		/// <summary>
		/// Пароль
		/// </summary>
		public string Pass { get; set; }

		public string InstitutionID { get; set; }

	}
}
