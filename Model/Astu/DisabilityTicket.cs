using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Справка об установлении инвалидности
    /// </summary>
    public class DisabilityTicket: StudentDocumentBase
    {
        public DisabilityTicket()
            : base ()
        {
            DocumentTypeId = 11;
            Name = "Справка об установлении инвалидности";
        }

        /// <summary>
        /// Идентификатор инвалидной группы
        /// </summary>
        [DbFieldInfo("ID_DISABILITYTYPE", DbFieldType.Integer)]
        public int? DisabilityTypeId { get; set; }

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
