using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.WorkOk
{
    public class ContragentChangingAgreement: AuxAgreement
    {
        public ContragentChangingAgreement()
        {
            AgreementTypeId = 13;
        }

        [DbFieldInfo("OldContragentId", DbFieldType.Integer)]
        public int? OldContragentId { get; set; }

        public Contragent OldContragent
        {
            get
            {
                return (Context.OrganizationContragents.FirstOrDefault(a => a.Id == OldContragentId) as Contragent) ??
                    (Context.PersonContragents.FirstOrDefault(a => a.Id == OldContragentId) as Contragent);
            }
            set
            {
                OldContragentId = value?.Id;
            }
        }

        [DbFieldInfo("NewContragentId", DbFieldType.Integer)]
        public int? NewContragentId { get; set; }

        public Contragent NewContragent
        {
            get
            {
                return (Context.OrganizationContragents.FirstOrDefault(a => a.Id == NewContragentId) as Contragent) ??
                    (Context.PersonContragents.FirstOrDefault(a => a.Id == NewContragentId) as Contragent);
            }
            set
            {
                NewContragentId = value?.Id;
            }
        }

    }
}
