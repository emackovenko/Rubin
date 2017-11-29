using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonMethods.TypeExtensions.exDateTime;

namespace Model.Astu
{
    public class FacultyTransferOrder: StudentOrderBase
    {
        public FacultyTransferOrder() : base ()
        {
            OrderTypeId = "0014";
            Comment = "Переведен на ";
        }

        public override string DocumentLabel
        {
            get
            {
                return string.Format("Приказ о переводе на {0} курс {1} формы обучения по направлению бакалавриата {2} {3} {4} с {5} от {6} № {7}",
                    NewCourse ?? 1, EducationForm?.GenitiveName, Direction?.Code, Direction?.Name, Faculty?.GenitiveName, StartDate.Format() ?? Date.Format(), Date.Format(), Number);
            }
        }

        /// <summary>
        /// Идентификатор старого факультета
        /// </summary>
        [DbFieldInfo("OLD_FAK")]
        public string OldFacultyId { get; set; }

        /// <summary>
        /// Факультет, с которого осуществялется перевод
        /// </summary>
        public Faculty OldFaculty
        {
            get
            {
                return Astu.Faculties.FirstOrDefault(e => e.Id == OldFacultyId);
            }
            set
            {
                FacultyId = value?.Id;
            }
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

        /// <summary>
        /// Идентификтаор формы обучения
        /// </summary>
        [DbFieldInfo("FRM")]
        public string EducationFormId { get; set; }

        public EducationForm EducationForm
        {
            get
            {
                return Astu.EducationForms.FirstOrDefault(e => e.Id == EducationFormId);
            }
            set
            {
                EducationFormId = value?.Id;
            }
        }

    }
}
