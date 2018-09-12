using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Astu
{		  
	public enum EntityState
	{
		Default,
		New,
		Changed,
		Deleted
	}	
    
    public enum DbFieldType
    {
        String,
        Integer,
        Double,
        DateTime,
        Boolean
    }
}
