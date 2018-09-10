using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.WorkOk
{
    [TableName("acckadr")]
    public class StudentInfo: Entity
    {
        [PrimaryKey]
        [DbFieldInfo("Id", DbFieldType.Integer)]
        public int Id { get; set; }

        [DbFieldInfo("acc", DbFieldType.Integer)]
        public int StudentId { get; set; }

        public Student Student
        {
            get => Context.Students.FirstOrDefault(s => s.Id == StudentId);
            set => StudentId = value.Id;
        }

        [DbFieldInfo("spcitiz", DbFieldType.Integer)]
        public int? CitizenshipId { get; set; }

        public Citizenship Citizenship
        {
            get => Context.Citizenships.FirstOrDefault(c => c.Id == CitizenshipId);
            set => CitizenshipId = value?.Id;
        }

        [DbFieldInfo("dat_ro", DbFieldType.DateTime)]
        public DateTime? BirthDate { get; set; }

        [DbFieldInfo("mes_ro", DbFieldType.String)]
        public string BirthPlace { get; set; }

        [DbFieldInfo("pasnum", DbFieldType.String)]
        public string IdentityDocumentProps { get; set; }

        [DbFieldInfo("paswho", DbFieldType.String)]
        public string IdentityDocumentOrganization { get; set; }

        [DbFieldInfo("pasdate", DbFieldType.DateTime)]
        public DateTime? IdentityDocumentDate { get; set; }

        [DbFieldInfo("spksiva", DbFieldType.Integer)]
        public int? IdentityDocumentTypeId { get; set; }

        public IdentityDocumentType IdentityDocumentType
        {
            get => Context.IdentityDocumentTypes.FirstOrDefault(idt => idt.Id == IdentityDocumentTypeId);
            set => IdentityDocumentTypeId = value?.Id;
        }

        [DbFieldInfo("address", DbFieldType.String)]
        public string Address { get; set; }

        [DbFieldInfo("phone", DbFieldType.String)]
        public string PhoneNumber { get; set; }



    }
}
