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
        [DbFieldInfo("GRP")]
        public string Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [DbFieldInfo("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор учебного плана, по которому обучается группа
        /// </summary>
        [DbFieldInfo("ID_UCH")]
        [NavigationProperty(typeof(EducationPlan))]
        public string EducationPlanId { get; set; }

        /// <summary>
        /// Флаг окончания обучения группой
        /// </summary>
        [DbFieldInfo("END_OB", DbFieldType.Boolean)]
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
                EducationPlanId = value?.Id;
            }
        }
    }
}
