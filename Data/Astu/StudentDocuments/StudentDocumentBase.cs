using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu
{
    /// <summary>
    /// Документ студента
    /// </summary>
    [TableName("ANKETA_DOCUM")]
    public abstract class StudentDocumentBase: Entity
    {
        public StudentDocumentBase()
            : base ()
        {
            IsArchival = false;
            PropertyChanged += StudentDocumentBase_PropertyChanged;
        }

        private void StudentDocumentBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DocumentTypeId")
            {
                Name = DocumentType.Name;                
            }
        }

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
        [DbFieldInfo("DOC_NUM")]
        public string Number { get; set; }

        /// <summary>
        /// Дата выдачи
        /// </summary>
        [DbFieldInfo("DOC_DATE", DbFieldType.DateTime)]
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
        [DbFieldInfo("ARH", DbFieldType.Boolean)]
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
                StudentId = value?.Id;
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
                DocumentTypeId = value?.Id;
                Name = value?.Name;
            }
        }

        /// <summary>
        /// Государство
        /// </summary>
        public Citizenship Citizenship
        {
            get
            {
                return Astu.Citizenships.FirstOrDefault(c => c.Id == CitizenshipId);
            }
            set
            {
                CitizenshipId = value?.Id;
            }
        }


    }
}
