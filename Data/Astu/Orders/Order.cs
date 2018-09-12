using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders
{
    /// <summary>
    /// Приказ по движению контингента студентов
    /// </summary>
    [TableName("PRIKAZ")]
    public class Order: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [DbFieldInfo("ID_PRI")]
        public string Id { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        [DbFieldInfo("NOM")]
        public string Number { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        [DbFieldInfo("DAT", DbFieldType.DateTime)]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        [DbFieldInfo("KOMM")]
        public string Comment { get; set; }

        /// <summary>
        /// Идентификатор типа приказа
        /// </summary>
        [DbFieldInfo("TPR")]
        public string OrderTypeId { get; set; }

        /// <summary>
        /// Тип приказа
        /// </summary>
        public OrderType OrderType
        {
            get => Astu.OrderTypes.FirstOrDefault(e => e.Id == OrderTypeId);
            set => OrderTypeId = value?.Id;
        }

        /// <summary>
        /// Флаг - приказ подписан
        /// </summary>
        [DbFieldInfo("PRU", DbFieldType.Boolean)]
        public bool IsSigned { get; set; }

        /// <summary>
        /// Флаг - приказ в архиве
        /// </summary>
        [DbFieldInfo("ARH", DbFieldType.Boolean)]
        public bool IsArhival { get; set; }

        /// <summary>
        /// Идентификатор факультета
        /// </summary>
        [DbFieldInfo("FAK")]
        public string FacultyId { get; set; }

        /// <summary>
        /// Факультет
        /// </summary>
        public Faculty Faculty
        {
            get => Astu.Faculties.FirstOrDefault(e => e.Id == FacultyId);
            set => FacultyId = value?.Id;
        }


        /// <summary>
        /// Элементы приказа
        /// </summary>
        public IEnumerable<OrderItem> Items
        {
            get
            {
                var list = new List<OrderItem>();
                list.AddRange(Astu.AcademicVacationExitItems.Where(o => o.StudentId == Id));
                list.AddRange(Astu.AcademicVacationItems.Where(o => o.StudentId == Id));
                list.AddRange(Astu.ChildrenFussVacationExitItems.Where(o => o.StudentId == Id));
                list.AddRange(Astu.ChildrenFussVacationItems.Where(o => o.StudentId == Id));
                list.AddRange(Astu.DirectionChangingItems.Where(o => o.StudentId == Id));
                list.AddRange(Astu.EnrollmentByUniversityTransferItems.Where(o => o.StudentId == Id));
                list.AddRange(Astu.EnrollmentItems.Where(o => o.StudentId == Id));
                list.AddRange(Astu.EnrollToFullStateProvisionItems.Where(o => o.StudentId == Id));
                list.AddRange(Astu.FinanceSourceChangingItems.Where(o => o.StudentId == Id));
                list.AddRange(Astu.GraduationItems.Where(o => o.StudentId == Id));
                list.AddRange(Astu.GroupTransferItems.Where(o => o.StudentId == Id));
                list.AddRange(Astu.StudentNameChangingItems.Where(o => o.StudentId == Id));
                list.AddRange(Astu.OtherItems.Where(o => o.StudentId == Id));
                list.AddRange(Astu.ReinstatementItems.Where(o => o.StudentId == Id));
                list.AddRange(Astu.TransferToAcceleratedEducationItems.Where(o => o.StudentId == Id));
                list.AddRange(Astu.UnenrollmentItems.Where(o => o.StudentId == Id));
                return list;
            }
        }

    }
}
