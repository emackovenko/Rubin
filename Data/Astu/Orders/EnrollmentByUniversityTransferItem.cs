using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders
{
    public class EnrollmentByUniversityTransferItem: EnrollmentItem
    {
        public EnrollmentByUniversityTransferItem() : base ()
        {
            OrderTypeId = "0015";
            Comment = "Зачислен в порядке перевода из ";
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
