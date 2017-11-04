using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Фома обучения
    /// </summary>
    [TableName("FRMSPR")]
    public class EducationForm: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [FieldName("FRM")]
        [FieldType(DatabaseFieldType.String)]
        public string Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [FieldName("NAME")]
        [FieldType(DatabaseFieldType.String)]
        public string Name { get; set; }

        /// <summary>
        /// Наименование в родительном падеже
        /// </summary>
        [FieldName("NAME_R")]
        [FieldType(DatabaseFieldType.String)]
        public string GenitiveName { get; set; }
    }
}
