using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Reflection;
using System.Data;

namespace Data.WorkOk
{
    /// <summary>
    /// Контекст базы данных WorkOk
    /// </summary>
    public static class Context
    {

        /// <summary>
        /// Загружает все коллекции сущностей из БД в память
        /// </summary>
        static void InitializeContext()
        {
            Students = new EntitySet<Student>();
            Groups = new EntitySet<Group>();
            Directions = new EntitySet<Direction>();
            Faculties = new EntitySet<Faculty>();
            Statuses = new EntitySet<Status>();
            FinanceSources = new EntitySet<FinanceSource>();
            EducationForms = new EntitySet<EducationForm>();
            Orders = new EntitySet<Order>();
            OrderTypes = new EntitySet<OrderType>();
            IdentityDocumentTypes = new EntitySet<IdentityDocumentType>();
            Citizenships = new EntitySet<Citizenship>();
            StudentInfoSets = new EntitySet<StudentInfo>();
            StudentContracts = new EntitySet<StudentContract>();
            PaymentPeriodTypes = new EntitySet<PaymentPeriodType>();
            PersonContragents = new EntitySet<PersonContragent>("WHERE tip = 1");
            OrganizationContragents = new EntitySet<OrganizationContragent>("WHERE tip = 2");
            PaymentBalances = new EntitySet<PaymentBalance>();
            PaymentDelays = new EntitySet<PaymentDelay>();
            AgreementTypes = new EntitySet<AgreementType>();
            StudentNameChangingOrders = new EntitySet<StudentNameChangingOrder>();
            AuxAgreements = new EntitySet<AuxAgreement>();
            //StudentNameChangingAgreements = new EntitySet<StudentNameChangingAgreement>("WHERE vid = 7");
            //PriceChangingAgreements = new EntitySet<PriceChangingAgreement>("WHERE vid = 8");
            //ContractPeriodChangingAgreements = new EntitySet<ContractPeriodChangingAgreement>("WHERE vid = 9");
            //ContractTerminationAgreements = new EntitySet<ContractTerminationAgreement>("WHERE vid = 10");
            //ContractPriceAndPeriodChangingAgreements = new EntitySet<ContractPriceAndPeriodChangingAgreement>("WHERE vid = 11");
            //ContractTerminationWithObligationAgreements = new EntitySet<ContractTerminationWithObligationAgreement>("WHERE vid = 12");
            //ContragentChangingAgreements = new EntitySet<ContragentChangingAgreement>("WHERE vid = 13");
            //FastTrainingAgreements = new EntitySet<FastTrainingAgreement>("WHERE vid = 14");
        }

        #region Entity collections

        public static EntitySet<Order> Orders { get; set; }
        public static EntitySet<OrderType> OrderTypes { get; set; }
        public static EntitySet<Student> Students { get; set; }
        public static EntitySet<Group> Groups { get; set; }
        public static EntitySet<Direction> Directions { get; set; }
        public static EntitySet<Faculty> Faculties { get; set; }
        public static EntitySet<Status> Statuses { get; set; }
        public static EntitySet<FinanceSource> FinanceSources { get; set; }
        public static EntitySet<EducationForm> EducationForms { get; set; }
        public static EntitySet<IdentityDocumentType> IdentityDocumentTypes { get; set; }
        public static EntitySet<Citizenship> Citizenships { get; set; }
        public static EntitySet<StudentInfo> StudentInfoSets { get; set; }
        public static EntitySet<StudentContract> StudentContracts { get; set; }
        public static EntitySet<PaymentPeriodType> PaymentPeriodTypes { get; set; }
        public static EntitySet<PersonContragent> PersonContragents { get; set; }
        public static EntitySet<OrganizationContragent> OrganizationContragents { get; set; }
        public static EntitySet<PaymentBalance> PaymentBalances { get; set; }
        public static EntitySet<PaymentDelay> PaymentDelays { get; set; }
        public static EntitySet<AgreementType> AgreementTypes { get; set; }
        public static EntitySet<StudentNameChangingOrder> StudentNameChangingOrders { get; set; }
        public static EntitySet<AuxAgreement> AuxAgreements { get; set; }
        //public static EntitySet<StudentNameChangingAgreement> StudentNameChangingAgreements { get; set; }
        //public static EntitySet<PriceChangingAgreement> PriceChangingAgreements { get; set; }
        //public static EntitySet<ContractPeriodChangingAgreement> ContractPeriodChangingAgreements { get; set; }
        //public static EntitySet<ContractTerminationAgreement> ContractTerminationAgreements { get; set; }
        //public static EntitySet<ContractTerminationWithObligationAgreement> ContractTerminationWithObligationAgreements { get; set; }
        //public static EntitySet<ContractPriceAndPeriodChangingAgreement> ContractPriceAndPeriodChangingAgreements { get; set; }
        //public static EntitySet<ContragentChangingAgreement> ContragentChangingAgreements { get; set; }
        //public static EntitySet<FastTrainingAgreement> FastTrainingAgreements { get; set; }

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
            var type = typeof(Context);
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
