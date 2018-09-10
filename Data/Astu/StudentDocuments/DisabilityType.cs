using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu
{
    /// <summary>
    /// Группа инвалидности
    /// </summary>
    [TableName("adm.DISABILITYTYPE")]
    public class DisabilityType: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [DbFieldInfo("ID_DISABILITYTYPE", DbFieldType.Integer)]
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DbFieldInfo("NAME_DISABILITYTYPE")]
        public string Name { get; set; }
    }
}
