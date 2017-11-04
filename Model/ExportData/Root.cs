using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.ExportData
{
	[Serializable]
	public class Root
	{
		public AuthData AuthData { get; set; }

		public PackageData PackageData { get; set; }

		public Orders Orders { get; set; }
	}
}
