using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders.History
{
    /// <summary>
    /// Приказ о переводе на следующий курс
    /// </summary>
    public class NextCourseTransferOrder: StudentOrderBase
    {
        public NextCourseTransferOrder()
            : base ()
        {
            OrderTypeId = "0030";
            Comment = "Переведен с # курса на #";
        }

        /// <summary>
        /// Старый курс
        /// </summary>
        [DbFieldInfo("OLDKURS", DbFieldType.Integer)]
        public int? OldCourse { get; set; }

        /// <summary>
        /// Новый курс
        /// </summary>
        [DbFieldInfo("NEWKURS", DbFieldType.Integer)]
        public int? NewCourse { get; set; }

        /// <summary>
        /// Дата начала обучения
        /// </summary>
        [DbFieldInfo("DAT_START", DbFieldType.DateTime)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Перевод условно
        /// </summary>
        public IEnumerable<OrderType> AvailableOrderTypes
        {
            get
            {
                string[] orderTypeIds = new string[] { "0030", "0033" };
                return Astu.OrderTypes.Where(ot => orderTypeIds.Contains(ot.Id)).OrderBy(ot => ot.Name);
            }
        }
        

    }
}
