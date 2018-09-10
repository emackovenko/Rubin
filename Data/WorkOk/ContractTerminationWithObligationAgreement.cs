using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.WorkOk
{
    public class ContractTerminationWithObligationAgreement: ContractTerminationAgreement
    {
        public ContractTerminationWithObligationAgreement()
            : base()
        {
            AgreementTypeId = 12;
        }

        [DbFieldInfo("ObligationSum", DbFieldType.Double)]
        public double ObligationSum { get; set; }
    }
}
