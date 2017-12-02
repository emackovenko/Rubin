using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.WorkOk
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PrimaryKeyAttribute: Attribute
    {
        public PrimaryKeyAttribute()
        {

        }
    }
}
