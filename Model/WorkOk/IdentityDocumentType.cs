using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.WorkOk
{
    [TableName("spksiva")]
    public class IdentityDocumentType: Entity
    {
        [PrimaryKey]
        [DbFieldInfo("pin", DbFieldType.Integer)]
        public int Id { get; set; }

        [DbFieldInfo("fis_code", DbFieldType.Integer)]
        public int? FisCode { get; set; }
    }
}
