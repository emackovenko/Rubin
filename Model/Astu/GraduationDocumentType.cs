using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    [TableName("adm.VDOSPR")]
    public class GraduationDocumentType: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [DbFieldInfo("VDO")]
        public string Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DbFieldInfo("NAME_VDO")]
        public string Name { get; set; }
    }
}
