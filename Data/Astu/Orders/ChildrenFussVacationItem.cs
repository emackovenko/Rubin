using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders
{
    public class ChildrenFussVacationItem: OrderItem
    {
        public ChildrenFussVacationItem() : base ()
        {
            Comment = "Предоставить отпуск ";
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
        /// Доступные типы приказов
        /// </summary>
        public IEnumerable<OrderType> AvailableOrderTypes
        {
            get
            {
                string[] orderTypeIds = new string[] { "0022", "0023", "0024", "0027", "0028" };
                return Astu.OrderTypes.Where(ot => orderTypeIds.Contains(ot.Id)).OrderBy(ot => ot.Name);
            }
        }
    }
}
