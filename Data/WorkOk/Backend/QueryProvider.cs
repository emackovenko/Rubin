using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Data.WorkOk
{
    /// <summary>
    /// Провайдер SQL-запросов к базе данных
    /// </summary>
    internal static class QueryProvider
    {
        /// <summary>
        /// Возвращает строку запроса-выборки для типа сущности
        /// </summary>
        /// <param name="entityType">Тип загружаемой сущности</param>
        /// <param name="sqlOption">Опция, которая будет дописана в финальный запрос "SELECT {Fields} FROM {TableName}"</param>
        internal static string GetSelectQuery(Type entityType, string sqlOption = null)
        {
            if (entityType == null)
            {
                throw new ArgumentNullException("entityType");
            }

            if (!Cache.SelectQueries.TryGetValue(entityType, out string query))
            {
                // Получаем коллекцию загружаемых полей объекта
                var fields = entityType.GetProperties().Where(pi => pi.GetCustomAttributes(typeof(DbFieldInfoAttribute), true).Count() > 0).OrderBy(pi => pi.Name);

                // Составляем строку запроса
                var sb = new StringBuilder();
                sb.Append("SELECT ");
                foreach (var f in fields)
                {
                    if (f.PropertyType == typeof(string))
                    {
                        sb.AppendFormat("TRIM({0})",
                            (f.GetCustomAttributes(typeof(DbFieldInfoAttribute), true).First() as DbFieldInfoAttribute).Name);
                    }
                    else
                    {
                        sb.Append((f.GetCustomAttributes(typeof(DbFieldInfoAttribute), true).First() as DbFieldInfoAttribute).Name);
                    }
                    sb.Append(",");
                }
                sb.Remove(sb.Length - 1, 1);

                var tableName = (entityType.GetCustomAttributes(typeof(TableNameAttribute), true).First() as TableNameAttribute).Value;
                sb.AppendFormat(" FROM {0}", tableName);

                // докидывем условие, если оно есть
                if (!string.IsNullOrWhiteSpace(sqlOption))
                {
                    sb.AppendFormat(" {0}", sqlOption);
                }
                query = sb.ToString();
            }
            return query;
        }

        /// <summary>
        /// Возвращает строку запроса-вставки для сущности
        /// </summary>
        /// <param name="entity">Вставляемая сущность</param>
        /// <returns></returns>
        internal static string GetInsertQuery(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var entityType = entity.GetType();

            // Если идентификатор пустой, то проставляем ему значение
            var primProp = GetPrimaryKeyPropertyInfo(entityType);
            if (primProp == null)
            {
                throw new ArgumentNullException("Primary key not found");
            }
            if (primProp.GetValue(entity, null) == null)
            {
                primProp.SetValue(entity, Convert.ChangeType(GetNewGeneratedId(entityType), primProp.PropertyType), null);
            }

            var sb = new StringBuilder();

            sb.AppendFormat("INSERT INTO {0} ", GetEntityTableName(entityType));
            sb.Append("(");

            // Получаем набор свойств с аттрибутом FieldName
            var props = entity.GetType().GetProperties().Where(pi => pi.GetCustomAttributes(typeof(DbFieldInfoAttribute), true).Count() > 0);
            foreach (var prop in props)
            {
                var fieldName = prop.GetCustomAttributes(typeof(DbFieldInfoAttribute), true).First() as DbFieldInfoAttribute;
                sb.AppendFormat("{0},", fieldName.Name);
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append(") VALUES (");

            foreach (var prop in props)
            {
                sb.AppendFormat("{0},", 
                    ConvertObjectToExpression(GetDatabaseFieldType(entityType, prop.Name), entityType.GetProperty(prop.Name).GetValue(entity, null)));
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append(")");

            return sb.ToString();
        }

        /// <summary>
        /// Возвращает строку запроса-удаления для сущности
        /// </summary>
        /// <param name="entity">Удаляемая сущность</param>
        /// <returns></returns>
        static string GetDeleteQuery(Entity entity)
        {
            var entityType = entity.GetType();
            return string.Format("DELETE FROM {0} WHERE {1}={2}",
                GetEntityTableName(entityType), GetPrimaryFieldName(entityType), 
                ConvertObjectToExpression(GetDatabaseFieldType(entityType, GetPrimaryKeyPropertyInfo(entityType).Name), GetPrimaryKeyPropertyInfo(entityType).GetValue(entity, null)));
        }
        
        /// <summary>
        /// Возвращает строку запроса-обновления для сущности
        /// </summary>
        /// <param name="entity">Обновляемая сущность</param>
        /// <returns></returns>
        static string GetUpdateQuery(Entity entity)
        {
            var entityType = entity.GetType();

            var sb = new StringBuilder();
            sb.AppendFormat("UPDATE {0} ", GetEntityTableName(entityType));
            sb.Append("SET ");
            // Получаем набор свойств с аттрибутом FieldName 
            var props = entityType.GetProperties().Where(pi =>
            (pi.GetCustomAttributes(typeof(DbFieldInfoAttribute), true).Count() > 0 &&
            pi.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Count() == 0));

            foreach (var prop in props)
            {
                var fieldName = prop.GetCustomAttributes(typeof(DbFieldInfoAttribute), true).First() as DbFieldInfoAttribute;
                sb.AppendFormat("{0}={1},",
                    fieldName.Name,
                    ConvertObjectToExpression(GetDatabaseFieldType(entityType, prop.Name),
                    prop.GetValue(entity, null)));
            }
            sb.Remove(sb.Length - 1, 1);
            sb.AppendFormat(" WHERE {0}={1}",
                GetPrimaryFieldName(entityType),
                ConvertObjectToExpression(GetDatabaseFieldType(entityType, GetPrimaryKeyPropertyInfo(entityType).Name), GetPrimaryKeyPropertyInfo(entityType).GetValue(entity, null)));
            return sb.ToString();
        }

        /// <summary>
        /// Возвращает запрос для сохранения текущего состояния сущности
        /// </summary>
        /// <param name="entity">Сохраняемая сущность</param>
        /// <returns></returns>
        internal static string GetSaveQuery(Entity entity)
        {
            string str = string.Empty;
            switch (entity.EntityState)
            {
                case EntityState.Default:
                    break;
                case EntityState.New:
                    str = GetInsertQuery(entity);
                    break;
                case EntityState.Changed:
                    str = GetUpdateQuery(entity);
                    break;
                case EntityState.Deleted:
                    str = GetDeleteQuery(entity);
                    break;
                default:
                    throw new System.ComponentModel.InvalidEnumArgumentException("Unknown entity state");
            }

            return str;
        }

        /// <summary>
        /// Возвращает строковое представление объекта в формате SQL выражения
        /// </summary>
        /// <param name="databaseFieldType">Тип поля объекта в БД</param>
        /// <param name="value">Сам объект</param>
        /// <returns></returns>
        static string ConvertObjectToExpression(DbFieldType databaseFieldType, object value)
        {
            if (value == null)
            {
                return "NULL";
            }
            switch (databaseFieldType)
            {
                case DbFieldType.String:
                    return string.Format(@"'{0}'", value.ToString());
                case DbFieldType.Integer:
                    return value.ToString();
                case DbFieldType.Double:
                    return value.ToString();
                case DbFieldType.DateTime:
                    return string.Format(@"STR_TO_DATE('{0}', '%Y%m%d')", ((DateTime)value).ToString("yyyyMMdd"));
                case DbFieldType.Boolean:
                    return ((bool)value) == true ? "1" : "0";
                default:
                    throw new InvalidOperationException("Non-registered field type.");
            }
        }

        /// <summary>
        /// Возвращает имя сопоставляемой таблицы для типа сущности
        /// </summary>
        /// <param name="entityType">Тип сущности</param>
        /// <returns></returns>
        static string GetEntityTableName(Type entityType)
        {
            if (!Cache.TableNames.TryGetValue(entityType, out string result))
            {
                var tableNameAttribute = entityType.GetCustomAttributes(typeof(TableNameAttribute), true).FirstOrDefault() as TableNameAttribute;

                if (tableNameAttribute == null)
                {
                    throw new Exception(string.Format("Для сущности {0} не задана сопоставляемая таблица в БД.", entityType.Name));
                }

                result = tableNameAttribute.Value;
                Cache.TableNames.Add(entityType, result);
            }
            
            return result;
        }

        /// <summary>
        /// Возвращает сопоставляемый тип поля в таблице базы данных для указанного свойства
        /// </summary>
        /// <param name="entityType">Тип сущности</param>
        /// <param name="propertyName">Имя заданного свойства</param>
        /// <returns></returns>
        static DbFieldType GetDatabaseFieldType(Type entityType, string propertyName)
        {
            var p = entityType.GetProperty(propertyName).GetCustomAttributes(typeof(DbFieldInfoAttribute), true).FirstOrDefault() as DbFieldInfoAttribute;
            if (p == null)
            {
                throw new Exception(string.Format("Для свойства {0} не указано сопоставляемое поле.", propertyName));
            }
            return p.Type;
        }

        /// <summary>
        /// Возвращает метаданные свойства, спопоставляемого первичному ключу в таблице БД.
        /// </summary>
        /// <param name="entityType">Тип сущности</param>
        /// <returns></returns>
        internal static PropertyInfo GetPrimaryKeyPropertyInfo(Type entityType)
        {
            if (!Cache.PrimaryKeyProperties.TryGetValue(entityType, out PropertyInfo result))
            {
                var props = entityType.GetProperties();
                foreach (var prop in props)
                {
                    if (prop.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Count() > 0)
                    {
                        Cache.PrimaryKeyProperties.Add(entityType, prop);
                        return prop;
                    }
                }
            }
            else
            {
                return result;
            }
            throw new InvalidOperationException("Для сущности не указано поле первичного ключа");
        }

        /// <summary>
        /// Возвращает имя поля первичного ключа в таблице БД.
        /// </summary>
        /// <param name="entityType">Тип сущности</param>
        /// <returns></returns>
        static string GetPrimaryFieldName(Type entityType)
        {
            var fieldName = GetPrimaryKeyPropertyInfo(entityType).GetCustomAttributes(typeof(DbFieldInfoAttribute), true).FirstOrDefault() as DbFieldInfoAttribute;
            
            return fieldName?.Name;
        }

        /// <summary>
        /// Если значение поля первичного ключа пустое, возвращает новый идентификатор
        /// </summary>
        /// <returns></returns>
        internal static object GetNewGeneratedId(Type entityType)
        {
            var props = entityType.GetProperties();

            // Составляем запрос для получения максимального ид
            string query = string.Format("SELECT MAX({0}) + 1 FROM {1}",
                GetPrimaryFieldName(entityType), GetEntityTableName(entityType));

            object id = null;

            using (var transaction = Context.DbConnection.BeginTransaction())
            {
                var cmd = Context.DbConnection.CreateCommand();
                cmd.Transaction = transaction;
                cmd.CommandText = query;
                id = cmd.ExecuteScalar();
            }

            return id;
        }

    }
}
