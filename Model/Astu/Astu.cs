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
        }


        #region Entity collections

        public static EntitySet<Student> Students { get; set; }

        public static EntitySet<Group> Groups { get; set; }

        public static EntitySet<EducationProgramType> EducationProgramTypes { get; set; }

        public static EntitySet<Direction> Directions { get; set; }

        public static EntitySet<Faculty> Faculties { get; set; }

        public static EntitySet<EducationForm> EducationForms { get; set; }

        public static EntitySet<EducationPlan> EducationPlans { get; set; }

        public static EntitySet<ForeignLanguage> ForeignLanguages { get; set; }

        public static EntitySet<QuotaType> QuotaTypes { get; set; }

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

            // выполняем команду
            if (sb.Length > 0)
            {
                var cmd = _dbConnection.CreateCommand();
                cmd.CommandText = sb.ToString();
                OnSaveChanges(sb.ToString());
                cmd.ExecuteNonQuery();
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
                    ((IEntitySet)currentCollection).EntityRemoving += AddToRemovedEntities;
                }
            }
        }

        public static event ModelSaveChangesHandler OnSaveChanges;
    }
}
