using System;

namespace Model.ExportData
{
    /// <summary>
    /// Диплом о среднем профессиональном образовании
    /// </summary>
    [Serializable]
    public class MiddleEduDiplomaDocument
    {
        #region Поля
        /// <summary>
        /// Идентификатор в ИС ОО
        /// </summary>
        public string UID { get; set; }
            
        /// <summary>
        /// Дата предоставления оригинала документа
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
        /// дата выдачи документа
        /// </summary>
        public string DocumentDate { get; set; }

        /// <summary>
        /// Организация, выдавшая документ
        /// </summary>
        public string DocumentOrganization { get; set; }

        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public string RegistrationNumber { get; set; }		

        /// <summary>
        /// Год окончания
        /// </summary>
        public int EndYear { get; set; }

        /// <summary>
        /// Средний балл
        /// </summary>
        public double GPA { get; set; }
        #endregion
    }
}
