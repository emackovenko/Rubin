using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders.History
{
    public class OtherOrder: StudentOrderBase
    {
        public OtherOrder() : base ()
        {
            OrderTypeId = "0009";
        }


        /// <summary>
        /// Дата начала обучения
        /// </summary>
        [DbFieldInfo("DAT_START", DbFieldType.DateTime)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Курс, на который зачислен студент
        /// </summary>
        [DbFieldInfo("NEWKURS", DbFieldType.Integer)]
        public int NewCourse { get; set; }


        /// <summary>
        /// Идентификатор группы, куда зачислен студент
        /// </summary>        
        [DbFieldInfo("GRP")]
        public string GroupId { get; set; }


        /// <summary>
        /// Группа, в которую зачислен студент
        /// </summary>
        public Group Group
        {
            get => Astu.Groups.FirstOrDefault(g => g.Id == GroupId);
            set => GroupId = value?.Id;
        }
    }
}
