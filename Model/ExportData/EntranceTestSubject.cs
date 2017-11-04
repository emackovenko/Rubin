using System;

namespace Model.ExportData
{
    /// <summary>
    /// Дисциплина вступительного испытания
    /// </summary>
    [Serializable]
    public class EntranceTestSubject
    {
        #region Поля
        /// <summary>
        /// ИД дисциплины
        /// </summary>
        public int SubjectID { get; set; }

        /// <summary>
        /// Наименование дисциплины
        /// </summary>
        public string SubjectName { get; set; }

        #endregion
    }
}
