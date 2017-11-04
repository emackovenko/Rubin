using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Astu
{		  
	public enum EntityState
	{
		Default,
		New,
		Changed,
		Deleted
	}	
    
    public enum DatabaseFieldType
    {
        String,
        Integer,
        Double,
        DateTime,
        Boolean,
        OracleDateTime
    }
}
