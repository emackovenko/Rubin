using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.WorkOk
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class TableNameAttribute: Attribute
	{
		public string Value
		{
			get; set;
		}

		public TableNameAttribute(string tableName)
		{
			Value = tableName;
		}
	}
}
