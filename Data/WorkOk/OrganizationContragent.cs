using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.WorkOk
{
    public class OrganizationContragent: Contragent
    {
        public OrganizationContragent()
            : base()
        {
            ContragentType = 2;
        }

        [DbFieldInfo("okpo")]
        public string Okpo { get; set; }

        [DbFieldInfo("inn")]
        public string Inn { get; set; }

        [DbFieldInfo("ls")]
        public string Ls { get; set; }

        [DbFieldInfo("kpp")]
        public string Kpp { get; set; }

        [DbFieldInfo("bik")]
        public string Bik { get; set; }

        [DbFieldInfo("bnk")]
        public string BankName { get; set; }
    }
}
