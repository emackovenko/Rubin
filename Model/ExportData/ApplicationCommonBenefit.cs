using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.ExportData
{
    [Serializable]
    public class ApplicationCommonBenefit
    {
        public string UID { get; set; }

        public string CompetitiveGroupUID { get; set; }

        public string DocumentTypeID { get; set; }

        public string BenefitKindID { get; set; }

        public DocumentReason DocumentReason { get; set; }

    }
}
