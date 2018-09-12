using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu
{
    /// <summary>
    /// Справка об установлении инвалидности
    /// </summary>
    public class DisabilityTicket: StudentDocumentBase
    {

        const int DOCUMENT_TYPE_ID = 30;
        const string DOCUMENT_NAME = "Справка об установлении инвалидности";

        public DisabilityTicket()
            : base ()
        {
            DocumentTypeId = DOCUMENT_TYPE_ID;
            Name = DOCUMENT_NAME;
        }

        /// <summary>
        /// Идентификатор инвалидной группы
        /// </summary>
        [DbFieldInfo("ID_DISABILITYTYPE", DbFieldType.Integer)]
        
        public int? DisabilityTypeId { get; set; }

        /// <summary>
        /// Группа инвалидности
        /// </summary>
        public DisabilityType DisabilityType
        {
            get
            {
                return Astu.DisabilityTypes.FirstOrDefault(dt => dt.Id == DisabilityTypeId);
            }
            set
            {
                DisabilityTypeId = value?.Id;
            }
        }
    }
}
