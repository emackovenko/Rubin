using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu
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
        [DbFieldInfo("FRM")]
        public string Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DbFieldInfo("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// Наименование в родительном падеже
        /// </summary>
        [DbFieldInfo("NAME_R")]
        public string GenitiveName { get; set; }
    }
}
