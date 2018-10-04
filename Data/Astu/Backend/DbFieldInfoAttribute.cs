using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu
{
    /// <summary>
    /// Информация о сопоставляемом поле таблицы базы данных
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DbFieldInfoAttribute: Attribute
    {
        public string Name { get; set; }
        public DbFieldType Type { get; set; }

        /// <summary>
        /// Сопоставляет свойству поле таблицы БД с именем fieldName и указывает ему тип DatabaseFieldType.String
        /// </summary>
        /// <param name="fieldName">Имя сопоставляемого поля</param>
        public DbFieldInfoAttribute(string fieldName)
        {
            Name = fieldName;
            Type = DbFieldType.String;
        }

        
        /// <summary>
        /// Сопоставляет свойству поле таблицы БД с именем fieldName и указывает ему тип fieldType
        /// </summary>
        /// <param name="fieldName">Имя сопоставляемого поля</param>
        /// <param name="fieldType">Тип сопоставляемого поля</param>
        public DbFieldInfoAttribute(string fieldName, DbFieldType fieldType)
            : this(fieldName)
        {
            Type = fieldType;
        }
    }
}
