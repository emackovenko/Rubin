using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.Common;
using System.Data;

namespace Model.Astu
{
    /// <summary>
    /// Предоставляет набор всех сущностей указанного типа, которые могут быть загружены из базы данных
    /// </summary>
    /// <typeparam name="TEntity">Тип загружаемой сущности</typeparam>
    public class EntitySet<TEntity>: List<TEntity>, IEntitySet where TEntity: Entity
    {

        public event EntityRemovingHandler EntityRemoving;
        
        /// <summary>
        /// Инициализирует коллекцию и загружает ее из БД
        /// </summary>
        /// <param name="databaseModel"></param>
        public EntitySet()
        {
            var dbConnection = Astu.DbConnection;
            var type = typeof(TEntity);

            // Получаем коллекцию загружаемых полей объекта
            var fields = type.GetProperties().Where(pi => pi.GetCustomAttributes(typeof(FieldNameAttribute), true).Count() > 0);

            // Составляем строку запроса
            var sb = new StringBuilder();
            sb.Append("SELECT ");
            foreach (var f in fields)
            {
                if (f.PropertyType == typeof(string))
                {
                    sb.AppendFormat("TRIM({0})",
                        (f.GetCustomAttributes(typeof(FieldNameAttribute), true).First() as FieldNameAttribute).Value);
                }
                else
                {
                    sb.Append((f.GetCustomAttributes(typeof(FieldNameAttribute), true).First() as FieldNameAttribute).Value);
                }                
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1, 1);

            var tableName = (type.GetCustomAttributes(typeof(TableNameAttribute), true).First() as TableNameAttribute).Value;
            sb.AppendFormat(" FROM {0}", tableName);

            // Создаем SQL команду для выполнения запроса и загрузки данных
            var dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = sb.ToString();
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

                            // КОСТЫЛЬ: проверка типов
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
            base.Remove(item);
            item.EntityState = EntityState.Deleted;
            EntityRemoving(item);
        }
    }
}
