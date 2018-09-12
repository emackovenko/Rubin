using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders.History
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
        /// Идентификатор элемента приказа
        /// </summary>
        [DbFieldInfo("ID_ST_PRI")]
        public string OrderItemId { get; set; }



        /// <summary>
        /// Идентификатор приказа
        /// </summary>
        [DbFieldInfo("ID_PRI")]
        public string OrderId { get; set; }

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
                StudentId = value?.Id;
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
                OrderTypeId = value?.Id;
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
                FacultyId = value?.Id;
            }
        }


        public string Reinstate(StudentOrderBase order)
        {
            string changed = string.Empty;

            // Получить дочерние свойства
            string[] uncheckedProps = new string[]
            {
                "EntityState",
                "OrderItemId",
                "OrderId",
                "Id",
                "StudentId",
                "OrderTypeId",
                "FacultyId"
            };
            var childProps = GetType().GetProperties().Where(pi => !uncheckedProps.Contains(pi.Name)).
                Where(pi => pi.GetCustomAttributes(typeof(DbFieldInfoAttribute), true).Count() > 0);

            // по каждому свойству
            foreach (var pi in childProps)
            {
                object selfValue = pi.GetValue(this, null);
                object workValue = pi.GetValue(order, null);
                if (workValue != null)
                {
                    if ((!selfValue?.Equals(workValue)) ?? true)
                    {
                        pi.SetValue(this, workValue, null);
                        changed += string.Format("\t\t\tСвойство {0} было изменено с «{1}» на «{2}»;\n",
                            pi.Name, selfValue?.ToString(), workValue?.ToString());
                    }
                }
            }
                        
            return changed;
        }
    }
}
