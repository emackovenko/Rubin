using System;
using System.Collections.Generic;
using System.Text;

namespace Data.University
{
    /// <summary>
    /// Тип образовательного стандарта (ГОС, ФГОС, ФГОС-3+ и т.д.)
    /// </summary>
    public class EducationStandartType
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
    }
}
