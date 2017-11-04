using System;
using System.Collections.Generic;

namespace Model.ExportData
{
    /// <summary>
    /// Документы приложенные к заявлению
    /// </summary>
    [Serializable]
    public class ApplicationDocuments
    {
        #region Поля
        /// <summary>
        /// Свидетельства о результатах ЕГЭ
        /// </summary>
        public List<EgeDocument> EgeDocuments { get; set; }

        /// <summary>
        /// Документ, удостоверяющий личность
        /// </summary>
        public IdentityDocument IdentityDocument { get; set; }
        
        /// <summary>
        /// Документы об образовании
        /// </summary>
        public List<EduDocument> EduDocuments { get; set; }

        /// <summary>
        /// Документы, подтверждающие сиротство
        /// </summary>
        public List<OrphanDocument> OrphanDocuments { get; set; }

		public List<IdentityDocument> OtherIdentityDocuments { get; set; }
        #endregion
    }
}
