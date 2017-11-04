using System;

namespace Model.ExportData
{
    /// <summary>
    /// Димплом о высшем профессиональном образовании
    /// </summary>
    [Serializable]
    public class HighEduDiplomaDocument
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
