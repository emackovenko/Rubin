using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu
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

        public override string ToString()
        {
            return string.Format("{0} {1}", Name, EducationPlan?.Year);
        }

        public IEnumerable<Student> Students
        {
            get
            {
                return Astu.Students.Where(s => s.GroupId == Id);
            }
        }

    }
}
