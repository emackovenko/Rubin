using System;
using System.Collections.Generic;
using System.Text;

namespace Data.University
{
    /// <summary>
    /// Источник финансирования/Категория обучения
    /// </summary>
    public class FinanceSource
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
        /// Флаг - относится к бюджетным категориям
        /// </summary>
        public bool? IsBudget { get; set; }
    }
}
