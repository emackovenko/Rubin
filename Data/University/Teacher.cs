using System;
using System.Collections.Generic;
using System.Text;

namespace Data.University
{
    /// <summary>
    /// Преподаватель
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор физического лица
        /// </summary>
        public int? PersonId { get; set; }

        /// <summary>
        /// Физическое лицо
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Идентификатор кафедры
        /// </summary>
        public int? CathedraId { get; set; }

        /// <summary>
        /// Кафедра
        /// </summary>
        public Cathedra Cathedra { get; set; }
    }
}
