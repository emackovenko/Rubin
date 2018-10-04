using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders.History
{
    /// <summary>
    /// Приказ об окончании обучения
    /// </summary>
    public class GraduationOrder: StudentOrderBase
    {
        public GraduationOrder() : base ()
        {
            OrderTypeId = "0005";
            Comment = "Отчислен в связи с получением образования";
            UnenrollmentReasonId = "0042";
        }

        /// <summary>
        /// Дата окончания обучения
        /// </summary>
        [DbFieldInfo("DAT_END", DbFieldType.DateTime)]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Идентификатор причины отчисления
        /// </summary>
        [DbFieldInfo("POT")]
        
        public string UnenrollmentReasonId { get; set; }

        /// <summary>
        /// Причина отчисления
        /// </summary>
        public UnenrollmentReason UnenrollmentReason
        {
            get
            {
                return Astu.UnenrollmentReasons.FirstOrDefault(ur => ur.Id == UnenrollmentReasonId);
            }
            set
            {
                UnenrollmentReasonId = value?.Id;
            }
        }
    }
}
