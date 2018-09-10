using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.WorkOk
{
    [TableName("platn.otsrochka")]
    public class PaymentDelay: Entity
    {
        [PrimaryKey]
        [DbFieldInfo("pin", DbFieldType.Integer)]
        public int Id { get; set; }

        [DbFieldInfo("acc")]
        public int StudentId { get; set; }

        public Student Student
        {
            get => Context.Students.FirstOrDefault(e => e.Id == StudentId);
            set => StudentId = value.Id;
        }

        [DbFieldInfo("datbeg", DbFieldType.DateTime)]
        public DateTime? BeginDate { get; set; }

        [DbFieldInfo("datend", DbFieldType.DateTime)]
        public DateTime? EndDate { get; set; }

        [DbFieldInfo("sum", DbFieldType.Double)]
        public float Sum { get; set; }

        [DbFieldInfo("prim")]
        public string Comment { get; set; }



        public override string ToString()
        {
            return string.Format("{0} руб. до {1} г.", Sum, EndDate?.ToString("dd.MM.yyyy"));
        }


    }
}
