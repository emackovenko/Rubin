using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders
{
    /// <summary>
    /// Приказ об отчислении
    /// </summary>
    public class UnenrollmentItem: OrderItem
    {
        public UnenrollmentItem()
            : base ()
        {
            OrderTypeId = "0003";
            Comment = "Отчислен ";
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
            get => Astu.UnenrollmentReasons.FirstOrDefault(ur => ur.Id == UnenrollmentReasonId);
            set => UnenrollmentReasonId = value?.Id;
        }
    }
}
