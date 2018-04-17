using System;
using System.Collections.Generic;
using System.Text;

namespace Data
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

        /// <summary>
        /// Дисциплины компетенции
        /// </summary>
        public List<Discipline> Disciplines { get; set; }

        public EducationCompetence()
        {
            Disciplines = new List<Discipline>();
        }
    }
}
