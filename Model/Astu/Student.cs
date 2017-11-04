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
        [FieldType(DatabaseFieldType.String)]
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
        [FieldType(DatabaseFieldType.String)]
        public DateTime? IdentityDocumentDate { get; set; }

        /// <summary>
        /// Идентификатор группы
        /// </summary>
        [FieldName("GRP")]
        [FieldType(DatabaseFieldType.String)]
        public string GroupId { get; set; }

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
    }
}
