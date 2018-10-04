using System;
using System.Collections.Generic;
using System.Text;

namespace Data.University
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public class Role
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
        /// Пользователи, принадлежащие данной роли
        /// </summary>
        public List<User> Users { get; set; }

        /// <summary>
        /// Доступные защищенные команды для данной роли
        /// </summary>
        public List<CommandPermission> AvailableCommands { get; set; }

        /// <summary>
        /// Доступные для роли защищенные имена элементов интерфейса
        /// </summary>
        public List<InterfacePermission> AvailableViews { get; set; }

        /// <summary>
        /// Для пользователя требуется доступ к данным из БД WorkOk
        /// </summary>
        public bool? IsWorkOkAccessRequired { get; set; }

        /// <summary>
        /// Для пользователя требуется доступ к данным из БД rating.astu
        /// </summary>
        public bool? IsAstuAccessRequired { get; set; }

        public Role()
        {
            Users = new List<User>();
            AvailableCommands = new List<CommandPermission>();
            AvailableViews = new List<InterfacePermission>();
        }
    }
}
