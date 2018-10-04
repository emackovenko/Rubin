using System;
using System.Collections.Generic;
using System.Text;

namespace Data.University
{
    /// <summary>
    /// Улица
    /// </summary>
    public class Street
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
        /// Префикс
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Код объекта в БД ФИАС
        /// </summary>
        public string FiasCode { get; set; }

        /// <summary>
        /// Код объекта в БД КЛАДР
        /// </summary>
        public string KladrCode { get; set; }

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Уровень родительского объекта (3 - город, 4 - населенный пункт)
        /// </summary>
        public int ParentLevel { get; set; }

        /// <summary>
        /// Идентификатор родительского объекта
        /// </summary>
        public int? ParentId { get; set; }
        
    }
}
