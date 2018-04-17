using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    /// <summary>
    /// Учебная дисциплина
    /// </summary>
    public class Discipline
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
        /// Идентификатор кафедры
        /// </summary>
        public int? CathedraId { get; set; }

        /// <summary>
        /// Кафедра
        /// </summary>
        public Cathedra Cathedra { get; set; }

        /// <summary>
        /// Идентификатор цикла
        /// </summary>
        public int? DisciplineCycleId { get; set; }

        /// <summary>
        /// Цикл
        /// </summary>
        public DisciplineCycle DisciplineCycle { get; set; }

        /// <summary>
        /// Идентификатор компетенции
        /// </summary>
        public int? EducationCompetenceId { get; set; }

        /// <summary>
        /// Компетенция
        /// </summary>
        public EducationCompetence EducationCompetence { get; set; }
    }
}
