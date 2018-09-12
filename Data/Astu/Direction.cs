using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu
{
    /// <summary>
    /// Направление подготовки (специальность)
    /// </summary>
    [TableName("SPCSPR")]
    public class Direction: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [DbFieldInfo("SPC")]
        public string Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DbFieldInfo("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// Краткое наименование
        /// </summary>
        [DbFieldInfo("KN")]
        public string ShortName { get; set; }

        /// <summary>
        /// Код по ФГОС
        /// </summary>
        [DbFieldInfo("SHIFR")]
        public string Code { get; set; }

        /// <summary>
        /// Вид ФГОС
        /// </summary>
        [DbFieldInfo("VIDS")]
        public string EducationStandart { get; set; }

        /// <summary>
        /// Идентификатор вида образовательной программы
        /// </summary>
        [DbFieldInfo("ID_VIDPROG", DbFieldType.Integer)]
        
        public int? EducationProgramTypeId { get; set; }

        /// <summary>
        /// Вид образовательной программы
        /// </summary>
        public EducationProgramType EducationProgramType
        {
            get
            {
                return Astu.EducationProgramTypes.Where(ept => ept.Id == EducationProgramTypeId).FirstOrDefault();
            }
            set
            {
                EducationProgramTypeId = value?.Id;
            }
        }

        public string DisplayName
        {
            get
            {
                return string.Format("{0} ({1})",
                    Name, EducationStandart);
            }
        }
    }
}
