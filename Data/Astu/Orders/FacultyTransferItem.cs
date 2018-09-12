using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu.Orders
{
    public class FacultyTransferItem: OrderItem
    {
        public FacultyTransferItem() : base ()
        {
            OrderTypeId = "0014";
            Comment = "Переведен на ";
        }

        /// <summary>
        /// Идентификатор старого факультета
        /// </summary>
        [DbFieldInfo("NEW_FAK")]
        public string NewFacultyId { get; set; }

        /// <summary>
        /// Факультет, с которого осуществялется перевод
        /// </summary>
        public Faculty NewFaculty
        {
            get => Astu.Faculties.FirstOrDefault(e => e.Id == NewFacultyId);
            set => NewFacultyId = value?.Id;
        }


        /// <summary>
        /// Дата начала обучения
        /// </summary>
        [DbFieldInfo("DAT_START", DbFieldType.DateTime)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Курс, на который зачислен студент
        /// </summary>
        [DbFieldInfo("NEW_KURS", DbFieldType.Integer)]
        public int? NewCourse { get; set; }

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

        /// <summary>
        /// Идентификтаор формы обучения
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
