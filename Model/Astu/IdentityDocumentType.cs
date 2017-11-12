using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Тип документа, удостоверяющего личность
    /// </summary>
    [TableName("adm.IDENTITYDOCUMENTTYPE")]
    public class IdentityDocumentType: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [DbFieldInfo("ID_IDENTITYDOCUMENTTYPE", DbFieldType.Integer)]
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DbFieldInfo("NAME_IDENTITYDOCUMENTTYPE")]
        public string Name { get; set; }
    }
}
