using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.WorkOk
{
    public class PriceChangingAgreement: AuxAgreement
    {
        public PriceChangingAgreement()
        {
            AgreementTypeId = 8;
        }

        [DbFieldInfo("SimplePrice", DbFieldType.Double)]
        public float SimplePrice { get; set; }

        [DbFieldInfo("FullPrice", DbFieldType.Double)]
        public float FullPrice { get; set; }

        [DbFieldInfo("IndividualFullPrice", DbFieldType.Double)]
        public float IndividualFullPrice { get; set; }
    }
}
