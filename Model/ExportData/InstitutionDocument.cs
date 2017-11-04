using System;

namespace Model.ExportData
{
    /// <summary>
    /// Испытания ОО
    /// </summary>
    [Serializable]
    public class InstitutionDocument
    {
        #region Поля
        /// <summary>
        /// Номер документа
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата выдачи документа
        /// </summary>
        public string DocumentDate { get; set; }

        /// <summary>
        /// Тип документа
        /// </summary>
        public int DocumentTypeID { get; set; }

        #endregion
    }
}
