using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.WorkOk
{
    public class PersonContragent: Contragent
    {
        public PersonContragent()
            : base()
        {
            ContragentType = 1;
        }

        [DbFieldInfo("phone")]
        public string PhoneNumber { get; set; }
        
        [DbFieldInfo("datro", DbFieldType.DateTime)]
        public DateTime? BirthDate { get; set; }

        [DbFieldInfo("pas_ser", DbFieldType.String)]
        public string IdentityDocumentSeries { get; set; }

        [DbFieldInfo("pas_num", DbFieldType.String)]
        public string IdentityDocumentNumber { get; set; }

        [DbFieldInfo("paswho", DbFieldType.String)]
        public string IdentityDocumentOrganization { get; set; }

        [DbFieldInfo("pasdate", DbFieldType.DateTime)]
        public DateTime? IdentityDocumentDate { get; set; }
    }
}
