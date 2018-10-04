using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.University
{
    /// <summary>
    /// Направление подготовки (специальность)
    /// </summary>
    public class Direction
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
        /// Краткое наименование
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Код по ГОС/ФГОС/ФГОС-3+
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Идентификатор типа образовательного стандарта
        /// </summary>
        public int? EducationStandartTypeId { get; set; }

        /// <summary>
        /// Тип образовательного стандарта
        /// </summary>
        public EducationStandartType EducationStandartType { get; set; }

        /// <summary>
        /// Идентификатор уровня образования
        /// </summary>
        public int? EducationLevelId { get; set; }

        /// <summary>
        /// Уровень образования
        /// </summary>
        public EducationLevel EducationLevel { get; set; }

        /// <summary>
        /// Идентификатор типа образовательной программы
        /// </summary>
        public int? EducationProgramTypeId { get; set; }

        /// <summary>
        /// Тип образовательной программы
        /// </summary>
        public EducationProgramType EducationProgramType { get; set; }

        [NotMapped]
        public string DisplayName
        {
            get => string.Format("{0} {1}", Code, Name);
        }
    }
}
