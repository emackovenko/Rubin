using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.University
{
    /// <summary>
    /// График элемента учебного плана
    /// </summary>
    public class EducationPlanGraphic
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор элемента учебного плана
        /// </summary>
        public int? EducationPlanItemElementId { get; set; }

        /// <summary>
        /// Элемент учебного плана
        /// </summary>
        public EducationPlanItem EducationPlanItem { get; set; }

        /// <summary>
        /// Номер семестра
        /// </summary>
        public int SemesterNo { get; set; }

        /// <summary>
        /// Количество ЗЕТ (зачетная единица трудоёмкости)
        /// </summary>
        public double Zet { get; set; }

        /// <summary>
        /// Количество лекционных часов
        /// </summary>
        public int LectionHours { get; set; }

        /// <summary>
        /// Количество часов практических работ
        /// </summary>
        public int PracticeHours { get; set; }

        /// <summary>
        /// Количество часов лабораторных работ
        /// </summary>
        public int LaboratoryHours { get; set; }

        /// <summary>
        /// Количество часов самостоятельных работ
        /// </summary>
        public int IndependentWorkHours { get; set; }

        /// <summary>
        /// Зачет
        /// </summary>
        public bool? CreditTest { get; set; }
        
        /// <summary>
        /// Дифференцированный зачет
        /// </summary>
        public bool? DiffCreditTest { get; set; }

        /// <summary>
        /// Экзамен
        /// </summary>
        public bool? ExaminationTest { get; set; }

        /// <summary>
        /// Курсовая работа
        /// </summary>
        public bool? CourseWorkTest { get; set; }

        /// <summary>
        /// Расчетно-графическая работа
        /// </summary>
        public bool? SettlementWorkTest { get; set; }
        
    }
}
