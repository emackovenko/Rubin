using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders
{
    /// <summary>
    /// Элемент приказа
    /// </summary>
    [TableName("ST_PRIK")]
    public abstract class OrderItem: Entity
    {

        /// <summary>
        /// Идентификатор
        /// </summary>
        [DbFieldInfo("ID_ST_PRI")]
        public string Id { get; set; }

        /// <summary>
        /// Идентификатор приказа
        /// </summary>
        [DbFieldInfo("ID_PRI")]
        public string OrderId { get; set; }

        /// <summary>
        /// Приказ
        /// </summary>
        public Order Order
        {
            get => Astu.Orders.FirstOrDefault(e => e.Id == OrderId);
            set => OrderId = value?.Id;
        }

        /// <summary>
        /// Идентификатор студента
        /// </summary>
        [DbFieldInfo("KOD")]
        public string StudentId { get; set; }

        /// <summary>
        /// Студент
        /// </summary>
        public Student Student
        {
            get => Astu.Students.FirstOrDefault(e => e.Id == StudentId);
            set => StudentId = value?.Id;
        }

        /// <summary>
        /// Комментарий
        /// </summary>
        [DbFieldInfo("KOMM")]
        public string Comment { get; set; }

        /// <summary>
        /// Идентификатор типа приказа
        /// </summary>
        [DbFieldInfo("TPR")]
        public string OrderTypeId { get; set; }
        
        /// <summary>
        /// Тип приказа
        /// </summary>
        public OrderType OrderType
        {
            get => Astu.OrderTypes.FirstOrDefault(e => e.Id == OrderTypeId);
            set => OrderTypeId = value?.Id;
        }



    }
}
