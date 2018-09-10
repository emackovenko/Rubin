using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.WorkOk
{
    [TableName("spevent")]
    public class OrderType: Entity
    {
        [PrimaryKey]
        [DbFieldInfo("pin", DbFieldType.Integer)]
        public int Id { get; set; }

        [DbFieldInfo("ViewName")]
        public string Name { get; set; }

        [DbFieldInfo("ASTU_Code")]
        public string AstuId { get; set; }

        [DbFieldInfo("spstatus")]
        public int? FinanceSourceId { get; set; }

        [DbFieldInfo("Reason_Code")]
        public string AstuReasonId { get; set; }

        public FinanceSource FinanceSource
        {
            get => Context.FinanceSources.FirstOrDefault(e => e.Id == FinanceSourceId);
            set => FinanceSourceId = value?.Id;
        }
    }
}
