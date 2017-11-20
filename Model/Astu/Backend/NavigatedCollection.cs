using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace Model.Astu
{
    /// <summary>
    /// Навигационная коллекция дочерних сущностей
    /// </summary>
    /// <typeparam name="TEntity">Тип дочерних сущностей (ген)</typeparam>
    public class NavigatedCollection<TEntity>: List<TEntity>, INavigatedCollection where TEntity: Entity
    {
        /************* Определения ***************
         * 
         * Родитель - Entity - объект, порождающий коллекцию дочерних сущностей (генов)
         * 
         * Ген - тип дочернего элемента навигационной коллекции
         * 
         *****************************************/

        public NavigatedCollection(Entity parentEntity): base()
        {
            _parentEntity = parentEntity;

            // Получить значение первичного ключа родителя
            var parentType = _parentEntity.GetType();
            var parentPrimaryKeyProperty = parentType.GetProperties().FirstOrDefault(pi => pi.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Count() > 0);
            if (parentPrimaryKeyProperty == null)
            {
                throw new Exception("У родительского элемента навигационной коллекции не найдено поле первичного ключа.");
            }

            if (parentPrimaryKeyProperty.GetValue(_parentEntity, null) == null)
            {
                parentPrimaryKeyProperty.SetValue(_parentEntity, QueryProvider.GetNewGeneratedId(parentType), null);
            }

            _parentPrimaryValue = parentPrimaryKeyProperty.GetValue(_parentEntity, null);
            if (_parentPrimaryValue == null)
            {
                throw new Exception("У родительского элемента навигационной коллекции не задано значение поля первичного ключа.");
            }

            // Получить свойство, содержащее значение первичного ключа родителя, у гена
            var geneType = typeof(TEntity);
            var geneNavigationProperties = geneType.GetProperties().Where( pi => pi.GetCustomAttributes(typeof(NavigationPropertyAttribute), true).Count() > 0);
            foreach (var geneProp in geneNavigationProperties)
            {
                var attr = geneProp.GetCustomAttributes(typeof(NavigationPropertyAttribute), true).First() as NavigationPropertyAttribute;
                if (attr.NavigationType == parentType)
                {
                    _geneNavigationProperty = geneProp;
                    break;
                }
            }

            if (_geneNavigationProperty == null)
            {
                string error = string.Format("Для типа {0} не установлено навигационных свойств к {1}.", geneType.Name, parentType.Name);
                throw new Exception(error);
            }


            // собираем коллекцию
            var collection = EntitySet.Where(e => _geneNavigationProperty.GetValue(e, null)?.Equals(_parentPrimaryValue) ?? false);

            AddRange(collection);
        }

        Entity _parentEntity;

        PropertyInfo _geneNavigationProperty;

        object _parentPrimaryValue;
        
        EntitySet<TEntity> EntitySet
        {
            get
            {
                var contextType = typeof(Astu);
                var entitySetType = typeof(EntitySet<>);
                var searchedType = entitySetType.MakeGenericType(typeof(TEntity));
                var parentalCollectionPropertyInfo = contextType.GetProperties().FirstOrDefault(pi => pi.PropertyType == searchedType);
                var value = parentalCollectionPropertyInfo.GetValue(contextType, null);
                return value as EntitySet<TEntity>;
            }
        }

        public new void Add(TEntity item)
        {
            _geneNavigationProperty.SetValue(item, _parentPrimaryValue, null);
            base.Add(item);
            EntitySet.Add(item);
        }

        public new void Remove(TEntity item)
        {
            base.Remove(item);
            EntitySet.Remove(item);
        }
    }
}
