using System;
using System.Collections.Generic;

namespace Model.ExportData
{
    /// <summary>
    /// Заявление
    /// </summary>
    public class Application
    {
        #region Поля
        /// <summary>
        /// Идентификатор и ИС ОО
        /// </summary>
        public string UID { get; set; }

        /// <summary>
        /// Номер заявления OO
        /// </summary>
        public string ApplicationNumber { get; set; }

        /// <summary>
        /// Абитуриент
        /// </summary>
        public Entrant Entrant { get; set; }

        /// <summary>
        /// Дата регистрации заявления
        /// </summary>
        public string RegistrationDate { get; set; }

        /// <summary>
        /// Признак необходимости общежития
        /// </summary>
        public bool NeedHostel { get; set; }


		public string StatusID { get; set; }

		/// <summary>
		/// Комментарий к статусу заявления
		/// </summary>
		public string StatusComment { get; set; }

        /// <summary>
        /// Условия приема
        /// </summary>
        public List<FinSourceEduForm> FinSourceAndEduForms { get; set; }

        /// <summary>
        /// Документы, приложенные к заявлению
        /// </summary>
        public ApplicationDocuments ApplicationDocuments { get; set; }

        /// <summary>
        /// Результаты вступительных испытаний
        /// </summary>
        public List<EntranceTestResult> EntranceTestResults { get; set; }
#endregion
    }
}
