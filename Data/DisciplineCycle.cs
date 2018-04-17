using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    /// <summary>
    /// Цикл учебных дисциплин
    /// </summary>
    public class DisciplineCycle
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
        /// Дисциплины цикла
        /// </summary>
        public List<Discipline> Disciplines { get; set; }

        public DisciplineCycle()
        {
            Disciplines = new List<Discipline>();
        }
    }
}
