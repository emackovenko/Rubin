using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu
{
    /// <summary>
    /// Причина отчисления
    /// </summary>
    [TableName("POTSPR")]
    public class UnenrollmentReason: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [DbFieldInfo("POT")]
        public string Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DbFieldInfo("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// Архивный
        /// </summary>
        [DbFieldInfo("ARH", DbFieldType.Boolean )]
        public bool IsArchival { get; set; }
    }
}
