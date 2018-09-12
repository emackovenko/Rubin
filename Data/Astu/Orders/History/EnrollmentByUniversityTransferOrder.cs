using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders.History
{
    /// <summary>
    /// Приказ о зачислении переводом из другого ВУЗа
    /// </summary>
    public class EnrollmentByUniversityTransferOrder: EnrollmentOrder
    {
        public EnrollmentByUniversityTransferOrder()
            : base ()
        {
            OrderTypeId = "0015";
            Comment = "Зачислен переводом из ";
            IsFromAdmission = false;
        }

        /// <summary>
        /// Название старого ВУЗа
        /// </summary>
        [DbFieldInfo("OLD_VUZ")]
        public string OldUniversityName { get; set; }

        /// <summary>
        /// Сокращенное название старого ВУЗа
        /// </summary>
        [DbFieldInfo("OLD_KR_VUZ")]
        public string OldUniversityShortName { get; set; }
    }
}
