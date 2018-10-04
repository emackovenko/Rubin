using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders
{
    /// <summary>
    /// Приказ о переводе на ускоренное обучение по ИУП
    /// </summary>
    public class TransferToAcceleratedEducationItem: OrderItem
    {

        public TransferToAcceleratedEducationItem() : base ()
        {
            OrderTypeId = "0034";
            Comment = "Переведён на ускоренное обучение по ИУП";
        }
        
    }
}
