using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Reflection;
using System.Data;

namespace Model.Astu
{
    /// <summary>
    /// Контекст базы данных АлтГТУ
    /// </summary>
    public static class Astu
    {

        /// <summary>
        /// Загружает все коллекции сущностей из БД в память
        /// </summary>
        static void InitializeContext()
        {
            Students = new EntitySet<Student>();
            Groups = new EntitySet<Group>();
            EducationProgramTypes = new EntitySet<EducationProgramType>();
            Directions = new EntitySet<Direction>();
            Faculties = new EntitySet<Faculty>();
            EducationForms = new EntitySet<EducationForm>();
            EducationPlans = new EntitySet<EducationPlan>();
            QuotaTypes = new EntitySet<QuotaType>();
            ForeignLanguages = new EntitySet<ForeignLanguage>();
            StudentStatuses = new EntitySet<StudentStatus>();
            FinanceSources = new EntitySet<FinanceSource>();
            Citizenships = new EntitySet<Citizenship>("ORDER BY GOS");
            GrantTypes = new EntitySet<GrantType>("WHERE POR_VIS <> 99");
            EducationDocumentTypes = new EntitySet<EducationDocumentType>(@"WHERE VDO IN ('0001', '0009', '0052')");
            OrderTypes = new EntitySet<OrderType>();
            UnenrollmentReasons = new EntitySet<UnenrollmentReason>();
            AcademicVacationReasons = new EntitySet<AcademicVacationReason>();
            EnrollmentOrders = new EntitySet<EnrollmentOrder>(@"WHERE TPR='0001' ORDER BY KOD, DAT");
            UnenrollmentOrders = new EntitySet<UnenrollmentOrder>(@"WHERE TPR='0003' ORDER BY KOD, DAT");
            AcademicVacationOrders = new EntitySet<AcademicVacationOrder>(@"WHERE TPR='0002' ORDER BY KOD, DAT");
            ReinstatementOrders = new EntitySet<ReinstatementOrder>(@"WHERE TPR='0006' ORDER BY KOD, DAT");
            AcademicVacationExitOrders = new EntitySet<AcademicVacationExitOrder>(@"WHERE TPR='0007' ORDER BY KOD, DAT");
            NextCourseTransferOrders = new EntitySet<NextCourseTransferOrder>(@"WHERE TPR='0030' ORDER BY KOD,DAT");
            DocumentTypes = new EntitySet<DocumentType>();
            IdentityDocumentTypes = new EntitySet<IdentityDocumentType>();
            IdentityDocuments = new EntitySet<IdentityDocument>(@"WHERE ID_DOCUMENTTYPE = 1 ORDER BY KOD, DOC_DATE");
            EducationDocuments = new EntitySet<EducationDocument>(@"WHERE ID_DOCUMENTTYPE IN (3,4,5) ORDER BY KOD, DOC_DATE");
            EnrollmentByUniversityTransferOrders = new EntitySet<EnrollmentByUniversityTransferOrder>(@"WHERE TPR='0015'");
            DisabilityTickets = new EntitySet<DisabilityTicket>(@"WHERE ID_DOCUMENTTYPE = 11");
            DisabilityTypes = new EntitySet<DisabilityType>();
            OrphanCategories = new EntitySet<OrphanCategory>();
            OrphanTickets = new EntitySet<OrphanTicket>(@"WHERE ID_DOCUMENTTYPE = 30");
        }


        #region Entity collections

        public static EntitySet<OrphanCategory> OrphanCategories { get; set; }

        public static EntitySet<OrphanTicket> OrphanTickets { get; set; }

        public static EntitySet<DisabilityType> DisabilityTypes { get; set; }

        public static EntitySet<DisabilityTicket> DisabilityTickets { get; set; }

        public static EntitySet<EnrollmentByUniversityTransferOrder> EnrollmentByUniversityTransferOrders { get; set; }

        public static EntitySet<EducationDocument> EducationDocuments { get; set; }

        public static EntitySet<IdentityDocument> IdentityDocuments { get; set; }

        public static EntitySet<IdentityDocumentType> IdentityDocumentTypes { get; set; }

        public static EntitySet<DocumentType> DocumentTypes { get; set; }

        public static EntitySet<NextCourseTransferOrder> NextCourseTransferOrders { get; set; }

        public static EntitySet<ReinstatementOrder> ReinstatementOrders { get; set; }

        public static EntitySet<AcademicVacationReason> AcademicVacationReasons { get; set; }

        public static EntitySet<AcademicVacationOrder> AcademicVacationOrders { get; set; }

        public static EntitySet<AcademicVacationExitOrder> AcademicVacationExitOrders { get; set; }

        public static EntitySet<UnenrollmentOrder> UnenrollmentOrders { get; set; }

        public static EntitySet<UnenrollmentReason> UnenrollmentReasons { get; set; }

        public static EntitySet<EnrollmentOrder> EnrollmentOrders { get; set; }

        public static EntitySet<OrderType> OrderTypes { get; set; }

        public static EntitySet<EducationDocumentType> EducationDocumentTypes { get; set; }

        public static EntitySet<GrantType> GrantTypes { get; set; }

        public static EntitySet<Student> Students { get; set; }

        public static EntitySet<Group> Groups { get; set; }

        public static EntitySet<EducationProgramType> EducationProgramTypes { get; set; }

        public static EntitySet<Direction> Directions { get; set; }

        public static EntitySet<Faculty> Faculties { get; set; }

        public static EntitySet<EducationForm> EducationForms { get; set; }

        public static EntitySet<EducationPlan> EducationPlans { get; set; }

        public static EntitySet<ForeignLanguage> ForeignLanguages { get; set; }

        public static EntitySet<QuotaType> QuotaTypes { get; set; }

        public static EntitySet<StudentStatus> StudentStatuses { get; set; }

        public static EntitySet<FinanceSource> FinanceSources { get; set; }

        public static EntitySet<Citizenship> Citizenships { get; set; }

        #endregion


        /// <summary>
        /// Точка инициализации контекста
        /// </summary>
        /// <param name="dbConnection">Подготовленное соединение с базой данных</param>
        public static bool Auth(DbConnection dbConnection)
        {
            if (dbConnection == null)
            {
                return false;
            }

            if (dbConnection.State != ConnectionState.Open)
            {
                try
                {
                    dbConnection.Open();
                }
                catch (Exception)
                {
                    throw new Exception("Невозможно открыть соединение с БД.");
                }
            }

            DbConnection = dbConnection;

            InitializeContext();
            PostLoadInitialize();

            return true;
        }

        static DbConnection _dbConnection;

        /// <summary>
        /// Возвращает открытое соединение с базой данных
        /// </summary>
        public static DbConnection DbConnection
        {
            get
            {
                if (_dbConnection.State != ConnectionState.Open)
                {
                    try
                    {
                        _dbConnection.Open();
                    }
                    catch (Exception)
                    {
                        throw new Exception("Invalid database connection.");
                    }
                }
                return _dbConnection;
            }
            private set
            {
                if (value != null)
                {
                    _dbConnection = value;
                }
            }
        }
        
        /// <summary>
        /// Постзагрузочная инициализация сущностей
        /// </summary>
        static void PostLoadInitialize()
        {
            // Получаем все коллекции сущностей
            var type = typeof(Astu);
            var collections = type.GetProperties();

            foreach (var col in collections)
            {
                if (col.PropertyType.GetInterfaces().Contains(typeof(IEntitySet)))
                {
                    // Вызываем их метод Reset
                    var currentCollection = col.GetValue(type, null);
                    var methodRef = col.PropertyType.GetMethod("PostLoadInitialize");
                    methodRef.Invoke(currentCollection, null);
                }
            }
        }
    }
}
