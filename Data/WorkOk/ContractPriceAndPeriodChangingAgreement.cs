using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.WorkOk
{
    public class ContractPriceAndPeriodChangingAgreement: PriceChangingAgreement
    {
        public ContractPriceAndPeriodChangingAgreement()
            : base()
        {
            AgreementTypeId = 11;
        }
        
        [DbFieldInfo("srokd", DbFieldType.DateTime)]
        public DateTime? NewContractDate { get; set; }
    }
}
