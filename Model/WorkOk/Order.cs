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

        public A.StudentOrderBase ToAstu()
        {
            int[] enrollment = new int[] { 1, 2, 3 };
            if (enrollment.Contains(OrderTypeId.Value))
            {
                return GetEnrollmentOrder();
            }

            int[] enrollmentByTransfer = new int[] { 64, 65 };
            if (enrollmentByTransfer.Contains(OrderTypeId.Value))
            {
                return GetEnrollmentByTransferOrder();
            }

            int[] reinstatement = new int[] { 10, 11, 12, 13, 25 };
            if (reinstatement.Contains(OrderTypeId.Value))
            {
                return GetReinstatementOrder();
            }

            int[] academ = new int[] { 19, 35, 36, 52 };
            if (academ.Contains(OrderTypeId.Value))
            {
                return GetAcademicVacationOrder();
            }

            int[] academExit = new int[] { 18, 20 };
            if (academExit.Contains(OrderTypeId.Value))
            {
                return GetAcademicVacationExitOrder();
            }

            int[] childrenFuss = new int[] { 41, 43, 45, 61, 62 };
            if (childrenFuss.Contains(OrderTypeId.Value))
            {
                return GetChildrenFussVacationOrder();
            }

            int[] childrenFussExit = new int[] { 59, 60 };
            if (childrenFussExit.Contains(OrderTypeId.Value))
            {
                return GetChildrenFussVacationExitOrder();
            }

            int[] stateProvision = new int[] { 54 };
            if (stateProvision.Contains(OrderTypeId.Value))
            {
                return GetEnrollToFullStateProvisionOrder();
            }

            int[] facultyTransfer = new int[] { 37, 38 };
            if (facultyTransfer.Contains(OrderTypeId.Value))
            {
                return GetFacultyTransferOrder();
            }

            int[] finSource = new int[] { 15, 16, 17 };
            if (finSource.Contains(OrderTypeId.Value))
            {
                return GetFinanceSourceChangingOrder();
            }

            int[] graduation = new int[] { 5, 67 };
            if (graduation.Contains(OrderTypeId.Value))
            {
                return GetGraduationOrder();
            }

            int[] acceleratedEdu = new int[] { 73, 74 };
            if (acceleratedEdu.Contains(OrderTypeId.Value))
            {
                return GetTransferToAcceleratedEducationOrder();
            }

            int[] unenrollment = new int[] { 4, 5, 6, 7, 8, 9, 27, 28,
                29, 30, 31, 34, 39, 40, 42, 53, 56, 66, 67, 68, 69, 70 };
            if (unenrollment.Contains(OrderTypeId.Value))
            {
                return GetUnenrollmentOrder();
            }

            return null;
        }



        A.EnrollmentOrder GetEnrollmentOrder()
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

        A.EnrollmentByUniversityTransferOrder GetEnrollmentByTransferOrder()
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

        A.ReinstatementOrder GetReinstatementOrder()
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

        A.AcademicVacationOrder GetAcademicVacationOrder()
        {
            var order = new A.AcademicVacationOrder();
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
            order.ReasonId = OrderType.AstuReasonId;

            return order;
        }

        A.AcademicVacationExitOrder GetAcademicVacationExitOrder()
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

        A.ChildrenFussVacationExitOrder GetChildrenFussVacationExitOrder()
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

        A.ChildrenFussVacationOrder GetChildrenFussVacationOrder()
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

        A.DirectionChangingOrder GetDirectionChangingOrder()
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

        A.EnrollToFullStateProvisionOrder GetEnrollToFullStateProvisionOrder()
        {
            var order = new A.EnrollToFullStateProvisionOrder();
            order.Number = Number;
            order.Date = Date;
            order.FacultyId = Faculty.AstuId;
            string comment = OrderType.Name;
            if (FactDate.HasValue)
            {
                comment += string.Format(" с {0} г.", FactDate.Format());
            }
            order.StartDate = FactDate;
            return order;
        }

        A.FacultyTransferOrder GetFacultyTransferOrder()
        {
            var order = new A.FacultyTransferOrder();
            order.Number = Number;
            order.Date = Date;
            order.FacultyId = Faculty.AstuId;
            order.OldFacultyId = GetPreviouslyOrder()?.Faculty.AstuId;
            order.StartDate = FactDate;
            order.NewCourse = Course;
            order.DirectionId = Direction.AstuId;

            if (Group?.AstuId == null)
            {
                throw new Exception(string.Format("Группе {0} ({1}) не сопоставлена группа в асту", Group?.Name, Group?.Id));
            }
            order.GroupId = Group.AstuId;

            order.EducationFormId = EducationForm.AstuId;
            order.Comment = string.Format("Переведен с {0} на {1} на {2} с {3} г.",
                GetPreviouslyOrder().Faculty.Name, Faculty.Name, OrderType.Name, FactDate.Format());
            return order;
        }

        A.FinanceSourceChangingOrder GetFinanceSourceChangingOrder()
        {
            var order = new A.FinanceSourceChangingOrder();
            order.Number = Number;
            order.Date = Date;
            order.FacultyId = Faculty.AstuId;
            order.Comment = string.Format("{0} с {1} г.", OrderType.Name, FactDate.Format());
            order.FinanseSourceId = OrderType.FinanceSource.AstuId;
            return order;
        }

        A.GraduationOrder GetGraduationOrder()
        {
            var order = new A.GraduationOrder();
            order.Number = Number;
            order.Date = Date;
            order.FacultyId = Faculty.AstuId;
            order.EndDate = FactDate;
            order.Comment += string.Format(" с {0} г.", FactDate.Format());
            return order;
        }

        A.GroupTransferOrder GetGroupTransferOrder()
        {
            var order = new A.GroupTransferOrder();
            order.Number = Number;
            order.Date = Date;
            order.FacultyId = Faculty.AstuId;
            order.StartDate = FactDate;
            order.NewCourse = Course;

            if (Group.AstuId == null)
            {
                throw new Exception(string.Format("Группе {0} не сопоставлена группа в асту.", Group.Name));
            }

            order.GroupId = Group.AstuId;
            Comment = string.Format("Переведен в группу {0} с {1} г.", Group.Name, FactDate.Format());
            return order;
        }

        A.OtherOrder GetOtherOrder()
        {
            var order = new A.OtherOrder();
            order.Number = Number;
            order.Date = Date;
            order.FacultyId = Faculty.AstuId;
            order.StartDate = FactDate;
            order.NewCourse = Course;

            if (Group.AstuId == null)
            {
                throw new Exception(string.Format("Группе {0} не сопоставлена группа в асту.", Group.Name));
            }

            order.GroupId = Group.AstuId;
            Comment = OrderType.Name;
            return order;
        }

        A.TransferToAcceleratedEducationOrder GetTransferToAcceleratedEducationOrder()
        {
            var order = new A.TransferToAcceleratedEducationOrder();
            order.Number = Number;
            order.Date = Date;
            order.FacultyId = Faculty.AstuId;
            return order;
        }

        A.UnenrollmentOrder GetUnenrollmentOrder()
        {
            var order = new A.UnenrollmentOrder();
            order.Number = Number;
            order.Date = Date;
            order.FacultyId = Faculty.AstuId;
            order.EndDate = FactDate;
            order.UnenrollmentReasonId = OrderType.AstuReasonId;
            order.Comment = string.Format("{0} с {1} г.", OrderType.Name, FactDate.Format());
            return order;
        }

    }
}
