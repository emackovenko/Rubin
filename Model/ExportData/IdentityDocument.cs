using System;

namespace Model.ExportData
{
    /// <summary>
    /// Документ, удостоверяющий личность
    /// </summary>
    [Serializable]
    public class IdentityDocument
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
        public string MiddleName{ get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public int GenderID { get; set; }

        /// <summary>
        /// Серия документа
        /// </summary>
        public string DocumentSeries { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Код подразделения
        /// </summary>
        public string SubdivisionCode { get; set; }

        /// <summary>
        /// Дата выдачи документа
        /// </summary>
        public string DocumentDate { get; set; }

        /// <summary>
        /// Кем выдан
        /// </summary>
        public string DocumentOrganization { get; set; }

        /// <summary>
        /// Ид типа документа, удостоверяющего личность
        /// </summary>
        public string IdentityDocumentTypeID { get; set; }

        /// <summary>
        /// Ид гражданства
        /// </summary>
        public string NationalityTypeID { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public string BirthDate { get; set; }

        /// <summary>
        /// Место рождения
        /// </summary>
        public string BirthPlace { get; set; }
        #endregion
    }
}
