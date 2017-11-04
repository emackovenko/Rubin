using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Admission
{
	public partial class EgeDocument
	{	 
		public bool IsOriginal
		{
			get
			{
				return OriginalReceivedDate != null;
			}
			set
			{
				if (value)
				{
					OriginalReceivedDate = DateTime.Now;
				}
				else
				{
					OriginalReceivedDate = null;
				}
			}
		}
	}
}
