using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.University
{
    /// <summary>
    /// Защищенная команда приложения
    /// </summary>
    public class Command
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
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Разрешения для ролей на использование данной команды
        /// </summary>
        public List<CommandPermission> Permissions { get; set; }

        public Command()
        {
            Permissions = new List<CommandPermission>();
        }
    }
}
