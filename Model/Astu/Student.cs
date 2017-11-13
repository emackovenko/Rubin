using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Карточка студента
    /// </summary>
    [TableName("ANKETA")]
    public class Student : Entity
    {

        #region Database properties

        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [DbFieldInfo("KOD")]
        public string Id { get; set; }

        /// <summary>
        /// Ф.И.О.
        /// </summary>
        [DbFieldInfo("FIO")]
        public string Name { get; set; }

        /// <summary>
        /// Регистрационный номер студента
        /// </summary>
        [DbFieldInfo("RNOM")]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [DbFieldInfo("DATR", DbFieldType.DateTime)]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        [DbFieldInfo("POL")]
        public string Gender { get; set; }

        /// <summary>
        /// Флаг - нуждается в общежитии
        /// </summary>
        [DbFieldInfo("OBCHAGA", DbFieldType.Boolean)]
        public bool IsNeedHostel { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [DbFieldInfo("TEL")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Документ, удостоверяющий личность - серия
        /// </summary>
        [DbFieldInfo("PASP_SER")]
        public string IdentityDocumentSeries { get; set; }

        /// <summary>
        /// Документ, удостоверяющий личность - номер
        /// </summary>
        [DbFieldInfo("PASP_NOM")]
        public string IdentityDocumentNumber { get; set; }

        /// <summary>
        /// Документ, удостоверяющий личность - кем выдан
        /// </summary>
        [DbFieldInfo("PASP_VID")]
        public string IdentityDocumentOrganization { get; set; }

        /// <summary>
        /// Документ, удостоверяющий личность - дата выдачи
        /// </summary>
        [DbFieldInfo("PASP_DATE", DbFieldType.DateTime)]
        public DateTime? IdentityDocumentDate { get; set; }

        ///// <summary>
        ///// Место рождения
        ///// </summary>
        //[DbFieldInfo("PASP_MST_ROJ")]
        //public string BirthPlace { get; set; }

        /// <summary>
        /// Документ об образовании - серия
        /// </summary>
        [DbFieldInfo("ATT_SER")]
        public string GraduationDocumentSeries { get; set; }

        /// <summary>
        /// Документ об образовании - номер
        /// </summary>
        [DbFieldInfo("ATT_NOM")]
        public string GraduationDocumentNumber { get; set; }

        /// <summary>
        /// Документ об образовании - дата выдачи
        /// </summary>
        [DbFieldInfo("ATT_DATE", DbFieldType.DateTime)]
        public DateTime? GraduationDocumentDate { get; set; }
        

        /// <summary>
        /// Идентификатор группы
        /// </summary>
        [DbFieldInfo("GRP")]
        public string GroupId { get; set; }

        /// <summary>
        /// Идентификатор статуса студента
        /// </summary>
        [DbFieldInfo("ID_STAT")]
        public string StatusId { get; set; }

        /// <summary>
        /// Текущий курс студента
        /// </summary>
        [DbFieldInfo("KURS", DbFieldType.Integer)]
        public int? Course { get; set; }

        /// <summary>
        /// Год приёма
        /// </summary>
        [DbFieldInfo("GOD_P", DbFieldType.Integer)]
        public int AdmissionYear { get; set; }

        /// <summary>
        /// Идентификатор источника финансирования
        /// </summary>
        [DbFieldInfo("KOB")]
        public string FinanceSourceId { get; set; }

        /// <summary>
        /// Идентификатор формы обучения
        /// </summary>
        [DbFieldInfo("FRM")]
        public string EducationFormId { get; set; }

        /// <summary>
        /// Идентификатор факультета
        /// </summary>
        [DbFieldInfo("FAK")]
        public string FacultyId { get; set; }

        /// <summary>
        /// Идентификатор направления подготовки
        /// </summary>
        [DbFieldInfo("SPC")]
        public string DirectionId { get; set; }

        /// <summary>
        /// Идентификатор гражданства
        /// </summary>
        [DbFieldInfo("GOS")]
        public string CitizenshipId { get; set; }

        /// <summary>
        /// Идентификатор изучаемого иностранного языка
        /// </summary>
        [DbFieldInfo("LNG")]
        public string ForeignLanguageId { get; set; }

        /// <summary>
        /// Идентификатор вида стипендии
        /// </summary>
        [DbFieldInfo("VST")]
        public string GrantTypeId { get; set; }

        /// <summary>
        /// Идентификатор вида документа об образовании
        /// </summary>
        [DbFieldInfo("VDO")]
        public string EducationDocumentTypeId { get; set; }

        #endregion

        #region Navigation properties

        /// <summary>
        /// Группа, в которой обучается студент
        /// </summary>
        public Group Group
        {
            get
            {
                return Astu.Groups.Where(g => g.Id == GroupId).FirstOrDefault();
            }
            set
            {
                if (value != null)
                {
                    GroupId = value.Id;
                }
                else
                {
                    GroupId = null;
                }
            }
        }

        /// <summary>
        /// Статус студента
        /// </summary>
        public StudentStatus Status
        {
            get
            {
                return Astu.StudentStatuses.Where(s => s.Id == StatusId).FirstOrDefault();
            }
            set
            {
                if (value != null)
                {
                    StatusId = value.Id;
                }
                else
                {
                    StatusId = null;
                }
            }
        }

        /// <summary>
        /// Источник финансирования (категория обучения студента)
        /// </summary>
        public FinanceSource FinanceSource
        {
            get
            {
                return Astu.FinanceSources.FirstOrDefault(fs => fs.Id == FinanceSourceId);
            }
            set
            {
                if (value != null)
                {
                    FinanceSourceId = value.Id;
                }
                else
                {
                    FinanceSourceId = null;
                }
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
                if (value != null)
                {
                    EducationFormId = value.Id;
                }
                else
                {
                    EducationFormId = null;
                }
            }
        }

        /// <summary>
        /// Факультет
        /// </summary>
        public Faculty Faculty 
        {
            get
            {
                return Astu.Faculties.FirstOrDefault(f => f.Id == FacultyId);
            }
            set
            {
                if (value != null)
                {
                    FacultyId = value.Id;
                }
                else
                {
                    FacultyId = null;
                }
            }
        }

        /// <summary>
        /// Направление подготовки (специальность)
        /// </summary>
        public Direction Direction 
        {
            get
            {
                return Astu.Directions.FirstOrDefault(d => d.Id == DirectionId);
            }
            set
            {
                if (value != null)
                {
                    DirectionId = value.Id;
                }
                else
                {
                    DirectionId = null;
                }
            }
        }

        /// <summary>
        /// Гражданство
        /// </summary>
        public Citizenship Citizenship 
        {
            get
            {
                return Astu.Citizenships.FirstOrDefault(c => c.Id == CitizenshipId);
            }
            set
            {
                if (value != null)
                {
                    CitizenshipId = value.Id;
                }
                else
                {
                    CitizenshipId = null;
                }
            }
        }

        /// <summary>
        /// Вид стипендии
        /// </summary>
        public GrantType GrantType 
        {
            get
            {
                return Astu.GrantTypes.FirstOrDefault(gt => gt.Id == GrantTypeId);
            }
            set
            {
                if (value != null)
                {
                    GrantTypeId = value.Id;
                }
                else
                {
                    GrantTypeId = null;
                }
            }
        }

        /// <summary>
        /// Вид документа об образовании
        /// </summary>
        public EducationDocumentType EducationDocumentType
        {
            get
            {
                return Astu.EducationDocumentTypes.FirstOrDefault(gdt => gdt.Id == EducationDocumentTypeId);
            }
            set
            {
                if (value != null)
                {
                    EducationDocumentTypeId = value.Id;
                }
                else
                {
                    EducationDocumentTypeId = null;
                }
            }
        }

        /// <summary>
        /// Изучаемый иностранный язык
        /// </summary>
        public ForeignLanguage ForeignLanguage
        {
            get
            {
                return Astu.ForeignLanguages.FirstOrDefault(fl => fl.Id == ForeignLanguageId);
            }
            set
            {
                if (value != null)
                {
                    ForeignLanguageId = value.Id;
                }
                else
                {
                    ForeignLanguageId = null;
                }
            }
        }

        #endregion


        #region Navigation collections

        /// <summary>
        /// Приказы по студенту (ANK_HIST)
        /// </summary>
        public IEnumerable<StudentOrderBase> Orders
        {
            get
            {
                var list = new List<StudentOrderBase>();
                list.AddRange(Astu.EnrollmentOrders.Where(o => o.StudentId == Id).Cast<StudentOrderBase>());
                list.AddRange(Astu.UnenrollmentOrders.Where(o => o.StudentId == Id).Cast<StudentOrderBase>());
                list.AddRange(Astu.AcademicVacationOrders.Where(o => o.StudentId == Id).Cast<StudentOrderBase>());
                list.AddRange(Astu.ReinstatementOrders.Where(o => o.StudentId == Id).Cast<StudentOrderBase>());
                list.AddRange(Astu.AcademicVacationExitOrders.Where(o => o.StudentId == Id).Cast<StudentOrderBase>());
                list.AddRange(Astu.NextCourseTransferOrders.Where(o => o.StudentId == Id).Cast<StudentOrderBase>());
                return list;
            }
        }

        /// <summary>
        /// Документы
        /// </summary>
        public IEnumerable<StudentDocumentBase> Documents
        {
            get
            {
                var list = new List<StudentDocumentBase>();
                list.AddRange(Astu.IdentityDocuments.Where(d => d.StudentId == Id).Cast<StudentDocumentBase>());
                list.AddRange(Astu.EducationDocuments.Where(d => d.StudentId == Id).Cast<StudentDocumentBase>());
                return list;
            }
        }

        #endregion

    }
}
