using System;

namespace Model.ExportData
{
    /// <summary>
    /// Электронный или почтовый адрес
    /// </summary>
    [Serializable]
    public class EmailOrMailAddress
    {
        #region Поля
        /// <summary>
        /// Электронный адрес
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// Почтовый адрес
        /// </summary>
        public MailAddress MailAddress { get; set; }
        #endregion
    }
}
