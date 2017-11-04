using System;

namespace Model.ExportData
{
    /// <summary>
    /// Иной документ об образовании
    /// </summary>
    [Serializable]
    public class EduCustomDocument
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
        public int DocumentSeries { get; set; }

        //Номер документа
        public int DocumentNumber { get; set; }

        /// <summary>
        /// Дата выдачи документа
        /// </summary>
        public string DocumentDate { get; set; }

        /// <summary>
        /// Организация, выдавшая документ
        /// </summary>
        public string DocumentOrganization { get; set; }

        /// <summary>
        /// Наименование документа
        /// </summary>
        public string DocumentTypeNameText { get; set; }
        #endregion
    }
}
