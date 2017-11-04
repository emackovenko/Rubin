using System;

namespace Model.ExportData
{
    /// <summary>
    /// Почтовый адрес
    /// </summary>
    [Serializable]
    public class MailAddress
    {
        #region Поля
        /// <summary>
        /// Регион
        /// </summary>
        public int RegionID { get; set; }

        /// <summary>
        /// Тип населенного пункта
        /// </summary>
        public int TownTypeID { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }
        #endregion
    }
}
