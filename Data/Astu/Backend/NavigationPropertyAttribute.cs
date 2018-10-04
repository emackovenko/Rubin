using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Astu
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NavigationPropertyAttribute: Attribute
    {
        public Type NavigationType { get; set; }

        public NavigationPropertyAttribute(Type navigationType)
        {
            NavigationType = navigationType;
        }
    }
}
