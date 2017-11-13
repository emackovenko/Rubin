using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Приказ о зачислении переводом из другого ВУЗа
    /// </summary>
    public class EnrollmentByUniversityTransferOrder: StudentOrderBase
    {
        public EnrollmentByUniversityTransferOrder()
            : base ()
        {
            OrderTypeId = "0015";
            Comment = "Зачислен переводом из ";
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

        /// <summary>
        /// Дата начала обучения
        /// </summary>
        [DbFieldInfo("DAT_START", DbFieldType.DateTime)]
        public DateTime? StartDate { get; set; }
    }
}
