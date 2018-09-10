using System;
using System.Collections.Generic;
using System.Text;

namespace Data.University
{
    /// <summary>
    /// Образовательная компетенция
    /// </summary>
    public class EducationCompetence
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Код
        /// </summary>
        public string Code { get; set; }
    }
}
