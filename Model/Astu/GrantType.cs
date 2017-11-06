using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Вид стипендии студента
    /// </summary>
    [TableName("VSTSPR")]
    public class GrantType: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [FieldName("VST")]
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
