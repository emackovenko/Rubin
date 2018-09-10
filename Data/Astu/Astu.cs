using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Reflection;
using System.Data;
using Data.Astu.Orders;
using Data.Astu.Orders.History;
using System.Collections;

namespace Data.Astu
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
            DocumentTypes = new EntitySet<DocumentType>();
            IdentityDocumentTypes = new EntitySet<IdentityDocumentType>();
            IdentityDocuments = new EntitySet<IdentityDocument>(@"WHERE ID_DOCUMENTTYPE = 1 ORDER BY KOD, DOC_DATE");
            EducationDocuments = new EntitySet<EducationDocument>(@"WHERE ID_DOCUMENTTYPE IN (3,4,5) ORDER BY KOD, DOC_DATE");
            DisabilityTickets = new EntitySet<DisabilityTicket>(@"WHERE ID_DOCUMENTTYPE = 11");
            DisabilityTypes = new EntitySet<DisabilityType>();
            OrphanCategories = new EntitySet<OrphanCategory>();
            OrphanTickets = new EntitySet<OrphanTicket>(@"WHERE ID_DOCUMENTTYPE = 30");

            AcademicVacationExitOrders = new EntitySet<AcademicVacationExitOrder>(@"WHERE TPR='0007' ORDER BY KOD, DAT");
            AcademicVacationOrders = new EntitySet<AcademicVacationOrder>(@"WHERE TPR='0002' ORDER BY KOD, DAT");
            EnrollmentByUniversityTransferOrders = new EntitySet<EnrollmentByUniversityTransferOrder>(@"WHERE TPR='0015'");
            EnrollmentOrders = new EntitySet<EnrollmentOrder>(@"WHERE TPR='0001' ORDER BY KOD, DAT");
            NextCourseTransferOrders = new EntitySet<NextCourseTransferOrder>(@"WHERE TPR IN ('0030', '0033') ORDER BY KOD,DAT");
            ReinstatementOrders = new EntitySet<ReinstatementOrder>(@"WHERE TPR='0006' ORDER BY KOD, DAT");
            UnenrollmentOrders = new EntitySet<UnenrollmentOrder>(@"WHERE TPR='0003' ORDER BY KOD, DAT");
            ChildrenFussVacationExitOrders = new EntitySet<ChildrenFussVacationExitOrder>(@"WHERE TPR='0029' ORDER BY KOD, DAT");
            ChildrenFussVacationOrders = new EntitySet<ChildrenFussVacationOrder>(@"WHERE TPR IN ('0022', '0023', '0024', '0027', '0028') ORDER BY KOD, DAT");
            DirectionChangingOrders = new EntitySet<DirectionChangingOrder>(@"WHERE TPR='0011' ORDER BY KOD, DAT");
            EnrollToFullStateProvisionOrders = new EntitySet<EnrollToFullStateProvisionOrder>(@"WHERE TPR='0017' ORDER BY KOD, DAT");
            FinanceSourceChangingOrders = new EntitySet<FinanceSourceChangingOrder>(@"WHERE TPR='0012' ORDER BY KOD, DAT");
            GraduationOrders = new EntitySet<GraduationOrder>(@"WHERE TPR='0005' ORDER BY KOD, DAT");
            GroupTransferOrders = new EntitySet<GroupTransferOrder>(@"WHERE TPR='0008' ORDER BY KOD, DAT");
            //StudentNameChangingOrders = new EntitySet<StudentNameChangingOrder>(@"WHERE TPR='0013' ORDER BY KOD, DAT");
            OtherOrders = new EntitySet<OtherOrder>(@"WHERE TPR='0009' ORDER BY KOD, DAT");
            TransferToAcceleratedEducationOrders = new EntitySet<TransferToAcceleratedEducationOrder>(@"WHERE TPR='0034' ORDER BY KOD, DAT");

            Orders = new EntitySet<Order>();
            AcademicVacationExitItems = new EntitySet<AcademicVacationExitItem>(@"WHERE TPR='0007'");
            AcademicVacationItems = new EntitySet<AcademicVacationItem>(@"WHERE TPR='0002'");
            EnrollmentByUniversityTransferItems = new EntitySet<EnrollmentByUniversityTransferItem>(@"WHERE TPR='0015'");
            EnrollmentItems = new EntitySet<EnrollmentItem>(@"WHERE TPR='0001'");
            ReinstatementItems = new EntitySet<ReinstatementItem>(@"WHERE TPR='0006'");
            UnenrollmentItems = new EntitySet<UnenrollmentItem>(@"WHERE TPR='0003'");
            ChildrenFussVacationExitItems = new EntitySet<ChildrenFussVacationExitItem>(@"WHERE TPR='0029'");
            ChildrenFussVacationItems = new EntitySet<ChildrenFussVacationItem>(@"WHERE TPR IN ('0022', '0023', '0024', '0027', '0028')");
            DirectionChangingItems = new EntitySet<DirectionChangingItem>(@"WHERE TPR='0011'");
            EnrollToFullStateProvisionItems = new EntitySet<EnrollToFullStateProvisionItem>(@"WHERE TPR='0017'");
            FinanceSourceChangingItems = new EntitySet<FinanceSourceChangingItem>(@"WHERE TPR='0012'");
            GraduationItems = new EntitySet<GraduationItem>(@"WHERE TPR='0005'");
            GroupTransferItems = new EntitySet<GroupTransferItem>(@"WHERE TPR='0008'");
            StudentNameChangingItems = new EntitySet<StudentNameChangingItem>(@"WHERE TPR='0013'");
            OtherItems = new EntitySet<OtherItem>(@"WHERE TPR='0009'");
            TransferToAcceleratedEducationItems = new EntitySet<TransferToAcceleratedEducationItem>(@"WHERE TPR='0034'");
        }

        #region Entity collections

        public static EntitySet<AcademicVacationExitItem> AcademicVacationExitItems { get; set; }

        public static EntitySet<AcademicVacationItem> AcademicVacationItems { get; set; }

        public static EntitySet<ChildrenFussVacationExitItem> ChildrenFussVacationExitItems { get; set; }

        public static EntitySet<ChildrenFussVacationItem> ChildrenFussVacationItems { get; set; }

        public static EntitySet<DirectionChangingItem> DirectionChangingItems { get; set; }

        public static EntitySet<EnrollmentByUniversityTransferItem> EnrollmentByUniversityTransferItems { get; set; }

        public static EntitySet<EnrollmentItem> EnrollmentItems { get; set; }

        public static EntitySet<EnrollToFullStateProvisionItem> EnrollToFullStateProvisionItems { get; set; }

        public static EntitySet<FinanceSourceChangingItem> FinanceSourceChangingItems { get; set; }

        public static EntitySet<GraduationItem> GraduationItems { get; set; }

        public static EntitySet<GroupTransferItem> GroupTransferItems { get; set; }

        public static EntitySet<StudentNameChangingItem> StudentNameChangingItems { get; set; }

        public static EntitySet<OtherItem> OtherItems { get; set; }

        public static EntitySet<ReinstatementItem> ReinstatementItems { get; set; }

        public static EntitySet<TransferToAcceleratedEducationItem> TransferToAcceleratedEducationItems { get; set; }

        public static EntitySet<UnenrollmentItem> UnenrollmentItems { get; set; }

        public static EntitySet<Order> Orders { get; set; }

        public static EntitySet<AcademicVacationExitOrder> AcademicVacationExitOrders { get; set; }

        public static EntitySet<AcademicVacationOrder> AcademicVacationOrders { get; set; }

        public static EntitySet<ChildrenFussVacationExitOrder> ChildrenFussVacationExitOrders { get; set; }

        public static EntitySet<ChildrenFussVacationOrder> ChildrenFussVacationOrders { get; set; }

        public static EntitySet<DirectionChangingOrder> DirectionChangingOrders { get; set; }

        public static EntitySet<EnrollmentByUniversityTransferOrder> EnrollmentByUniversityTransferOrders { get; set; }

        public static EntitySet<EnrollmentOrder> EnrollmentOrders { get; set; }

        public static EntitySet<EnrollToFullStateProvisionOrder> EnrollToFullStateProvisionOrders { get; set; }

        public static EntitySet<FinanceSourceChangingOrder> FinanceSourceChangingOrders { get; set; }

        public static EntitySet<GraduationOrder> GraduationOrders { get; set; }

        public static EntitySet<GroupTransferOrder> GroupTransferOrders { get; set; }

       // public static EntitySet<StudentNameChangingOrder> StudentNameChangingOrders { get; set; }

        public static EntitySet<NextCourseTransferOrder> NextCourseTransferOrders { get; set; }

        public static EntitySet<OtherOrder> OtherOrders { get; set; }

        public static EntitySet<ReinstatementOrder> ReinstatementOrders { get; set; }

        public static EntitySet<TransferToAcceleratedEducationOrder> TransferToAcceleratedEducationOrders { get; set; }

        public static EntitySet<UnenrollmentOrder> UnenrollmentOrders { get; set; }

        public static EntitySet<AcademicVacationReason> AcademicVacationReasons { get; set; }

        public static EntitySet<UnenrollmentReason> UnenrollmentReasons { get; set; }

        public static EntitySet<OrphanCategory> OrphanCategories { get; set; }

        public static EntitySet<OrphanTicket> OrphanTickets { get; set; }

        public static EntitySet<DisabilityType> DisabilityTypes { get; set; }

        public static EntitySet<DisabilityTicket> DisabilityTickets { get; set; }

        public static EntitySet<EducationDocument> EducationDocuments { get; set; }

        public static EntitySet<IdentityDocument> IdentityDocuments { get; set; }

        public static EntitySet<IdentityDocumentType> IdentityDocumentTypes { get; set; }

        public static EntitySet<DocumentType> DocumentTypes { get; set; }

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
                    // Вызываем их метод PostLoadInitialize
                    var currentCollection = col.GetValue(type, null);
                    var methodRef = col.PropertyType.GetMethod("PostLoadInitialize");
                    methodRef.Invoke(currentCollection, null);
                }
            }
        }
    }
}
