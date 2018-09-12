using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu
{
    /// <summary>
    /// Категория сиротства
    /// </summary>
    [TableName("adm.ORPHANCATEGORY")]
    public class OrphanCategory: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [DbFieldInfo("ID_ORPHANCATEGORY", DbFieldType.Integer)]
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DbFieldInfo("NAME_ORPHANCATEGORY")]
        public string Name { get; set; }
    }
}
