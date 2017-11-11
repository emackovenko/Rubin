using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Приказ о зачислении из приёмной комиссии
    /// </summary>
    public class AdmissionOrder: StudentOrderBase
    {
        public AdmissionOrder()
            : base ()
        {
            OrderTypeId = "0001";
        }

        /// <summary>
        /// Дата начала обучения
        /// </summary>
        [DbFieldInfo("DAT_START", DbFieldType.DateTime)]
        public DateTime? StartDate { get; set; }

    }
}
