using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    public class AcademicVacationExitOrder: StudentOrderBase
    {
        public AcademicVacationExitOrder()
            : base ()
        {
            OrderTypeId = "0007";
            Comment = "Выход из академического отпуска с ";
        }

        /// <summary>
        /// Дата начала обучения
        /// </summary>
        [DbFieldInfo("DAT_START", DbFieldType.DateTime)]
        public DateTime? StartDate { get; set; }
    }
}
