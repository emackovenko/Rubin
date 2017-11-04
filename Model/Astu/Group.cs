using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Студенческая группа
    /// </summary>
    [TableName("GRPSPR")]
    public class Group : Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [FieldName("GRP")]
        [FieldType(DatabaseFieldType.String)]
        public string Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [FieldName("Name")]
        [FieldType(DatabaseFieldType.String)]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор учебного плана, по которому обучается группа
        /// </summary>
        [FieldName("ID_UCH")]
        [FieldType(DatabaseFieldType.String)]
        public string EducationPlanId { get; set; }

        /// <summary>
        /// Флаг окончания обучения группой
        /// </summary>
        [FieldName("END_OB")]
        [FieldType(DatabaseFieldType.Boolean)]
        public bool IsGraduated { get; set; }

        /// <summary>
        /// Учебный план, по которому обучается группа
        /// </summary>
        public EducationPlan EducationPlan
        {
            get
            {
                return Astu.EducationPlans.Where(ep => ep.Id == EducationPlanId).FirstOrDefault();
            }
            set
            {
                if (value != null)
                {
                    EducationPlanId = value.Id;
                }
                else
                {
                    EducationPlanId = null;
                }
            }
        }
    }
}
