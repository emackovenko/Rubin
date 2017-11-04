using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Изучаемый иностранный язык
    /// </summary>
    [TableName("LNGSPR")]
    public class ForeignLanguage: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [FieldName("LNG")]
        [FieldType(DatabaseFieldType.String)]
        public string Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [FieldName("NAME")]
        [FieldType(DatabaseFieldType.String)]
        public string Name { get; set; }
    }
}
