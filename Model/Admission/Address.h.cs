using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Model.Admission
{
    public partial class Address
    {
        [NotMapped]
        public string Index
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Street.MailIndex))
                {
                    return Street.MailIndex;
                }
                else
                {
                    if (TownId != null)
                    {
                        if (!string.IsNullOrWhiteSpace(Town.MailIndex))
                        {
                            return Town.MailIndex;
                        }
                        else if (!string.IsNullOrWhiteSpace(Region.MailIndex))
                        {
                            return Region.MailIndex;
                        }
                        else return string.Empty;
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(Locality.MailIndex))
                        {
                            return Locality.MailIndex;
                        }
                        else if (!string.IsNullOrWhiteSpace(District.MailIndex))
                        {
                            return District.MailIndex;
                        }
                        else if (!string.IsNullOrWhiteSpace(Region.MailIndex))
                        {
                            return Region.MailIndex;
                        }
                        else return string.Empty;
                    }
                }
            }
        }

        public string GetAstuAddress(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException();
            }

            string strForReturn = "";
            switch (address.CountryId)
            {
                case 1: strForReturn += "643,"; break;
                case 3: strForReturn += "398,"; break;
                case 15: strForReturn += "762,"; break;
            }

            strForReturn += Index + ',';

            if (address.CountryId==1)
            {
                strForReturn += RegionId + ',';
            }
            else
            {
                strForReturn += Region.Name + ',';
            }

            strForReturn += ScrutchAddress;

            return strForReturn;
        }

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
