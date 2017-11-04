using System;

namespace Model.ExportData
{
    /// <summary>
    /// Документ об оброзовании
    /// </summary>
    [Serializable]
    public class EduDocument
    {
        #region Поля
        /// <summary>
        /// Аттестат о среднем (полном) общем образовании
        /// </summary>
        public SchoolCertificateDocument SchoolCertificateDocument { get; set; }

        /// <summary>
        /// Доплом о высшем профессиональном образовании
        /// </summary>
        public HighEduDiplomaDocument HighEduDiplomaDocument { get; set; }

        /// <summary>
        /// Диплом о среднем профессиональном образовании
        /// </summary>
        public MiddleEduDiplomaDocument MiddleEduDiplomaDocument { get; set; }

        /// <summary>
        /// Иной документ об образовании
        /// </summary>
        public EduCustomDocument EduCustomDocument { get; set; }
        #endregion
    }
}
