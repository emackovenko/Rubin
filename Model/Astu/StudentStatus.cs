using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    [TableName("STATUS")]
    public class StudentStatus: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [DbDbFieldInfo("ID_STAT", 
DbFieldType.String)]
        public string Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DbDbFieldInfo("NAME", 
DbFieldType.String)]
        public string Name { get; set; }
    }
}
