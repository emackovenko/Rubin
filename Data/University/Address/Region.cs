using System;
using System.Collections.Generic;
using System.Text;

namespace Data.University
{
    /// <summary>
    /// Регион
    /// </summary>
    public class Region
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
        /// Идентификатор страны
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// Страна
        /// </summary>
        public Country Country { get; set; }


        /// <summary>
        /// Города региона
        /// </summary>
        public List<Town> Towns { get; set; }

        /// <summary>
        /// Районы региона
        /// </summary>
        public List<District> Districts { get; set; }

        public Region()
        {
            Towns = new List<Town>();
            Districts = new List<District>();
        }
    }
}
