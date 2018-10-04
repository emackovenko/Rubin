using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.WorkOk
{
    [TableName("platn.PaymentDictionaries")]
    public class PaymentDictionary: Entity
    {
        [PrimaryKey]
        [DbFieldInfo("Id", DbFieldType.Integer)]
        public int Id { get; set; }

        [DbFieldInfo("DirectionId", DbFieldType.Integer)]
        public int DirectionId { get; set; }

        public Direction Direction
        {
            get => Context.Directions.FirstOrDefault(e => e.Id == DirectionId);
            set => DirectionId = value.Id;
        }

        [DbFieldInfo("EducationFormId", DbFieldType.Integer)]
        public int EducationFormId { get; set; }

        public EducationForm EducationForm
        {
            get => Context.EducationForms.FirstOrDefault(e => e.Id == EducationFormId);
            set => EducationFormId = value.Id;
        }

        [DbFieldInfo("Course", DbFieldType.Integer)]
        public int Course { get; set; }

        [DbFieldInfo("Price", DbFieldType.Double)]
        public float Price { get; set; }
    }
}
