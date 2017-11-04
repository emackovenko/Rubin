using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Admission
{
	public partial class Direction
	{
		public override string ToString()
		{
			return string.Format("{0} {1}", Code, Name);
		}
	}
}
