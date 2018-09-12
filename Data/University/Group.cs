using System;
using System.Collections.Generic;
using System.Text;

namespace Data.University
{
    /// <summary>
    /// Группа студентов
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Год приема
        /// </summary>
        public int AdmissionYear { get; set; }

        /// <summary>
        /// Идентификатор факультета
        /// </summary>
        public int? FacultyId { get; set; }

        /// <summary>
        /// Факультет
        /// </summary>
        public Faculty Faculty { get; set; }

        /// <summary>
        /// Идентификатор формы обучения
        /// </summary>
        public int? EducationFormId { get; set; }

        /// <summary>
        /// Форма обучения
        /// </summary>
        public EducationForm EducationForm { get; set; }

        /// <summary>
        /// идентификатор учебного плана, по которому обучается группа
        /// </summary>
        public int? EducationPlanId { get; set; }
        
        /// <summary>
        /// Учебнвй план, по которому обучается группа
        /// </summary>
        public EducationPlan EducationPlan { get; set; }


        /// <summary>
        /// Студенты группы
        /// </summary>
        public List<Student> Students { get; set; }


        public Group()
        {
            Students = new List<Student>();
        }
    }
}
