using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Reflection;
using System.Data;
using Model.Astu.Orders;
using Model.Astu.Orders.History;

namespace Model.WorkOk
{
    /// <summary>
    /// Контекст базы данных АлтГТУ
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
            Orders = new EntitySet<Order>("WHERE spfak IN (57,65,98)");
            OrderTypes = new EntitySet<OrderType>();
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
