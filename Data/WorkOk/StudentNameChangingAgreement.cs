using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.WorkOk
{
    public class StudentNameChangingAgreement: AuxAgreement
    {
        public StudentNameChangingAgreement()
            : base()
        {
            AgreementTypeId = 7;
        }

        [DbFieldInfo("StudentNameChangingOrderId", DbFieldType.Integer)]
        public int? StudentNameChangingOrderId { get; set; }

        public StudentNameChangingOrder StudentNameChangingOrder
        {
            get => Context.StudentNameChangingOrders.FirstOrDefault(e => e.Id == StudentNameChangingOrderId);
            set => StudentNameChangingOrderId = value?.Id;
        }
    }
}
