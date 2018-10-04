using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu
{
    /// <summary>
    /// Документ, удостоверяющий личность
    /// </summary>
    public class IdentityDocument: StudentDocumentBase
    {
        public IdentityDocument()
            : base ()
        {
            DocumentTypeId = 1;
            Name = "Документ, удостоверяющий личность";
        }

        /// <summary>
        /// Идентификатор типа документа, удостоверяющего личность
        /// </summary>
        [DbFieldInfo("ID_IDENTYDOCUMENTTYPE", DbFieldType.Integer)]
        
        public int? IdentityDocumentTypeId { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [DbFieldInfo("FIRSTNAME")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [DbFieldInfo("LASTNAME")]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [DbFieldInfo("MIDDLENAME")]
        public string Patronimyc { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        [DbFieldInfo("POL")]
        public string Gender { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [DbFieldInfo("DATR", DbFieldType.DateTime)]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Место рождения
        /// </summary>
        [DbFieldInfo("DOCUM_MESTOROJ")]
        public string BirthPlace { get; set; }

        /// <summary>
        /// Тип документа, удостоверяющего личность
        /// </summary>
        public IdentityDocumentType IdentityDocumentType
        {
            get
            {
                return Astu.IdentityDocumentTypes.FirstOrDefault(idt => idt.Id == IdentityDocumentTypeId);
            }
            set
            {
                IdentityDocumentTypeId = value?.Id;
            }
        }

    }
}
