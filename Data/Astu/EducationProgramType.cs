using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Data.Astu
{
    /// <summary>
    /// Вид образовательной программы
    /// </summary>
    [TableName("VIDPROG")]
    public class EducationProgramType: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [DbFieldInfo("ID_VIDPROG", DbFieldType.Integer)]
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DbFieldInfo("NAME_VIDPROG")]
        public string Name { get; set; }

        /// <summary>
        /// Краткое наименование
        /// </summary>
        [DbFieldInfo("FOLDER_OMKO")]
        public string ShortName { get; set; }
        

    }
}
