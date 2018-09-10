using System;
using System.Collections.Generic;
using System.Text;

namespace Data.University
{
    /// <summary>
    /// Страна
    /// </summary>
    public class Country
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
        /// Код в ФИАС
        /// </summary>
        public string FiasCode { get; set; }

        /// <summary>
        /// Код в КЛАДР
        /// </summary>
        public string KladrCode { get; set; }

        /// <summary>
        /// Регионы страны
        /// </summary>
        public List<Region> Regions { get; set; }

        public Country()
        {
            Regions = new List<Region>();
        }
    }
}
