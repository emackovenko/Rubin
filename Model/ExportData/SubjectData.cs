using System;

namespace Model.ExportData
{
    /// <summary>
    /// Дисциплина
    /// </summary>
    [Serializable]
    public class SubjectData
    {
        #region Поля
        /// <summary>
        /// ID дисциплины
        /// </summary>
        public string SubjectID { get; set; }

        /// <summary>
        /// Балл
        /// </summary>
        public int Value { get; set; }
        #endregion
    }
}
