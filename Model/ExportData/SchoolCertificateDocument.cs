using System;
using System.Collections.Generic;

namespace Model.ExportData
{
    /// <summary>
    /// Аттестат о среднем (полном) общем образовании
    /// </summary>
    [Serializable]
    public class SchoolCertificateDocument
    {
        #region Поля
        /// <summary>
        /// Идентификатор в ИС ОО
        /// </summary>
        public string UID { get; set; }
        
        /// <summary>
        /// Дата предоставления оригиналов документов
        /// </summary>
        public string OriginalReceivedDate { get; set; }

        /// <summary>
        /// Серия документа
        /// </summary>
        public string DocumentSeries { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата выдачи документа
        /// </summary>
        public string DocumentDate { get; set; }

        /// <summary>
        /// Организация, выдавшая документ
        /// </summary>
        public string DocumentOrganization { get; set; }

        /// <summary>
        /// Год окончания
        /// </summary>
        public int EndYear { get; set; }

        /// <summary>
        /// Средний балл
        /// </summary>
        public double GPA { get; set; }

        /// <summary>
        /// Баллы по предметам
        /// </summary>
        public List<SubjectData> Subjects { get; set; }

        #endregion
    }
}
