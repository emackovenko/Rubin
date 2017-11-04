using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Учебный план
    /// </summary>
    [TableName("UCHPLAN")]
    public class EducationPlan: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [FieldName("ID_UCH")]
        [FieldType(DatabaseFieldType.String)]
        public string Id { get; set; }

        /// <summary>
        /// Идентификатор факультета
        /// </summary>
        [FieldName("FAK")]
        [FieldType(DatabaseFieldType.String)]
        public string FacultyId { get; set; }

        /// <summary>
        /// Идентификатор направления подготовки
        /// </summary>
        [FieldName("SPC")]
        [FieldType(DatabaseFieldType.String)]
        public string DirectionId { get; set; }

        /// <summary>
        /// Идентификатор формы обучения
        /// </summary>
        [FieldName("FRM")]
        [FieldType(DatabaseFieldType.String)]
        public string EducationFormId { get; set; }

        /// <summary>
        /// Год, с которого учебный план вступает в силу
        /// </summary>
        [FieldName("GOD")]
        [FieldType(DatabaseFieldType.Integer)]
        public int Year { get; set; }

        /// <summary>
        /// Факультет, реализующий учебный план
        /// </summary>
        public Faculty Faculty
        {
            get
            {
                return Astu.Faculties.Where(f => f.Id == FacultyId).FirstOrDefault();
            }
            set
            {
                if (value != null)
                {
                    FacultyId = value.Id;
                }
                else
                {
                    FacultyId = null;
                }
            }
        }

        /// <summary>
        /// Направление подготовки, по которому реализуется учебный план
        /// </summary>
        public Direction Direction
        {
            get
            {
                return Astu.Directions.Where(d => d.Id == DirectionId).FirstOrDefault();
            }
            set
            {
                if (value != null)
                {
                    DirectionId = value.Id;
                }
                else
                {
                    DirectionId = null;
                }
            }
        }

        /// <summary>
        /// Форма обучения, по которой реализуется учебный план
        /// </summary>
        public EducationForm EducationForm
        {
            get
            {
                return Astu.EducationForms.Where(ef => ef.Id == EducationFormId).FirstOrDefault();
            }
            set
            {
                if (value != null)
                {
                    EducationFormId = value.Id;
                }
            }
        }
    }
}
