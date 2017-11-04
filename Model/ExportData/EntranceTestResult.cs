using System;

namespace Model.ExportData
{
    /// <summary>
    /// Результат вступительного испытания
    /// </summary>
    [Serializable]
    public class EntranceTestResult
    {
        #region Поля
        /// <summary>
        /// Идентификатор в ИС ОО
        /// </summary>
        public string UID { get; set; }

        /// <summary>
        /// Балл
        /// </summary>
        public int ResultValue { get; set; }

        /// <summary>
        /// ИД основания для оценки
        /// </summary>
        public int ResultSourceTypeID { get; set; }

        /// <summary>
        /// Дисциплина встепительноо испытания
        /// </summary>
        public EntranceTestSubject EntranceTestSubject { get; set; }

        /// <summary>
        /// ИД вступительного испытания
        /// </summary>
        public int EntranceTestTypeID { get; set; }

        /// <summary>
        /// UID конкурсной группы
        /// </summary>
        public string CompetitiveGroupUID { get; set; }

        /// <summary>
        /// Сведения об основании для оценки
        /// </summary>
        public ResultDocument ResultDocument { get; set; }
        #endregion
    }
}
