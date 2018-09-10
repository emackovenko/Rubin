using System;
using System.Collections.Generic;
using System.Text;

namespace Data.University
{
    /// <summary>
    /// Физическое лицо
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronimyc { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Идентификатор пола
        /// </summary>
        public int? GenderId { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Идентификатор адреса регистрации
        /// </summary>
        public int? AddressId { get; set; }

        /// <summary>
        /// Адрес регистрации
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Документы, удостоверяющие личность
        /// </summary>
        public List<IdentityDocument> IdentityDocuments { get; set; }

        public Person()
        {
            IdentityDocuments = new List<IdentityDocument>();
        }
    }
}
