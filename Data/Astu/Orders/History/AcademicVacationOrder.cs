using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders.History
{
    /// <summary>
    /// Приказ о предоставлении академического отпуска
    /// </summary>
    public class AcademicVacationOrder: StudentOrderBase
    {
        public AcademicVacationOrder()
        {
            OrderTypeId = "0002";
            Comment = "Предоставить академический отпуск с ";
        }

        /// <summary>
        /// Дата начала отпуска
        /// </summary>
        [DbFieldInfo("DAT_START", DbFieldType.DateTime)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания отпуска
        /// </summary>
        [DbFieldInfo("DAT_END", DbFieldType.DateTime)]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Идентификатор причины отпуска
        /// </summary>
        [DbFieldInfo("PAO")]
        
        public string ReasonId { get; set; }

        /// <summary>
        /// Причина академического отпуска
        /// </summary>
        public AcademicVacationReason Reason
        {
            get => Astu.AcademicVacationReasons.FirstOrDefault(avr => avr.Id == ReasonId);
            set => ReasonId = value?.Id;
        }

    }
}
