using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders
{
    /// <summary>
    /// Приказ о зачислении студента на полное гос. обеспечение
    /// </summary>
    public class EnrollToFullStateProvisionItem: OrderItem
    {
        public EnrollToFullStateProvisionItem() : base ()
        {
            OrderTypeId = "0017";
            Comment = "Зачислен на полное государственное обеспечение с ";
        }

        /// <summary>
        /// Дата начала обучения
        /// </summary>
        [DbFieldInfo("DAT_START", DbFieldType.DateTime)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания обучения
        /// </summary>
        [DbFieldInfo("DAT_END", DbFieldType.DateTime)]
        public DateTime? EndDate { get; set; }
    }
}
