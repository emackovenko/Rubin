using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu
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
        [DbFieldInfo("FAK")]
        public string Id { get; set; }
        
        /// <summary>
        /// Название
        /// </summary>
        [DbFieldInfo("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// Краткое название
        /// </summary>
        [DbFieldInfo("KN")]
        public string ShortName { get; set; }

        /// <summary>
        /// И.О. Фамилия декана
        /// </summary>
        [DbFieldInfo("DECAN")]
        public string DeanName { get; set; }

        /// <summary>
        /// Полное название факультета в родительном падеже
        /// </summary>
        [DbFieldInfo("NAME_R")]
        public string GenitiveName { get; set; }
    }
}
