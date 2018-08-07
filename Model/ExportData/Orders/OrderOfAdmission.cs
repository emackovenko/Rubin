using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.ExportData.Orders
{
	[Serializable]
	public class OrderOfAdmission
	{
		public string OrderOfAdmissionUID { get; set; }
		public string CampaignUID { get; set; }
		public string OrderName { get; set; }
		public string OrderNumber { get; set; }
		public string OrderDate { get; set; }
		public int EducationFormID { get; set; }
		public int FinanceSourceID { get; set; }
		public int EducationLevelID { get; set; }
		public int Stage { get; set; }
	}
}
