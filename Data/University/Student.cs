using System;
using System.Collections.Generic;
using System.Text;

namespace Data.University
{
    /// <summary>
    /// Студент
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Номер личного дела
        /// </summary>
        public string PersonFileNumber { get; set; }

        /// <summary>
        /// Идентификатор физического лица
        /// </summary>
        public int? PersonId { get; set; }

        /// <summary>
        /// Физическое лицо
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Идентификатор группы
        /// </summary>
        public int? GroupId { get; set; }

        /// <summary>
        /// Группа
        /// </summary>
        public Group Group { get; set; }

        /// <summary>
        /// Идентификатор статуса
        /// </summary>
        public int? StudentStateId { get; set; }

        /// <summary>
        /// Статус студента
        /// </summary>
        public StudentState State { get; set; }
        
        /// <summary>
        /// Идентификатор категории обучения
        /// </summary>
        public int? FinanceSourceId { get; set; }

        /// <summary>
        /// Категория обучения
        /// </summary>
        public FinanceSource FinanceSource { get; set; }
    }
}
