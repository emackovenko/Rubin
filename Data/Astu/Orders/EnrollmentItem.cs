using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders
{
    public class EnrollmentItem: OrderItem
    {
        public EnrollmentItem() : base ()
        {
            OrderTypeId = "0001";
            Comment = "Зачислен на ";
        }
        
        /// <summary>
        /// Дата начала обучения
        /// </summary>
        [DbFieldInfo("DAT_START", DbFieldType.DateTime)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Новый курс
        /// </summary>
        [DbFieldInfo("NEW_KURS", DbFieldType.Integer)]
        public int? NewCourse { get; set; }


        /// <summary>
        /// Идентификатор группы
        /// </summary>
        [DbFieldInfo("GRP")]
        public string GroupId { get; set; }

        /// <summary>
        /// Группа
        /// </summary>
        public Group Group
        {
            get => Astu.Groups.FirstOrDefault(e => e.Id == GroupId);
            set => GroupId = value?.Id;
        }

        /// <summary>
        /// Идентификатор направления подготовки
        /// </summary>
        [DbFieldInfo("SPC")]
        public string DirectionId { get; set; }

        /// <summary>
        /// Направление подготовки
        /// </summary>
        public Direction Direction
        {
            get => Astu.Directions.FirstOrDefault(e => e.Id == DirectionId);
            set => DirectionId = value?.Id;
        }

        /// <summary>
        /// Идентификатор источника финансирования (категории обучения)
        /// </summary>
        [DbFieldInfo("KOB")]
        public string FinanceSourceId { get; set; }

        /// <summary>
        /// Источник финансирования (категория обучения)
        /// </summary>
        public FinanceSource FinanceSource
        {
            get => Astu.FinanceSources.FirstOrDefault(e => e.Id == FinanceSourceId);
            set => FinanceSourceId = value?.Id;
        }

        /// <summary>
        /// Идентификатор формы обучения
        /// </summary>
        [DbFieldInfo("FRM")]
        public string EducationFormId { get; set; }

        /// <summary>
        /// Форма обучения
        /// </summary>
        public EducationForm EducationForm
        {
            get => Astu.EducationForms.FirstOrDefault(e => e.Id == EducationFormId);
            set => EducationFormId = value?.Id;
        }
        
    }
}
