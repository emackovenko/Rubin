using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Тип приказа
    /// </summary>
    [TableName("TPRSPR")]
    public class OrderType: Entity
    {

        /// <summary>
        /// Идентификатор
        /// </summary>
        [DbFieldInfo("TPR")]
        public string Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DbFieldInfo("NAME")]
        public string Name { get; set; }
    }
}
