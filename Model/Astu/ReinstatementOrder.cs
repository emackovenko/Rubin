using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    public class ReinstatementOrder: StudentOrderBase
    {
        public ReinstatementOrder()
            : base ()
        {
            OrderTypeId = "0006";
            Comment = "Восстановлен на ";
        }

        /// <summary>
        /// Дата начала обучения
        /// </summary>
        [DbFieldInfo("DAT_START", DbFieldType.DateTime)]
        public DateTime? StartDate { get; set; }
    }
}
