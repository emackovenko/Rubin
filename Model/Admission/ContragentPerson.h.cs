using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Admission
{
	public partial class ContragentPerson
	{
		public string FullName
		{
			get
			{
				return string.Format("{0} {1} {2}",
					LastName, FirstName, Patronymic);
			}
		}

		public string ShortName
		{
			get
			{
				return string.Format("{0} {1}.{2}.",
					LastName, FirstName[0], Patronymic[0]);
			}
		}

		public override string ToString()
		{
			return FullName;
		}
	}
}
