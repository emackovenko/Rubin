using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Приказ по студенту (приказ в карточке студента)
    /// </summary>
    [TableName("ANK_HIST")]
    public abstract class StudentOrderBase: Entity
    {
        public StudentOrderBase()
            : base ()
        {
            Date = DateTime.Now;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [DbFieldInfo("ID_HIST")]
        public string Id { get; set; }

        /// <summary>
        /// Идентификатор студента
        /// </summary>
        [DbFieldInfo("KOD")]
        public string StudentId { get; set; }

        /// <summary>
        /// Комментарий к приказу
        /// </summary>
        [DbFieldInfo("KOMM")]
        public string Comment { get; set; }

        /// <summary>
        /// Номер приказа
        /// </summary>
        [DbFieldInfo("NOM")]
        public string Number { get; set; }

        /// <summary>
        /// Дата приказа
        /// </summary>
        [DbFieldInfo("DAT", DbFieldType.DateTime)]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Идентификатор типа приказа
        /// </summary>
        [DbFieldInfo("TPR")]
        public string OrderTypeId { get; set; }

        

        /// <summary>
        /// Идентификатор факультета
        /// </summary>
        [DbFieldInfo("FAK")]
        public string FacultyId { get; set; }

        /// <summary>
        /// Студент
        /// </summary>
        public Student Student
        {
            get
            {
                return Astu.Students.FirstOrDefault(s => s.Id == StudentId);
            }
            set
            {
                if (value != null)
                {
                    StudentId = value.Id;
                }
                else
                {
                    StudentId = null;
                }
            }
        }

        /// <summary>
        /// Тип приказа
        /// </summary>
        public OrderType OrderType
        {
            get
            {
                return Astu.OrderTypes.FirstOrDefault(ot => ot.Id == OrderTypeId);
            }
            set
            {
                if (value != null)
                {
                    OrderTypeId = value.Id;
                }
                else
                {
                    OrderTypeId = null;
                }
            }
        }

        /// <summary>
        /// Факультет
        /// </summary>
        public Faculty Faculty
        {
            get
            {
                return Astu.Faculties.FirstOrDefault(f => f.Id == FacultyId);
            }
            set
            {
                if (value != null)
                {
                    FacultyId = value.Id;
                }
                else
                {
                    FacultyId = null;
                }
            }
        }

    }
}
