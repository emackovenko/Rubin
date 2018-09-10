using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu
{
    /// <summary>
    /// Тип документа, удостоверяющего личность
    /// </summary>
    [TableName("adm.IDENTYDOCUMENTTYPE")]
    public class IdentityDocumentType: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [DbFieldInfo("ID_IDENTYDOCUMENTTYPE", DbFieldType.Integer)]
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DbFieldInfo("NAME_IDENTYDOCUMENTTYPE")]
        public string Name { get; set; }
    }
}
