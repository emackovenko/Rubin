using CommonMethods.TypeExtensions.exDateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using A = Model.Astu.Orders.History;

namespace Model.WorkOk
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

        public A.StudentOrderBase ToAstu()
        {
            int[] enrollment = new int[] { 1, 2, 3 };
            if (enrollment.Contains(OrderTypeId.Value))
            {
                return AstuEnrollmentOrder();
            }

            int[] enrollmentByTransfer = new int[] { 64, 65 };
            if (enrollmentByTransfer.Contains(OrderTypeId.Value))
            {
                return AstuEnrollmentByTransferOrder();
            }

            int[] reinstatement = new int[] { 10, 11, 12, 13, 25 };
            if (reinstatement.Contains(OrderTypeId.Value))
            {
                return AstuReinstatementOrder();
            }


            return null;
        }



        A.EnrollmentOrder AstuEnrollmentOrder()
        {
            var order = new A.EnrollmentOrder();
            order.Number = Number;
            order.Date = Date;

            string comment = string.Empty;
            comment += OrderType?.Name;
            if (FactDate.HasValue)
            {
                comment += string.Format(" с {0} г.", FactDate.Format());
            }
            order.Comment = comment;

            order.FacultyId = Faculty.AstuId;
            order.StartDate = FactDate;
            order.NewCourse = Course;
            order.IsFromAdmission = true;
            order.FinanseSourceId = OrderType.FinanceSource.AstuId;
            order.DirectionId = Direction?.AstuId;
            order.EducationFormId = EducationForm.AstuId;

            if (Group?.AstuId == null)
            {
                throw new Exception(string.Format("Группе {0} ({1}) не сопоставлена группа в асту", Group?.Name, Group?.Id));
            }
            order.GroupId = Group.AstuId;

            return order;
        }

        A.EnrollmentByUniversityTransferOrder AstuEnrollmentByTransferOrder()
        {
            var order = new A.EnrollmentByUniversityTransferOrder();
            order.Number = Number;
            order.Date = Date;

            string comment = string.Empty;
            comment += OrderType.Name;
            if (FactDate.HasValue)
            {
                comment += string.Format(" с {0} г.", FactDate.Format());
            }
            if (!string.IsNullOrWhiteSpace(Comment))
            {
                comment += " " + Comment;
            }
            order.Comment = comment;

            order.FacultyId = Faculty.AstuId;
            order.StartDate = FactDate;
            order.NewCourse = Course;
            order.IsFromAdmission = true;
            order.FinanseSourceId = OrderType.FinanceSource.AstuId;
            order.DirectionId = Direction.AstuId;
            order.EducationFormId = EducationForm.AstuId;

            if (Group?.AstuId == null)
            {
                throw new Exception(string.Format("Группе {0} ({1}) не сопоставлена группа в асту", Group?.Name, Group?.Id));
            }
            order.GroupId = Group.AstuId;

            return order;
        }

        A.ReinstatementOrder AstuReinstatementOrder()
        {
            var order = new A.ReinstatementOrder();
            order.Number = Number;
            order.Date = Date;

            string comment = string.Empty;
            comment += OrderType.Name;
            if (FactDate.HasValue)
            {
                comment += string.Format(" с {0} г.", FactDate.Format());
            }
            order.Comment = comment;

            order.FacultyId = Faculty.AstuId;
            order.StartDate = FactDate;
            order.NewCourse = Course;
            order.FinanseSourceId = OrderType.FinanceSource.AstuId;
            order.DirectionId = Direction?.AstuId;
            order.EducationFormId = EducationForm.AstuId;

            if (Group?.AstuId == null)
            {
                throw new Exception(string.Format("Группе {0} ({1}) не сопоставлена группа в асту", Group?.Name, Group?.Id));
            }
            order.GroupId = Group.AstuId;

            return order;
        }

        A.AcademicVacationExitOrder AstuAcademicVacationExitOrder()
        {
            var order = new A.AcademicVacationExitOrder();
            order.Number = Number;
            order.Date = Date;

            string comment = string.Empty;
            comment += OrderType.Name;
            if (FactDate.HasValue)
            {
                comment += string.Format(" с {0} г.", FactDate.Format());
            }
            order.Comment = comment;

            order.FacultyId = Faculty.AstuId;
            order.StartDate = FactDate;
            order.NewCourse = Course;
            order.FinanseSourceId = OrderType.FinanceSource.AstuId;
            order.DirectionId = Direction?.AstuId;
            order.EducationFormId = EducationForm.AstuId;

            if (Group?.AstuId == null)
            {
                throw new Exception(string.Format("Группе {0} ({1}) не сопоставлена группа в асту", Group?.Name, Group?.Id));
            }
            order.GroupId = Group.AstuId;

            return order;
        }

        A.ChildrenFussVacationExitOrder AstuChildrenFussVacationExitOrder()
        {
            var order = new A.ChildrenFussVacationExitOrder();
            order.Number = Number;
            order.Date = Date;

            string comment = string.Empty;
            comment += OrderType.Name;
            if (FactDate.HasValue)
            {
                comment += string.Format(" с {0} г.", FactDate.Format());
            }
            order.Comment = comment;

            order.FacultyId = Faculty.AstuId;
            order.StartDate = FactDate;
            order.NewCourse = Course;
            order.FinanseSourceId = OrderType.FinanceSource.AstuId;
            order.DirectionId = Direction?.AstuId;
            order.EducationFormId = EducationForm.AstuId;

            if (Group?.AstuId == null)
            {
                throw new Exception(string.Format("Группе {0} ({1}) не сопоставлена группа в асту", Group?.Name, Group?.Id));
            }
            order.GroupId = Group.AstuId;

            return order;
        }

        A.ChildrenFussVacationOrder AstuChildrenFussVacationOrder()
        {
            var order = new A.ChildrenFussVacationOrder();
            order.Number = Number;
            order.Date = Date;

            string comment = string.Empty;
            comment += OrderType.Name;
            if (FactDate.HasValue)
            {
                comment += string.Format(" с {0} г.", FactDate.Format());
            }
            order.Comment = comment;

            order.FacultyId = Faculty.AstuId;
            order.StartDate = FactDate;
            order.OrderTypeId = OrderType.AstuId;

            return order;
        }
        
        A.DirectionChangingOrder AstuDirectionChangingOrder()
        {
            var order = new A.DirectionChangingOrder();
            order.Number = Number;
            order.Date = Date;

            string comment = string.Empty;
            comment += OrderType.Name;
            if (FactDate.HasValue)
            {
                comment += string.Format(" с {0} г.", FactDate.Format());
            }
            order.Comment = comment;

            order.FacultyId = Faculty.AstuId;
            order.StartDate = FactDate;
            order.OrderTypeId = OrderType.AstuId;

            return order;
        }

    }
}
