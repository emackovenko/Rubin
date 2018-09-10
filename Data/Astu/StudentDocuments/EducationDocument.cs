using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu
{
    public class EducationDocument: StudentDocumentBase
    {
        public EducationDocument()
            : base ()
        {
            PropertyChanged += EducationDocument_PropertyChanged;
        }

        /// <summary>
        /// Идентификатор типа документа об образовании
        /// </summary>
        [DbFieldInfo("VDO")]
        
        public string EducationDocumentTypeId { get; set; }

        /// <summary>
        /// Год окончания учебного заведения
        /// </summary>
        [DbFieldInfo("ENDYEAR", DbFieldType.Integer)]
        public int GraduationYear { get; set; }

        /// <summary>
        /// Тип документа об образовании
        /// </summary>
        public EducationDocumentType EducationDocumentType
        {
            get
            {
                return Astu.EducationDocumentTypes.FirstOrDefault(edt => edt.Id == EducationDocumentTypeId);
            }
            set
            {
                EducationDocumentTypeId = value?.Id;
            }
        }

        /// <summary>
        /// Доступные типы документов
        /// </summary>
        public IEnumerable<DocumentType> AbailableDocumentTypes
        {
            get
            {
                int[] eduIds = new int[] { 3, 4, 5 };
                return Astu.DocumentTypes.Where(dt => eduIds.Contains(dt.Id));
            }
        }

        /// <summary>
        /// Реагирует на изменение свойств и проставляет значения зависимым от них свойствам.
        /// </summary>
        private void EducationDocument_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Date")
            {
                if (Date.HasValue)
                {
                    GraduationYear = Date.Value.Year;
                }
            }
            else
            {
                if (e.PropertyName == "EducationDocumentTypeId")
                {
                    if (EducationDocumentType != null)
                    {
                        Name = EducationDocumentType.Name;
                    }
                }
            }
        }
    }
}
