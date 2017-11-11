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
        [FieldName("KOD")]
        [FieldType(DatabaseFieldType.String)]
        public string Id { get; set; }

        /// <summary>
        /// Ф.И.О.
        /// </summary>
        [FieldName("FIO")]
        [FieldType(DatabaseFieldType.String)]
        public string Name { get; set; }

        /// <summary>
        /// Регистрационный номер студента
        /// </summary>
        [FieldName("RNOM")]
        [FieldType(DatabaseFieldType.String)]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [FieldName("DATR")]
        [FieldType(DatabaseFieldType.OracleDateTime)]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        [FieldName("POL")]
        [FieldType(DatabaseFieldType.String)]
        public string Gender { get; set; }

        /// <summary>
        /// Флаг - нуждается в общежитии
        /// </summary>
        [FieldName("OBCHAGA")]
        [FieldType(DatabaseFieldType.Boolean)]
        public bool IsNeedHostel { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [FieldName("TEL")]
        [FieldType(DatabaseFieldType.String)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Документ, удостоверяющий личность - серия
        /// </summary>
        [FieldName("PASP_SER")]
        [FieldType(DatabaseFieldType.String)]
        public string IdentityDocumentSeries { get; set; }

        /// <summary>
        /// Документ, удостоверяющий личность - номер
        /// </summary>
        [FieldName("PASP_NOM")]
        [FieldType(DatabaseFieldType.String)]
        public string IdentityDocumentNumber { get; set; }

        /// <summary>
        /// Документ, удостоверяющий личность - кем выдан
        /// </summary>
        [FieldName("PASP_VID")]
        [FieldType(DatabaseFieldType.String)]
        public string IdentityDocumentOrganization { get; set; }

        /// <summary>
        /// Документ, удостоверяющий личность - дата выдачи
        /// </summary>
        [FieldName("PASP_DATE")]
        [FieldType(DatabaseFieldType.OracleDateTime)]
        public DateTime? IdentityDocumentDate { get; set; }

        ///// <summary>
        ///// Место рождения
        ///// </summary>
        //[FieldName("PASP_MST_ROJ")]
        //[FieldType(DatabaseFieldType.String)]
        //public string BirthPlace { get; set; }

        /// <summary>
        /// Документ об образовании - серия
        /// </summary>
        [FieldName("ATT_SER")]
        [FieldType(DatabaseFieldType.String)]
        public string GraduationDocumentSeries { get; set; }

        /// <summary>
        /// Документ об образовании - номер
        /// </summary>
        [FieldName("ATT_NOM")]
        [FieldType(DatabaseFieldType.String)]
        public string GraduationDocumentNumber { get; set; }

        /// <summary>
        /// Документ об образовании - дата выдачи
        /// </summary>
        [FieldName("ATT_DATE")]
        [FieldType(DatabaseFieldType.OracleDateTime)]
        public DateTime? GraduationDocumentDate { get; set; }



        /// <summary>
        /// Идентификатор группы
        /// </summary>
        [FieldName("GRP")]
        [FieldType(DatabaseFieldType.String)]
        public string GroupId { get; set; }

        /// <summary>
        /// Идентификатор статуса студента
        /// </summary>
        [FieldName("ID_STAT")]
        [FieldType(DatabaseFieldType.String)]
        public string StatusId { get; set; }

        /// <summary>
        /// Текущий курс студента
        /// </summary>
        [FieldName("KURS")]
        [FieldType(DatabaseFieldType.Integer)]
        public int Course { get; set; }

        /// <summary>
        /// Год приёма
        /// </summary>
        [FieldName("GOD_P")]
        [FieldType(DatabaseFieldType.Integer)]
        public int AdmissionYear { get; set; }

        /// <summary>
        /// Идентификатор источника финансирования
        /// </summary>
        [FieldName("KOB")]
        [FieldType(DatabaseFieldType.String)]
        public string FinanceSourceId { get; set; }

        /// <summary>
        /// Идентификатор формы обучения
        /// </summary>
        [FieldName("FRM")]
        [FieldType(DatabaseFieldType.String)]
        public string EducationFormId { get; set; }

        /// <summary>
        /// Идентификатор факультета
        /// </summary>
        [FieldName("FAK")]
        [FieldType(DatabaseFieldType.String)]
        public string FacultyId { get; set; }

        /// <summary>
        /// Идентификатор направления подготовки
        /// </summary>
        [FieldName("SPC")]
        [FieldType(DatabaseFieldType.String)]
        public string DirectionId { get; set; }

        /// <summary>
        /// Идентификатор гражданства
        /// </summary>
        [FieldName("GOS")]
        [FieldType(DatabaseFieldType.String)]
        public string CitizenshipId { get; set; }

        /// <summary>
        /// Идентификатор изучаемого иностранного языка
        /// </summary>
        [FieldName("LNG")]
        [FieldType(DatabaseFieldType.String)]
        public string ForeignLanguageId { get; set; }

        /// <summary>
        /// Идентификатор вида стипендии
        /// </summary>
        [FieldName("VST")]
        [FieldType(DatabaseFieldType.String)]
        public string GrantTypeId { get; set; }

        /// <summary>
        /// Идентификатор вида документа об образовании
        /// </summary>
        [FieldName("VDO")]
        [FieldType(DatabaseFieldType.String)]
        public string GraduationDocumentTypeId { get; set; }

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
        public GraduationDocumentType GraduationDocumentType
        {
            get
            {
                return Astu.GraduationDocumentTypes.FirstOrDefault(gdt => gdt.Id == GraduationDocumentTypeId);
            }
            set
            {
                if (value != null)
                {
                    GraduationDocumentTypeId = value.Id;
                }
                else
                {
                    GraduationDocumentTypeId = null;
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

        #endregion

    }
}
