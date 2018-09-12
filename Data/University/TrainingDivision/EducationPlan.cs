using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.University
{
    /// <summary>
    /// Учебный план
    /// </summary>
    public class EducationPlan
    {
        public EducationPlan()
        {
            Items = new List<EducationPlanItem>();
        }

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
        /// Дата начала обучения
        /// </summary>
        public DateTime? BeginDate { get; set; }

        /// <summary>
        /// Дата окончания обучения
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Идентификатор типа образовательной программы
        /// </summary>
        public int? EducationProgramTypeId { get; set; }

        /// <summary>
        /// Тип образовательной программы
        /// </summary>
        public EducationProgramType EducationProgramType { get; set; }

        /// <summary>
        /// Элементы учебного плана
        /// </summary>
        public List<EducationPlanItem> Items { get; set; }
        
    }
}
