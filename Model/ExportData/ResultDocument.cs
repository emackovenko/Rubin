using System;

namespace Model.ExportData
{
    /// <summary>
    /// Сведения об основании для оценки
    /// </summary>
    [Serializable]
    public class ResultDocument
    {
        #region Поля
        /// <summary>
        /// Испытания ОО
        /// </summary>
        public InstitutionDocument InstitutionDocument { get; set; }

        /// <summary>
        /// UID свидетельства о резельтатах ЕГЭ, приложенное к заявлению
        /// </summary>
        public string EgeDocumentID { get; set; }
        #endregion
    }
}
