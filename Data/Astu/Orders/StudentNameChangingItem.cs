using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders
{
    /// <summary>
    /// Приказ о смене фамилии
    /// </summary>
    public class StudentNameChangingItem: OrderItem
    {
        public StudentNameChangingItem() : base ()
        {
            OrderTypeId = "0013";
            Comment = "Смена фамилии в связи с вступлением в брак";
        }
        
        /// <summary>
        /// Дата начала обучения
        /// </summary>
        [DbFieldInfo("DAT_START", DbFieldType.DateTime)]
        public DateTime? StartDate { get; set; }
        
        /// <summary>
        /// Новые Ф.И.О.
        /// </summary>
        [DbFieldInfo("NEW_FIO")]
        public string NewFullName { get; set; }
    }
}
