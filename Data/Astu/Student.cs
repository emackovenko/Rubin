using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Astu.Orders.History;

namespace Data.Astu
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
        public string FullName { get; set; }

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
                GroupId = value?.Id;
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
                StatusId = value?.Id;
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
                FinanceSourceId = value?.Id;
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
                FacultyId = value?.Id;
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
                DirectionId = value?.Id;
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
                CitizenshipId = value?.Id;
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
                GrantTypeId = value?.Id;
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
                EducationDocumentTypeId = value?.Id;
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
                ForeignLanguageId = value?.Id;
            }
        }

        #endregion


        #region CalculatedProperties

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName
        {
            get
            {
                return FullName?.Split(' ')[0] ?? null;
            }
        }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName
        {
            get
            {
                return FullName?.Split(' ')[1] ?? null;
            }
        }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronimyc
        {
            get
            {
                if (FullName?.Split(' ').Count() > 2)
                {
                    return FullName.Remove(0, FirstName.Length + LastName.Length + 2);
                }
                return null;
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
                list.AddRange(Astu.AcademicVacationExitOrders.Where(o => o.StudentId == Id));
                list.AddRange(Astu.AcademicVacationOrders.Where(o => o.StudentId == Id));
                list.AddRange(Astu.ChildrenFussVacationExitOrders.Where(o => o.StudentId == Id));
                list.AddRange(Astu.ChildrenFussVacationOrders.Where(o => o.StudentId == Id));
                list.AddRange(Astu.DirectionChangingOrders.Where(o => o.StudentId == Id));
                list.AddRange(Astu.EnrollmentByUniversityTransferOrders.Where(o => o.StudentId == Id));
                list.AddRange(Astu.EnrollmentOrders.Where(o => o.StudentId == Id));
                list.AddRange(Astu.EnrollToFullStateProvisionOrders.Where(o => o.StudentId == Id));
                list.AddRange(Astu.FinanceSourceChangingOrders.Where(o => o.StudentId == Id));
                list.AddRange(Astu.GraduationOrders.Where(o => o.StudentId == Id));
                list.AddRange(Astu.GroupTransferOrders.Where(o => o.StudentId == Id));
                //list.AddRange(Astu.StudentNameChangingOrders.Where(o => o.StudentId == Id));
                list.AddRange(Astu.NextCourseTransferOrders.Where(o => o.StudentId == Id));
                list.AddRange(Astu.OtherOrders.Where(o => o.StudentId == Id));
                list.AddRange(Astu.ReinstatementOrders.Where(o => o.StudentId == Id));
                list.AddRange(Astu.TransferToAcceleratedEducationOrders.Where(o => o.StudentId == Id));
                list.AddRange(Astu.UnenrollmentOrders.Where(o => o.StudentId == Id));
                return list.OrderBy(o => o.Date);
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
                list.AddRange(Astu.OrphanTickets.Where(d => d.StudentId == Id).Cast<StudentDocumentBase>());
                list.AddRange(Astu.DisabilityTickets.Where(d => d.StudentId == Id).Cast<StudentDocumentBase>());
                return list.OrderBy(d => d.Date);
            }
        }

        public IEnumerable<NextCourseTransferOrder> NextCourseTransferOrders
        {
            get
            {
                return Astu.NextCourseTransferOrders.Where(o => o.StudentId == Id);
            }
        }

        #endregion
    }
}
