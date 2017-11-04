using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Admission
{
	public partial class EntrantContract
	{
		public string PayerName
		{
			get
			{
				if (ContragentPerson != null)
				{
					return ContragentPerson.FullName;
				}
				if (ContragentOrganization != null)
				{
					return ContragentOrganization.Name;
				}
				return "Не указано ни одного контрагента";
			}
		}
	}
}
