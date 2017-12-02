using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.WorkOk
{
    [TableName("acc")]
    public class Student : Entity
    {
        [PrimaryKey]
        [DbFieldInfo("pin", DbFieldType.Integer)]
        public int Id { get; set; }

        [DbFieldInfo("fam")]
        public string LastName { get; set; }

        [DbFieldInfo("name")]
        public string FirstName { get; set; }

        [DbFieldInfo("vname")]
        public string Patronimyc { get; set; }

        [DbFieldInfo("spgrup", DbFieldType.Integer)]
        public int? GroupId { get; set; }

        public Group Group
        {
            get => Context.Groups.FirstOrDefault(e => e.Id == GroupId);
            set => GroupId = value?.Id;
        }

        [DbFieldInfo("spotd", DbFieldType.Integer)]
        public int? EducationFormId { get; set; }

        public EducationForm EducationForm
        {
            get => Context.EducationForms.FirstOrDefault(e => e.Id == EducationFormId);
            set => EducationFormId = value?.Id;
        }

        [DbFieldInfo("spfak", DbFieldType.Integer)]
        public int? FacultyId { get; set; }

        public Faculty Faculty
        {
            get => Context.Faculties.FirstOrDefault(e => e.Id == FacultyId);
            set => FacultyId = value?.Id;
        }

        [DbFieldInfo("spspec", DbFieldType.Integer)]
        public int? DirectionId { get; set; }

        public Direction Direction
        {
            get => Context.Directions.FirstOrDefault(e => e.Id == DirectionId);
            set => DirectionId = value?.Id;
        }

        [DbFieldInfo("spsost", DbFieldType.Integer)]
        public int? StatusId { get; set; }

        public Status Status
        {
            get => Context.Statuses.FirstOrDefault(e => e.Id == StatusId);
            set => StatusId = value?.Id;
        }

        [DbFieldInfo("godpr", DbFieldType.Integer)]
        public int? AdmissionYear { get; set; }

        [DbFieldInfo("codbar")]
        public string ContextId { get; set; }

        [DbFieldInfo("kurs", DbFieldType.Integer)]
        public int Course { get; set; }

        [DbFieldInfo("spstatus")]
        public int? FinanceSourceId { get; set; }

        public FinanceSource FinanceSource
        {
            get => Context.FinanceSources.FirstOrDefault(e => e.Id == FinanceSourceId);
            set => FinanceSourceId = value?.Id;
        }

        public IEnumerable<Order> Orders { get => Context.Orders.Where(o => o.StudentId == Id); }

        public string FullName { get => string.Format("{0} {1} {2}", LastName, FirstName, Patronimyc); }

        //[DbFieldInfo("BirthDate", DbFieldType.DateTime)]
        //public DateTime? BirthDate { get; set; }

    }
}
