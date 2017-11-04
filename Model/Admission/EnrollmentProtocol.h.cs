using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Admission
{
    public partial class EnrollmentProtocol
    {

        public EducationForm EducationForm
        {
            get
            {
                if (CompetitiveGroup != null)
                {
                    return CompetitiveGroup.EducationForm;
                }
                return null;
            }
        }

        public FinanceSource FinanceSource
        {
            get
            {
                if (CompetitiveGroup != null)
                {
                    return CompetitiveGroup.FinanceSource;
                }
                return null;
            }
        }

        public Direction Direction
        {
            get
            {
                if (CompetitiveGroup != null)
                {
                    return CompetitiveGroup.Direction;
                }
                return null;
            }
        }
    }
}
