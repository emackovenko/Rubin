using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.University
{
    /// <summary>
    /// Разрешения для ролей на отображение элементов пользовательского интерфейса
    /// </summary>
    public class InterfacePermission
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор элемента интерфейса
        /// </summary>
        public int? InterfaceElementId { get; set; }

        /// <summary>
        /// Элемент интерфейса
        /// </summary>
        public InterfaceElement InterfaceElement { get; set; }

        /// <summary>
        /// Идентификтаор роли
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public Role Role { get; set; }
    }
}
