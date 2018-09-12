using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders.History
{
    /// <summary>
    /// Приказ о смене фамилии
    /// </summary>
    public class StudentNameChangingOrder: StudentOrderBase
    {
        public StudentNameChangingOrder() : base ()
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
        /// Старые Ф.И.О.
        /// </summary>
        [DbFieldInfo("OLD_FIO")]
        public string OldFullName { get; set; }
        
        /// <summary>
        /// Новые Ф.И.О.
        /// </summary>
        public string NewFullName { get; set; }
    }
}
