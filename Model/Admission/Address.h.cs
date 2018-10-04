using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Admission
{
	public partial class Address
	{

        public string ScrutchAddress
        {
            get
            {
                string str = string.Empty;

                if (District != null)
                {
                    str += string.Format(", {0} {1}", District.Name, District.Prefix);
                }

                if (Locality != null)
                {
                    str += string.Format(", {0}. {1}", Locality.Prefix, Locality.Name);
                }

                if (Town != null)
                {
                    str += string.Format(", {0}. {1}", Town.Prefix, Town.Name);
                }

                if (Street != null)
                {
                    str += string.Format(", {0}. {1}", Street.Prefix, Street.Name);
                }

                if (BuildingNumber != null)
                {
                    str += string.Format(", д. {0}", BuildingNumber);
                }

                if (FlatNumber != null)
                {
                    str += string.Format(", кв. {0}", FlatNumber);
                }

                return str;
            }
        }

        public string ViewAddress
		{
			get
			{
				string str = Country.Name;

				if (Region != null)
				{
					str += string.Format(", {0} {1}", Region.Name, Region.Prefix);
				}

				if (District != null)
				{
					str += string.Format(", {0} {1}", District.Name, District.Prefix);
				}

				if (Locality != null)
				{
					str += string.Format(", {0}. {1}", Locality.Prefix, Locality.Name);
				}

				if (Town != null)
				{
					str += string.Format(", {0}. {1}", Town.Prefix, Town.Name);
				}

				if (Street != null)
				{
					str += string.Format(", {0}. {1}", Street.Prefix, Street.Name);
				}

				if (BuildingNumber != null)
				{
					str += string.Format(", д. {0}", BuildingNumber);
				}

				if (FlatNumber != null)
				{
					str += string.Format(", кв. {0}", FlatNumber);
				}	
							
				return str;
			}
		}

		public string MailString
		{
			get
			{
				var context = new AdmissionDatabase();
				var result = context.Database.SqlQuery<string>(string.Format("SELECT GetMailAddress({0})", Id)); 
				return result.FirstOrDefault();
			}
		}
		
	}
}
