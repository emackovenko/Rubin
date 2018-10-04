using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.WorkOk
{
    public class ContractPeriodChangingAgreement: AuxAgreement
    {
        public ContractPeriodChangingAgreement()
            : base()
        {
            AgreementTypeId = 9;
        }

        [DbFieldInfo("srokd", DbFieldType.DateTime)]
        public DateTime? NewContractDate { get; set; }
    }
}
