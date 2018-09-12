using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.WorkOk
{
    [TableName("moves")]
    public class Order : Entity
    {
        [PrimaryKey]
        [DbFieldInfo("pin", DbFieldType.Integer)]
        public int Id { get; set; }

        [DbFieldInfo("mvnum")]
        public string Number { get; set; }

        [DbFieldInfo("mvdate", DbFieldType.DateTime)]
        public DateTime? Date { get; set; }

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

        [DbFieldInfo("spevent", DbFieldType.Integer)]
        public int? OrderTypeId { get; set; }

        public OrderType OrderType
        {
            get => Context.OrderTypes.FirstOrDefault(e => e.Id == OrderTypeId);
            set => OrderTypeId = value?.Id;
        }

        [DbFieldInfo("acc")]
        public int? StudentId { get; set; }

        [DbFieldInfo("mvfakt", DbFieldType.DateTime)]
        public DateTime? FactDate { get; set; }

        [DbFieldInfo("mvosn")]
        public string Comment { get; set; }

        [DbFieldInfo("kurs")]
        public int Course { get; set; }

        public Student Student
        {
            get => Context.Students.FirstOrDefault(e => e.Id == StudentId);
            set => StudentId = value?.Id;
        }

        Order GetPreviouslyOrder()
        {
            var list = Student.Orders.OrderBy(o => o.Date).ToList();
            int currentIndex = list.FindIndex(o => o.Id == Id);
            if (currentIndex != 0)
            {
                return list[currentIndex - 1];
            }
            else
            {
                return null;
            }
        }

    }
}
