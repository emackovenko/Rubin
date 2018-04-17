using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    /// <summary>
    /// Учебный план
    /// </summary>
    public class EducationPlan
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
        /// Год начала подготовки
        /// </summary>
        public int? Year { get; set; }

        /// <summary>
        /// Идентификатор кафедры
        /// </summary>
        public int? CathedraId { get; set; }

        /// <summary>
        /// Кафедра
        /// </summary>
        public Cathedra Cathedra { get; set; }

        /// <summary>
        /// Идентификатор факультета
        /// </summary>
        public int? FacultyId { get; set; }

        /// <summary>
        /// Факультет
        /// </summary>
        public Faculty Faculty { get; set; }

        /// <summary>
        /// Идентификатор формы обучения
        /// </summary>
        public int? EducationFormId { get; set; }

        /// <summary>
        /// Форма обучения
        /// </summary>
        public EducationForm EducationForm { get; set; }

        /// <summary>
        /// Идентификатор направления подготовки (специальности)
        /// </summary>
        public int? DirectionId { get; set; }

        /// <summary>
        /// Направление подготовки (специальность)
        /// </summary>
        public Direction Direction { get; set; }

        /// <summary>
        /// Флаг - ускоренное обучение
        /// </summary>
        public bool? IsAcceleratedLearning { get; set; }
    }
}
