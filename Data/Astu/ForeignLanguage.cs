using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu
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
        [DbFieldInfo("LNG")]
        public string Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DbFieldInfo("NAME")]
        public string Name { get; set; }
    }
}
