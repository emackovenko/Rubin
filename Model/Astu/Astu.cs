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
        }


        #region Entity collections

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
            SignToRemoving();

            return true;
        }

        static DbConnection _dbConnection;

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

        static List<Entity> _removedEntities;

        static ICollection<Entity> RemovedEntities
        {
            get
            {
                if (_removedEntities == null)
                {
                    _removedEntities = new List<Entity>();
                }
                return _removedEntities;
            }
        }
        
        public static void Save()
        {
            var sb = new StringBuilder();
            // Получить все коллекции сущностей
            var type = typeof(Astu);
            var collections = type.GetProperties();

            // По каждой коллекции прогоняем следующий трекер:
            foreach (var col in collections)
            {
                if (col.PropertyType.GetInterfaces().Contains(typeof(IEntitySet)))
                {
                    var currentCollection = (IEnumerable<Entity>)col.GetValue(type, null);

                    // Все объекты, у которых свойства имеют статус, отличный от EntityState.Default
                    var filteredCollection = currentCollection.Where(e => e.EntityState != EntityState.Default);

                    // От каждого получаем SQL-команду на изменение в БД
                    foreach (var enitity in filteredCollection)
                    {
                        sb.Append(enitity.GetSaveQuery());
                    }
                }
            }

            if (RemovedEntities.Count > 0)
            {
                foreach (var entity in RemovedEntities)
                {
                    sb.Append(entity.GetSaveQuery());
                }
                RemovedEntities.Clear();
            }

            // выполняем команду, предварительно обернув в безопасную транзакцию
            if (sb.Length > 0)
            {
                var transaction = DbConnection.BeginTransaction();
                var cmd = _dbConnection.CreateCommand();
                cmd.Transaction = transaction;
                cmd.CommandText = sb.ToString();
                try
                {
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    string errorMessage = string.Format("При сохранении данных произошла ошибка. Подробности во внутреннем исключении.\n\nТекст ошибки:\n{0}",
                        e.Message);
                    var exception = new DataException(errorMessage, e);
                    throw exception;
                }
                                                
            }

            // У сущностей выставляем EntityState.Default
            foreach (var col in collections)
            {
                if (col.PropertyType.GetInterfaces().Contains(typeof(IEntitySet)))
                {
                    var currentCollection = (IEnumerable<Entity>)col.GetValue(type, null);

                    // Все объекты, у которых свойства имеют статус, отличный от EntityState.Default
                    var filteredCollection = currentCollection.Where(e => e.EntityState != EntityState.Default);

                    // От каждого получаем SQL-команду на изменение в БД
                    foreach (var enitity in filteredCollection)
                    {
                        enitity.EntityState = EntityState.Default;
                    }
                }
            }
        }

        static void AddToRemovedEntities(Entity entity)
        {
            RemovedEntities.Add(entity);
        }

        /// <summary>
        /// Подписка на события удаления элементов из коллекций
        /// </summary>
        static void SignToRemoving()
        {
            // Получаем все коллекции сущностей
            var type = typeof(Astu);
            var collections = type.GetProperties();

            foreach (var col in collections)
            {
                if (col.PropertyType.GetInterfaces().Contains(typeof(IEntitySet)))
                {
                    // Кидаем в их события метод AddToRemovedEntities()
                    var currentCollection = col.GetValue(type, null);
                    ((IEntitySet)currentCollection).OnEntityRemoving += AddToRemovedEntities;
                }
            }
        }


        static void ResetAll()
        {
            // Получаем все коллекции сущностей
            var type = typeof(Astu);
            var collections = type.GetProperties();

            foreach (var col in collections)
            {
                if (col.PropertyType.GetInterfaces().Contains(typeof(IEntitySet)))
                {
                    // Вызываем их метод Reset
                    var currentCollection = col.GetValue(type, null) as IEntitySet;
                    currentCollection.Reset();
                }
            }
        }
    }
}
