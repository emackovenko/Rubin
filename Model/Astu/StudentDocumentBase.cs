using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Документ студента
    /// </summary>
    [TableName("ANKETA_DOCUM")]
    public abstract class StudentDocumentBase: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [DbFieldInfo("ID_ANKETA_DOCUM")]
        public string Id { get; set; }

        /// <summary>
        /// Идентификатор студента
        /// </summary>
        [DbFieldInfo("KOD")]
        public string StudentId { get; set; }

        /// <summary>
        /// Идентификатор типа документа
        /// </summary>
        [DbFieldInfo("ID_DOCUMENTTYPE", DbFieldType.Integer)]
        public int? DocumentTypeId { get; set; }

        /// <summary>
        /// Серия
        /// </summary>
        [DbFieldInfo("DOC_SERIES")]
        public string Series { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        [DbFieldInfo("DOC_NUMBER")]
        public string Number { get; set; }

        /// <summary>
        /// Дата выдачи
        /// </summary>
        [DbFieldInfo("DOC_DATE")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Кем выдан
        /// </summary>
        [DbFieldInfo("DOC_ORG")]
        public string Organization { get; set; }

        /// <summary>
        /// Название документа
        /// </summary>
        [DbFieldInfo("DOC_NAME")]
        public string Name { get; set; }

        /// <summary>
        /// Флаг - документ в архиве
        /// </summary>
        [DbFieldInfo("ARH")]
        public bool IsArchival { get; set; }

        /// <summary>
        /// Идентификатор гражданства или государства, выдавшего документ
        /// </summary>
        [DbFieldInfo("GOS")]
        public string CitizenshipId { get; set; }

        /// <summary>
        /// Студент
        /// </summary>
        public Student Student
        {
            get
            {
                return Astu.Students.FirstOrDefault(s => s.Id == StudentId);
            }
            set
            {
                if (value != null)
                {
                    StudentId = value.Id;
                }
                else
                {
                    StudentId = null;
                }
            }
        }

        /// <summary>
        /// Тип документа
        /// </summary>
        public DocumentType DocumentType
        {
            get
            {
                return Astu.DocumentTypes.FirstOrDefault(dt => dt.Id == DocumentTypeId);
            }
            set
            {
                if (value != null)
                {
                    DocumentTypeId = value.Id;
                    Name = value.Name;
                }
                else
                {
                    DocumentTypeId = null;
                }
            }
        }


    }
}
