using System;

namespace Model.ExportData
{
    /// <summary>
    /// Условие приема
    /// </summary>
    [Serializable]
    public class FinSourceEduForm
    {
        #region Поля
        /// <summary>
        /// UID конкурсной группы
        /// </summary>
        public string CompetitiveGroupUID { get; set; }

        /// <summary>
        /// Дата согласования на зачисление (необходимо передать при наличии согласия на зачисления)
        /// </summary>
        public string IsAgreedDate { get; set; }

        /// <summary>
        /// Дата отказа от зачисления (необходимо передать при включении заявления в приказ об исключении)
        /// </summary>
        public string IsDisagreedDate { get; set; }
        #endregion
    }
}
