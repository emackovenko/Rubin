using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu
{
    /// <summary>
    /// Источник финансирования (категория обучения студента)
    /// </summary>
    [TableName("KOBSPR")]
    public class FinanceSource: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [DbFieldInfo("KOB")]
        public string Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DbFieldInfo("NAME")]
        public string Name { get; set; }
    }
}
