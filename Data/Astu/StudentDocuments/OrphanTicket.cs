using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu
{
    /// <summary>
    /// Документ, подтверждающий принадлежность к детям-сиротам и детям, оставшимся без попечения родителей
    /// </summary>
    public class OrphanTicket: StudentDocumentBase
    {

        const int DOCUMENT_TYPE_ID = 30;
        const string DOCUMENT_NAME = "Документ, подтверждающий принадлежность к детям-сиротам и детям, оставшимся без попечения родителей";

        public OrphanTicket()
            : base ()
        {
            DocumentTypeId = DOCUMENT_TYPE_ID;
            Name = DOCUMENT_NAME;
        }

        /// <summary>
        /// Идентификатор категории сиротства
        /// </summary>
        [DbFieldInfo("ID_ORPHANCATEGORY", DbFieldType.Integer)]
        
        public int? OrphanCategoryId { get; set; }

        /// <summary>
        /// Категория сиротства
        /// </summary>
        public OrphanCategory OrphanCategory
        {
            get
            {
                return Astu.OrphanCategories.FirstOrDefault(oc => oc.Id == OrphanCategoryId);
            }
            set
            {
                OrphanCategoryId = value?.Id;
            }
        }
    }
}
