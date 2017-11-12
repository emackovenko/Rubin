using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Приказ о переводе на следующий курс
    /// </summary>
    public class NextCourseTransferOrder: StudentOrderBase
    {
        public NextCourseTransferOrder()
            : base ()
        {
            OrderTypeId = "0030";
            Comment = "Переведен с # курса на #";
        }

        /// <summary>
        /// Старый курс
        /// </summary>
        [DbFieldInfo("OLDKURS", DbFieldType.Integer)]
        public int? OldCourse { get; set; }

        /// <summary>
        /// Новый курс
        /// </summary>
        [DbFieldInfo("NEWKURS", DbFieldType.Integer)]
        public int? NewCourse { get; set; }

    }
}
