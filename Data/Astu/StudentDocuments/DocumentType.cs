using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu
{
    /// <summary>
    /// Тип документа
    /// </summary>
    [TableName("adm.DOCUMENTTYPE")]
    public class DocumentType: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [DbFieldInfo("ID_DOCUMENTTYPE", DbFieldType.Integer)]
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DbFieldInfo("NAME_DOCUMENTTYPE")]
        public string Name { get; set; }
    }
}
