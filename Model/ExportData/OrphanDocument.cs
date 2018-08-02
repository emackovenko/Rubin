using System;

namespace Model.ExportData
{
    /// <summary>
    /// Документ, подтверждающий сиротство
    /// </summary>
    [Serializable]
    public class OrphanDocument
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
        /// Тип документа, подтверждающего сиротство
        /// </summary>
        public string OrphanCategoryID { get; set; }

        /// <summary>
        /// Наименование документа
        /// </summary>
        public string DocumentName { get; set; }

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
        /// Организация выдавшая документ
        /// </summary>
        public string DocumentOrganization { get; set; }

        #endregion
    }
}
