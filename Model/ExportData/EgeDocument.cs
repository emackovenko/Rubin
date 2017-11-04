using System;
using System.Collections.Generic;

namespace Model.ExportData
{
    /// <summary>
    /// Свидетельство о результатах ЕГЭ
    /// </summary>
    [Serializable]
    public class EgeDocument
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
        /// Номер свидетельства о результатах ЕГЭ
        /// </summary>
        public int DocumentNumber{ get; set; }

        /// <summary>
        /// Дата выдачи свидетельства
        /// </summary>
        public string DocumentDate { get; set; }

		public int DocumentYear { get; set; }

		/// <summary>
		/// Дисциплины
		/// </summary>
		public List<SubjectData> Subjects { get; set; }
        #endregion
    }
}
