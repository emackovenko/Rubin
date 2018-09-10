using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.University
{
    /// <summary>
    /// Элемент учебного плана
    /// </summary>
    public class EducationPlanItem
    {
        public EducationPlanItem()
        {
            Graphics = new List<EducationPlanGraphic>();
        }


        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Код элемента
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Флаг - элемент выбран
        /// </summary>
        public bool? IsChecked { get; set; }

        /// <summary>
        /// Идентификатор дисциплины
        /// </summary>
        public int? DisciplineId { get; set; }

        /// <summary>
        /// Дисциплина
        /// </summary>
        public Discipline Discipline { get; set; }

        /// <summary>
        /// Идентификатор цикла дисциплин
        /// </summary>
        public int? DisciplineCycleId { get; set; }

        /// <summary>
        /// Цикл дисциплин
        /// </summary>
        public DisciplineCycle Cycle { get; set; }

        /// <summary>
        /// Идентификатор компоненты учебного плана
        /// </summary>
        public int? EducationPlanComponentId { get; set; }

        /// <summary>
        /// Компонента учебного плана
        /// </summary>
        public EducationPlanCompoment Component { get; set; }

        /// <summary>
        /// Иднетификатор образовательной компетенциии
        /// </summary>
        public int? EducationCompetenceId { get; set; }

        /// <summary>
        /// Образовательная компетенция
        /// </summary>
        public EducationCompetence Competence { get; set; }

        /// <summary>
        /// Идентификатор учебного плана
        /// </summary>
        public int? EducationPlanId { get; set; }

        /// <summary>
        /// Учебный план
        /// </summary>
        public EducationPlan EducationPlan { get; set; }

        /// <summary>
        /// Учебные графики по элементу
        /// </summary>
        public List<EducationPlanGraphic> Graphics { get; set; }
    }
}
