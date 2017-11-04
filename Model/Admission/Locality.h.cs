using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Admission
{
	public partial class Locality
	{
		public override string ToString()
		{
			return string.Format("{0} {1}", Name, Prefix);
		}
	}
}
