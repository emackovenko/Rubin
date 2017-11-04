using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Astu
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class FieldNameAttribute: Attribute
	{
		public string Value
		{
			get; set;
		}

		public FieldNameAttribute(string fieldName)
		{
			Value = fieldName;
		}
		
	}
}
