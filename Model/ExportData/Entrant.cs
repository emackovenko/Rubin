using System;

namespace Model.ExportData
{
    /// <summary>
    /// Абитуриент
    /// </summary>
    [Serializable]
    public class Entrant
    {
        #region Поля
        /// <summary>
        /// Идентификатор в ИС ОО
        /// </summary>
        public string UID { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public int GenderID { get; set; }

        /// <summary>
        /// Дополнительные сведения, предоставленные абитуриентом
        /// </summary>
        public string CustomInformation { get; set; }

        /// <summary>
        /// Электронный или почтовый адрес
        /// </summary>
        public EmailOrMailAddress EmailOrMailAddress { get; set; }
        #endregion
    }
}
