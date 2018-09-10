using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders.History
{
    /// <summary>
    /// Приказ о зачислении студента на полное гос. обеспечение
    /// </summary>
    public class EnrollToFullStateProvisionOrder: StudentOrderBase
    {
        public EnrollToFullStateProvisionOrder() : base ()
        {
            OrderTypeId = "0017";
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
