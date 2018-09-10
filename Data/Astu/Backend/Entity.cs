using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.Astu
{
	/// <summary>
	/// Предоставляет базовый функционал для сущности
	/// </summary> 
	public abstract class Entity: PropertyChangedBase, ICloneable
	{
        public Entity()
        {
            IsLoaded = false;
            PostLoadInitialize();
        }

        internal bool IsLoaded { get; set; }

        private Entity _backup;
        
        EntityState _entityState = EntityState.New;

        /// <summary>
        /// Физическое состояние сущности
        /// </summary>
        [NoMagic]
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
        
        public string GetSaveQuery()
        {
            return string.Format("{0};", QueryProvider.GetSaveQuery(this));
        }

        /// <summary>
        /// Сохраняет текущее состояние сущности в базе данных
        /// </summary>
        public void Save()
        {
            if (EntityState == EntityState.Default)
            {
                return;
            }

            var cmd = Astu.DbConnection.CreateCommand();
            cmd.CommandText = QueryProvider.GetSaveQuery(this);
            using (var transaction = Astu.DbConnection.BeginTransaction())
            {
                try
                {
                    // выполняем команду
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    
                    // Помечаем сущность как дефолтную и пересоздаем бэкап
                    EntityState = EntityState.Default;
                    _backup = Clone() as Entity;
                }
                catch (Exception e)
                {
                    string errorMessage = string.Format("При сохранении сущности произошла ошибка.\nТекст SQL:\n{0}\n\nТекст ошибки:\n{1}",
                        cmd.CommandText, e.Message);
                    transaction.Rollback();
                    throw new DataException(errorMessage, e);
                }
            }
        }

        /// <summary>
        /// Производит удаление сущности из контекста и помечает сущность как удаленную.
        /// </summary>
        public void Delete()
        {
            // Ищем коллекцию, содержащую наш тип
            var contextType = typeof(Astu);
            var entitySetType = typeof(EntitySet<>);
            var searchedType = entitySetType.MakeGenericType(GetType());
            var parentalCollectionPropertyInfo = contextType.GetProperties().FirstOrDefault(pi => pi.PropertyType == searchedType);

            if (parentalCollectionPropertyInfo == null)
            {
                throw new MissingFieldException("Данный тип не был загружен контекстом.", searchedType.Name);
            }

            // получаем коллекцию и вызываем метод удаления
            var collection = parentalCollectionPropertyInfo.GetValue(contextType, null);
            var method = collection.GetType().GetMethod("Remove", new Type[] { GetType() });
            method.Invoke(collection, new object[] { this });
        }

        /// <summary>
        /// Обработчик изменения состояния сущности
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_entityState == EntityState.Default)
            {
                if (_backup != null)
                {
                    var selfValue = GetType().GetProperty(e.PropertyName).GetValue(this, null);
                    var backupValue = GetType().GetProperty(e.PropertyName).GetValue(_backup, null);
                    if (!selfValue?.Equals(backupValue) ?? false)
                    {
                        _entityState = EntityState.Changed;
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает автономную копию текущей сущности
        /// </summary>
        public object Clone()
        {
            // Создаем новый объект текущего типа
            var type = GetType();
            var obj = type.GetConstructor(new Type[] { }).Invoke(null);

            // Пробегаем по свойствам текущего типа, соответсвующим полям в таблице БД
            Dictionary<string, object> dbProperties = new Dictionary<string, object>();
            var fields = type.GetProperties().Where(pi => pi.GetCustomAttributes(typeof(DbFieldInfoAttribute), true).Count() > 0);
            foreach (var f in fields)
            {
                dbProperties.Add(f.Name, f.GetValue(this, null));
            }

            // По каждому получаем значение и пишем его в соответствующее свойство нового объекта
            foreach (var item in dbProperties)
            {
                type.GetProperty(item.Key).SetValue(obj, item.Value, null);
            }

            // Устанавливаем значение EntityState в новое
            (obj as Entity).EntityState = EntityState;

            return obj;
        }

        /// <summary>
        /// Возвращает true, если связанные с БД свойства объекта равны аналогичным свойствам obj
        /// </summary>
        /// <param name="obj">Сравниваемый объект</param>
        /// <returns></returns>
        public bool IsEquals(object obj)
        {
            // если разные типы, то сразу неравные
            if (obj.GetType() != GetType())
            {
                return false;
            }

            // Пробегаем по свойствам текущего типа, соответсвующим полям в таблице БД
            var type = GetType();
            bool result = true;

            var fields = type.GetProperties().Where(pi => pi.GetCustomAttributes(typeof(DbFieldInfoAttribute), true).Count() > 0);
            foreach (var f in fields)
            {
                var objValue = type.GetProperty(f.Name).GetValue(obj, null);
                var selfValue = type.GetProperty(f.Name).GetValue(this, null);
                if (objValue != null && selfValue != null)
                {
                    if (!objValue.Equals(selfValue))
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Восстанавливает состояние сущности из резервной копии
        /// </summary>
        /// <param name="backupEntity"></param>
        public void RestoreFromBackup(Entity backupEntity)
        {
            // Пробегаем по свойствам текущего типа, соответсвующим полям в таблице БД
            var fields = GetType().GetProperties().Where(pi => pi.GetCustomAttributes(typeof(DbFieldInfoAttribute), true).Count() > 0);
            foreach (var f in fields)
            {
                object backupValue = f.GetValue(backupEntity, null);
                f.SetValue(this, backupValue, null);
            }
            EntityState = backupEntity.EntityState;
        }
        
        /// <summary>
        /// Постзагрузочная инициализация функционала сущности
        /// </summary>
        internal void PostLoadInitialize()
        {
            if (IsLoaded)
            {
                // создаем бэкап
                _backup = Clone() as Entity;

                // Подписываемся на изменение свойств
                PropertyChanged += OnPropertyChanged;
            }
            IsLoaded = true;
        }
    }	
}
