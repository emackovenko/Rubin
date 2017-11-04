using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.Astu
{
	/// <summary>
	/// Предоставляет базовый функционал для сущности
	/// </summary> 
	public abstract class Entity: PropertyChangedBase
	{
        public Entity()
        {
            PropertyChanged += OnPropertyChanged;
        }

        #region Служебные поля

        EntityState _entityState = EntityState.New;

        [NoMagic]
        /// <summary>
        /// Физическое состояние сущности
        /// </summary>
        public EntityState EntityState
        {
            get
            {
                return _entityState;
            }
            internal set
            {
                _entityState = value;
            }
        }
		
        /// <summary>
        /// Возвращает имя таблицы в БД, соответствующей данной сущности
        /// </summary>
        string TableName
        {
            get
            {
                var attributeList = this.GetType().GetCustomAttributes(typeof(TableNameAttribute), true);
                if (attributeList.Count() == 0)
                {
                    throw new Exception();
                }
                var p = attributeList.First() as TableNameAttribute;
                return p.Value;
            }
        }

        /// <summary>
        /// Имя поля первичного ключа
        /// </summary>
        string PrimaryFieldName
        {
            get
            {
                var props = this.GetType().GetProperties();
                foreach (var prop in props)
                {
                    if (prop.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Count() > 0)
                    {
                        var fieldName = prop.GetCustomAttributes(typeof(FieldNameAttribute), true).FirstOrDefault() as FieldNameAttribute;
                        if (fieldName == null)
                        {
                            throw new ArgumentNullException(string.Format("Для свойства {0} не указан сопоставляемый тип", prop.Name));
                        }
                        return fieldName.Value;
                    }
                }
                throw new InvalidOperationException("Для таблицы не указано поле первичного ключа");
            }
        }
        
        #endregion

        #region Sql методы

        internal string GetSaveQuery()
        {
            switch (EntityState)
            {
                case EntityState.Default:
                    return string.Empty;
                case EntityState.New:
                    return InsertQuery();
                case EntityState.Changed:
                    return UpdateQuery();
                case EntityState.Deleted:
                    return DeleteQuery();
                default:
                    throw new InvalidEnumArgumentException("Unknown entity state");
            }

        }
        
        string DeleteQuery()
        {
            return string.Format("DELETE FROM {0} WHERE {1}={2};", 
                TableName, PrimaryFieldName, ConvertObjectToExpression(GetDatabaseFieldType("Id"), GetPropertyInfo("Id").GetValue(this, null)));
        }

        string InsertQuery()
        {
            var sb = new StringBuilder();

            sb.AppendFormat("INSERT INTO {0} ", TableName);
            sb.Append("(");

            // Получаем набор свойств с аттрибутом FieldName
            var props = this.GetType().GetProperties().Where(pi => pi.GetCustomAttributes(typeof(FieldNameAttribute), true).Count() > 0);
            foreach (var prop in props)
            {
                var fieldName = prop.GetCustomAttributes(typeof(FieldNameAttribute), true).First() as FieldNameAttribute;
                sb.AppendFormat("{0},", fieldName.Value);
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(") VALUES (");

            foreach (var prop in props)
            {
                sb.AppendFormat("{0},",ConvertObjectToExpression(GetDatabaseFieldType(prop.Name), GetPropertyInfo(prop.Name).GetValue(this, null)));
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append(");");

            return sb.ToString();
        }

        string UpdateQuery()
        {
            var primaryKey = this.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Count() > 0).FirstOrDefault();
            if (primaryKey == null)
            {
                throw new InvalidOperationException("Не указано поле первичного ключа");
            }

            var sb = new StringBuilder();
            sb.AppendFormat("UPDATE {0} ", TableName);
            sb.Append("SET ");
            // Получаем набор свойств с аттрибутом FieldName 
            var props = this.GetType().GetProperties().Where(pi => 
            (pi.GetCustomAttributes(typeof(FieldNameAttribute), true).Count() > 0 && 
            pi.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Count() == 0));

            foreach (var prop in props)
            {
                var fieldName = prop.GetCustomAttributes(typeof(FieldNameAttribute), true).First() as FieldNameAttribute;
                sb.AppendFormat("{0}={1},", 
                    fieldName.Value, 
                    ConvertObjectToExpression(GetDatabaseFieldType(prop.Name), 
                    GetPropertyInfo(prop.Name).GetValue(this, null)));
            }
            sb.Remove(sb.Length - 1, 1);
            sb.AppendFormat(" WHERE {0}={1};",
                PrimaryFieldName, 
                ConvertObjectToExpression(GetDatabaseFieldType(primaryKey.Name), GetPropertyInfo(primaryKey.Name).GetValue(this, null)));
            return sb.ToString();
        }
        
        #endregion

        #region Служебные и вспомогательные методы

        /// <summary>
        /// Возвращает строковое представление объекта в формате SQL выражения
        /// </summary>
        /// <param name="databaseFieldType">Тип поля объекта в БД</param>
        /// <param name="value">Сам объекта</param>
        /// <returns></returns>
        string ConvertObjectToExpression(DatabaseFieldType databaseFieldType, object value)
        {
            if (value == null)
            {
                return "NULL";
            }
            switch (databaseFieldType)
            {
                case DatabaseFieldType.String:
                    return string.Format(@"'{0}'", value.ToString());
                case DatabaseFieldType.Integer:
                    return value.ToString();
                case DatabaseFieldType.Double:
                    return value.ToString();
                case DatabaseFieldType.DateTime:
                    return string.Format(@"'{0}'", ((DateTime)value).ToString("yyyy-MM-dd"));
                case DatabaseFieldType.OracleDateTime:
                    return string.Format(@"TO_DATE('{0}', 'YYYYMMDD')", ((DateTime)value).ToString("yyyyMMdd"));
                case DatabaseFieldType.Boolean:
                    return ((bool)value) == true ? "1" : "0";
                default:
                    throw new InvalidOperationException("Non-registered field type.");
            }
        }

        /// <summary>
        /// Возвращает сопоставляемый тип поля в таблице базы данных для указанного свойства
        /// </summary>
        /// <param name="propertyName">Имя заданного свойства</param>
        /// <returns></returns>
        DatabaseFieldType GetDatabaseFieldType(string propertyName)
        {
            var p = this.GetType().GetProperty(propertyName).GetCustomAttributes(typeof(FieldTypeAttribute), true).FirstOrDefault() as FieldTypeAttribute;
            if (p == null)
            {
                throw new Exception(string.Format("Для свойства {0} не указан сопоставляемый тип.", propertyName));
            }
            return p.Value;
        }

        PropertyInfo GetPropertyInfo(string propertyName)
        {
            return this.GetType().GetProperty(propertyName);
        }

        void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_entityState == EntityState.Default)
            {
                _entityState = EntityState.Changed;
            }
        }

        #endregion
    }	
}
