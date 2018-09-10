using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.University
{
    /// <summary>
    /// Защищенные элементы/группы элементов пользовательского интерфейса
    /// </summary>
    public class InterfaceElement
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Разрешения на отображение для ролей
        /// </summary>
        public List<InterfacePermission> Permissions { get; set; }

        public InterfaceElement()
        {
            Permissions = new List<InterfacePermission>();
        }

    }
}
