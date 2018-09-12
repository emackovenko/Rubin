using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.University
{
    /// <summary>
    /// Адрес
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Идентификатор страны
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// Страна
        /// </summary>
        public Country Country { get; set; }


        /// <summary>
        /// Идентификатор региона
        /// </summary>
        public int? RegionId { get; set; }

        /// <summary>
        /// Регион
        /// </summary>
        public Region Region { get; set; }


        /// <summary>
        /// Идентификатор района
        /// </summary>
        public int? DisctrictId { get; set; }

        /// <summary>
        /// Район
        /// </summary>
        public District District { get; set; }

        /// <summary>
        /// Идентификатор населенного пункта
        /// </summary>
        public int? LocalityId { get; set; }

        /// <summary>
        /// Населенный пункт
        /// </summary>
        public Locality Locality { get; set; }

        /// <summary>
        /// Идентификатор города
        /// </summary>
        public int? TownId { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public Town Town { get; set; }

        /// <summary>
        /// Идентификатор улицы
        /// </summary>
        public int? StreetId { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        public Street Street { get; set; }

        /// <summary>
        /// Номер строения
        /// </summary>
        public string Bulding { get; set; }

        /// <summary>
        /// Номер квартиры
        /// </summary>
        public string Apartment { get; set; }

        /// <summary>
        /// Строка адреса в визуально простом формате
        /// </summary>
        [NotMapped]
        public string SimpleAddress { get; }

        /// <summary>
        /// Строка адреса в международном стандартизированном формате
        /// </summary>
        [NotMapped]
        public string StandartAddress { get; }
    }
}
