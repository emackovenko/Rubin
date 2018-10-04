using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.WorkOk
{
    [TableName("accfam")]
    public class StudentNameChangingOrder: Entity
    {
        [PrimaryKey]
        [DbFieldInfo("pin", DbFieldType.Integer)]
        public int Id { get; set; }

        [DbFieldInfo("acc", DbFieldType.Integer)]
        public int StudentId { get; set; }

        public Student Student
        {
            get => Context.Students.FirstOrDefault(s => s.Id == StudentId);
            set
            {
                StudentId = value.Id;
            }
        }

        [DbFieldInfo("prnum", DbFieldType.String)]
        public string Number { get; set; }

        [DbFieldInfo("prdate", DbFieldType.DateTime)]
        public DateTime Date { get; set; }

        [DbFieldInfo("fam")]
        public string OldLastName { get; set; }

        [DbFieldInfo("name")]
        public string OldFirstName { get; set; }

        [DbFieldInfo("vname")]
        public string OldPatronimyc { get; set; }
       
        [DbFieldInfo("pasnum", DbFieldType.String)]
        public string OldIdentityDocumentProps { get; set; }

        [DbFieldInfo("paswho", DbFieldType.String)]
        public string OldIdentityDocumentOrganization { get; set; }

        [DbFieldInfo("pasdate", DbFieldType.DateTime)]
        public DateTime? OldIdentityDocumentDate { get; set; }

        public override string ToString()
        {
            return string.Format("№{0} от {1} г.", Number, Date.ToString("dd.MM.yyyy"));
        }

    }
}
