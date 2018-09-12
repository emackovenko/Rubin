using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Data.WorkOk
{
    /// <summary>
    /// Кэш модели
    /// </summary>
    internal static class Cache
    {
        /// <summary>
        /// Кэш свойств первичного ключа
        /// </summary>
        internal static Dictionary<Type, PropertyInfo> PrimaryKeyProperties = new Dictionary<Type, PropertyInfo>();

        /// <summary>
        /// Кэш имен таблиц для типов сущностей
        /// </summary>
        internal static Dictionary<Type, string> TableNames = new Dictionary<Type, string>();

        /// <summary>
        /// Кэш запросов-выборки для типов сущностей
        /// </summary>
        internal static Dictionary<Type, string> SelectQueries = new Dictionary<Type, string>();
    }
}
