using System;
using System.Collections.Generic;
using System.Text;

namespace Data.University
{
    /// <summary>
    /// Документ, удостоверяющий личность
    /// </summary>
    public class IdentityDocument
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Серия
        /// </summary>
        public string Series { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime?  Date { get; set; }

        /// <summary>
        /// Идентификатор организации, выдавшей документ
        /// </summary>
        public int? OrganizationId { get; set; }

        /// <summary>
        /// Организация, выдавшая документ
        /// </summary>
        public IdentityOrganization Organization { get; set; }

        /// <summary>
        /// Идентификатор типа документа, удостоверяющего личность
        /// </summary>
        public int? TypeId { get; set; }

        /// <summary>
        /// Тип документа, удостоверяющего личность
        /// </summary>
        public IdentityType Type { get; set; }

        /// <summary>
        /// Идентификатор гражданства
        /// </summary>
        public int? CitizenshipId { get; set; }

        /// <summary>
        /// Гражданство
        /// </summary>
        public Citizenship Citizenship { get; set; }

        /// <summary>
        /// Идентификатор владельца документа
        /// </summary>
        public int? PersonId { get; set; }

        /// <summary>
        /// Владелец докмента
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Фамилия в документе
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя в документе
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество в документе
        /// </summary>
        public string Patronimyc { get; set; }
        
        /// <summary>
        /// Флаг - активный документ
        /// </summary>
        public bool? IsActive { get; set; }
    }
}
