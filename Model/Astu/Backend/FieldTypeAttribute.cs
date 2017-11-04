using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FieldTypeAttribute: Attribute
    {
        public DatabaseFieldType Value { get; set; }

        public FieldTypeAttribute(DatabaseFieldType databaseFieldType)
        {
            Value = databaseFieldType;
        }
    }
}
