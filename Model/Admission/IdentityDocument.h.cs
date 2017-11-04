using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Admission
{
	public partial class IdentityDocument
	{		   
		public IdentityDocument()
		{
			OriginalReceivedDate = DateTime.Now;
			Date = DateTime.Now;
			BirthDate = DateTime.Now;
		}

		public bool IsOriginal
		{
			get
			{
				return OriginalReceivedDate != null;
			}
			set
			{

			}
		}
	}
}
