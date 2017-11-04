using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
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
        [FieldName("SPC")]
        [FieldType(DatabaseFieldType.String)]
        public string Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [FieldName("NAME")]
        [FieldType(DatabaseFieldType.String)]
        public string Name { get; set; }

        /// <summary>
        /// Краткое наименование
        /// </summary>
        [FieldName("KN")]
        [FieldType(DatabaseFieldType.String)]
        public string ShortName { get; set; }

        /// <summary>
        /// Код по ФГОС
        /// </summary>
        [FieldName("SHIFR")]
        [FieldType(DatabaseFieldType.String)]
        public string Code { get; set; }

        /// <summary>
        /// Вид ФГОС
        /// </summary>
        [FieldName("VIDS")]
        [FieldType(DatabaseFieldType.String)]
        public string EducationStandart { get; set; }

        /// <summary>
        /// Идентификатор вида образовательной программы
        /// </summary>
        [FieldName("ID_VIDPROG")]
        [FieldType(DatabaseFieldType.Integer)]
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
                if (value != null)
                {
                    EducationProgramTypeId = value.Id;
                }
                else
                {
                    EducationProgramTypeId = null;
                }
            }
        }
    }
}
