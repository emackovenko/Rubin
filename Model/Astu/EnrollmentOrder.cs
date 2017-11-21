using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Приказ о зачислении из приёмной комиссии
    /// </summary>
    public class EnrollmentOrder: StudentOrderBase
    {
        public EnrollmentOrder()
            : base ()
        {
            OrderTypeId = "0001";
            Comment = "Зачислен на ";
            IsFromAdmission = true;
            NewCourse = 1;
        }

        /// <summary>
        /// Дата начала обучения
        /// </summary>
        [DbFieldInfo("DAT_START", DbFieldType.DateTime)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Курс, на который зачислен студент
        /// </summary>
        [DbFieldInfo("NEWKURS", DbFieldType.Integer)]
        public int NewCourse { get; set; }

        /// <summary>
        /// Признак зачисления из приёмной комиссии
        /// </summary>
        [DbFieldInfo("PR_ZACH", DbFieldType.Boolean)]
        public bool IsFromAdmission { get; set; }

        /// <summary>
        /// Идентификатор источника финансирования (категории обучения)
        /// </summary>
        [NavigationProperty(typeof(FinanceSource))]
        [DbFieldInfo("KOB")]
        public string FinanseSourceId { get; set; }

        /// <summary>
        /// Идентификатор формы обучения
        /// </summary>
        [NavigationProperty(typeof(EducationForm))]
        [DbFieldInfo("FRM")]
        public string EducationFormId { get; set; }

        /// <summary>
        /// Идентификатор направления подготовки
        /// </summary>
        [NavigationProperty(typeof(Direction))]
        [DbFieldInfo("SPC")]
        public string DirectionId { get; set; }
        
        /// <summary>
        /// Идентификатор группы, куда зачислен студент
        /// </summary>
        [NavigationProperty(typeof(Group))]
        [DbFieldInfo("GRP")]
        public string GroupId { get; set; }


        /// <summary>
        /// Источник финансирования (категория обучения)
        /// </summary>
        public FinanceSource FinanceSource
        {
            get
            {
                return Astu.FinanceSources.FirstOrDefault(fs => fs.Id == FinanseSourceId);
            }
            set
            {
                FinanseSourceId = value?.Id;
            }
        }

        /// <summary>
        /// Форма обучения
        /// </summary>
        public EducationForm EducationForm
        {
            get
            {
                return Astu.EducationForms.FirstOrDefault(ef => ef.Id == EducationFormId);
            }
            set
            {
                EducationFormId = value?.Id;
            }
        }

        /// <summary>
        /// Направление подготовки
        /// </summary>
        public Direction Direction
        {
            get
            {
                return Astu.Directions.FirstOrDefault(d => d.Id == DirectionId);
            }
            set
            {
                DirectionId = value?.Id;
            }
        }

        /// <summary>
        /// Группа, в которую зачислен студент
        /// </summary>
        public Group Group
        {
            get
            {
                return Astu.Groups.FirstOrDefault(g => g.Id == GroupId);
            }
            set
            {
                GroupId = value?.Id;
            }
        }
        
    }
}
