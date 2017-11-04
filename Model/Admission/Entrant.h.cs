using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Admission
{
	public partial class Entrant
	{
		public string FullName
		{
			get
			{
				return string.Format("{0} {1} {2}", LastName, 
					FirstName, Patronymic);
			}
		}
	}
}
