using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.ExportData.Orders
{
	[Serializable]
	public class Orders
	{
		public List<OrderOfAdmission> OrdersOfAdmission { get; set; }
		public List<OrderOfException> OrdersOfException { get; set; }
		public List<Application> Applications { get; set; }
	}
}
