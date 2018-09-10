using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu
{
    /// <summary>
    /// Причина академического отпуска
    /// </summary>
    [TableName("PAOSPR")]
    public class AcademicVacationReason: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [DbFieldInfo("PAO")]
        public string Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DbFieldInfo("NAME")]
        public string Name { get; set; }
    }
}
