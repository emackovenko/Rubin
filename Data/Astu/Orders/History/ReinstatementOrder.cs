using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders.History
{
    public class ReinstatementOrder: StudentOrderBase
    {
        public ReinstatementOrder()
            : base ()
        {
            OrderTypeId = "0006";
            Comment = "Восстановлен на ";
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
        /// Идентификатор источника финансирования (категории обучения)
        /// </summary>        
        [DbFieldInfo("KOB")]
        public string FinanseSourceId { get; set; }

        /// <summary>
        /// Идентификатор формы обучения
        /// </summary>        
        [DbFieldInfo("FRM")]
        public string EducationFormId { get; set; }

        /// <summary>
        /// Идентификатор направления подготовки
        /// </summary>        
        [DbFieldInfo("SPC")]
        public string DirectionId { get; set; }

        /// <summary>
        /// Идентификатор группы, куда зачислен студент
        /// </summary>        
        [DbFieldInfo("GRP")]
        public string GroupId { get; set; }


        /// <summary>
        /// Источник финансирования (категория обучения)
        /// </summary>
        public FinanceSource FinanceSource
        {
            get => Astu.FinanceSources.FirstOrDefault(fs => fs.Id == FinanseSourceId);
            set => FinanseSourceId = value?.Id;
        }

        /// <summary>
        /// Форма обучения
        /// </summary>
        public EducationForm EducationForm
        {
            get => Astu.EducationForms.FirstOrDefault(ef => ef.Id == EducationFormId);
            set => EducationFormId = value?.Id;
        }

        /// <summary>
        /// Направление подготовки
        /// </summary>
        public Direction Direction
        {
            get => Astu.Directions.FirstOrDefault(d => d.Id == DirectionId);
            set => DirectionId = value?.Id;
        }

        /// <summary>
        /// Группа, в которую зачислен студент
        /// </summary>
        public Group Group
        {
            get => Astu.Groups.FirstOrDefault(g => g.Id == GroupId);
            set => GroupId = value?.Id;
        }

    }
}
