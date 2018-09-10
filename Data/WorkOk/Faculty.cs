using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.WorkOk
{
    [TableName("spfak")]
    public class Faculty: Entity
    {
        [PrimaryKey]
        [DbFieldInfo("pin", DbFieldType.Integer)]
        public int Id { get; set; }

        [DbFieldInfo("name")]
        public string Name { get; set; }

        [DbFieldInfo("mean")]
        public string ShortName { get; set; }

        [DbFieldInfo("codbar")]
        public string AstuId { get; set; }
    }
}
