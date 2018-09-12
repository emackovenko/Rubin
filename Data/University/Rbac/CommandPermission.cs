using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.University
{
    /// <summary>
    /// Разрешения для ролей на использование защищенных команд
    /// </summary>
    public class CommandPermission
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор команды
        /// </summary>
        public int? CommandId { get; set; }

        /// <summary>
        /// Команда
        /// </summary>
        public Command Command { get; set; }

        /// <summary>
        /// Идентфикатор роли
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public Role Role { get; set; }
    }
}
