using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Факультет
    /// </summary>
    [TableName("FAKSPR")]
    public class Faculty: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [FieldName("FAK")]
        [FieldType(DatabaseFieldType.String)]
        public string Id { get; set; }
        
        /// <summary>
        /// Название
        /// </summary>
        [FieldName("NAME")]
        [FieldType(DatabaseFieldType.String)]
        public string Name { get; set; }

        /// <summary>
        /// Краткое название
        /// </summary>
        [FieldName("KN")]
        [FieldType(DatabaseFieldType.String)]
        public string ShortName { get; set; }

        /// <summary>
        /// И.О. Фамилия декана
        /// </summary>
        [FieldName("DECAN")]
        [FieldType(DatabaseFieldType.String)]
        public string DeanName { get; set; }

        /// <summary>
        /// Полное название факультета в родительном падеже
        /// </summary>
        [FieldName("NAME_R")]
        [FieldType(DatabaseFieldType.String)]
        public string GenitiveName { get; set; }
    }
}
