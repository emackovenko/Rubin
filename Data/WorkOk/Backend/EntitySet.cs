using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.Common;
using System.Data;

namespace Data.WorkOk
{
    /// <summary>
    /// Предоставляет набор всех сущностей указанного типа, которые могут быть загружены из базы данных
    /// </summary>
    /// <typeparam name="TEntity">Тип загружаемой сущности</typeparam>
    public class EntitySet<TEntity>: List<TEntity>, IEntitySet where TEntity: Entity
    {
        string _sqlOption;        
        
        /// <summary>
        /// Инициализирует коллекцию и загружает ее из БД
        /// </summary>
        /// <param name="sqlOption">Опция будет дописана в финальный запрос "SELECT {Fields} FROM {TableName}"</param>
        public EntitySet(string sqlOption = null) : base ()
        {
            _sqlOption = sqlOption;
            Reset();            
        }        

        /// <summary>
        /// Очищает текущую коллекцию и заново подгружает все элементы из БД
        /// </summary>
        public void Reset()
        {
            Clear();
            var dbConnection = Context.DbConnection;
            var type = typeof(TEntity);            

            // Создаем SQL команду для выполнения запроса и загрузки данных
            var dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = QueryProvider.GetSelectQuery(type, _sqlOption);

            // Получаем коллекцию загружаемых полей объекта
            var fields = type.GetProperties().Where(pi => pi.GetCustomAttributes(typeof(DbFieldInfoAttribute), true).Count() > 0).OrderBy(pi => pi.Name);

            // Выполняем запрос и отображаем реляционные данные в объекты
            using (var reader = dbCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // создаем объект типа
                        var ci = type.GetConstructor(new Type[] { });
                        TEntity record = (TEntity)ci.Invoke(null);
                        int i = 0;
                        foreach (var f in fields)
                        {
                            object value = reader.GetValue(i);

                            // SCRUTCH: проверка типов
                            if (value.GetType() == typeof(DBNull))
                            {
                                value = null;
                            }
                            else
                            {
                                if (value.GetType() == typeof(decimal))
                                {
                                    if (f.PropertyType == typeof(bool))
                                    {
                                        if ((value as decimal?) > 0)
                                        {
                                            value = (bool)true;
                                        }
                                        else
                                        {
                                            value = (bool)false;
                                        }
                                    }
                                    else
                                    {
                                        if (f.PropertyType == typeof(int) || f.PropertyType == typeof(int?))
                                        {
                                            value = Convert.ToInt32(value);
                                        }
                                    }
                                }
                            }

                            f.SetValue(record, value, null);
                            i++;
                        }

                        (record as Entity).EntityState = EntityState.Default;
                        base.Add((TEntity)record);
                    }
                }
            }
        }

        /// <summary>
        /// Удаляет элемент из коллекции. Данный элемент будет удален из БД при вызове DatabaseModel.Save().
        /// </summary>
        /// <param name="item">Удаляемый элемент</param>
        public new void Remove(TEntity item)
        {
            item.Delete();
        }

        /// <summary>
        /// Вызывается при автономном удалении сущности
        /// </summary>
        /// <param name="item">Удаляемая сущность</param>
        internal void AutonomosRemove(TEntity item)
        {
            base.Remove(item);
        }

        /// <summary>
        /// Постзагрузочная инициализация функционала всех сущностей коллекции
        /// </summary>
        public void PostLoadInitialize()
        {
            foreach (var item in this)
            {
                item.PostLoadInitialize();
            }
        }
    }
}
