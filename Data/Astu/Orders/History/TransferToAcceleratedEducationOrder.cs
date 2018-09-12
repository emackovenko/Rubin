using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders.History
{
    /// <summary>
    /// Приказ о переводе на ускоренное обучение по ИУП
    /// </summary>
    public class TransferToAcceleratedEducationOrder: StudentOrderBase
    {

        public TransferToAcceleratedEducationOrder() : base ()
        {
            OrderTypeId = "0034";
            Comment = "Переведён на ускоренное обучение по ИУП";
        }
        
    }
}
