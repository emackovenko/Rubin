using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.WorkOk
{
    [TableName("platn.currentost")]
    public class PaymentBalance: Entity
    {
        [PrimaryKey]
        [DbFieldInfo("pin", DbFieldType.Integer)]
        public int Id { get; set; }

        [DbFieldInfo("acc", DbFieldType.Integer)]
        public int? StudentId { get; set; }

        public Student Student
        {
            get => Context.Students.FirstOrDefault(e => e.Id == StudentId);
            set => StudentId = value?.Id;
        }

        [DbFieldInfo("summa", DbFieldType.Double)]
        public float Sum { get; set; }
    }
}
