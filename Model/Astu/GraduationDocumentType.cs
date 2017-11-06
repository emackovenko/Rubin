using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    [TableName("adm.VDOSPR")]
    public class GraduationDocumentType: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [FieldName("VDO")]
        [FieldType(DatabaseFieldType.String)]
        public string Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [FieldName("VDO_NAME")]
        [FieldType(DatabaseFieldType.String)]
        public string Name { get; set; }
    }
}
