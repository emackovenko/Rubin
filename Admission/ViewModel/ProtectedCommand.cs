using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Model.Admission;

namespace Admission.ViewModel
{
    /// <summary>
    /// Предоставляет защищенную команду, которая может быть выполнена только при наличии соответствующего разрешения у текущего пользователя
    /// </summary>
    public class ProtectedCommand : ICommand
    {
        readonly Action _execute;
        readonly Func<bool> _canExecute;
        readonly string _permissionName;

        /// <summary>
        /// Инициализирует новый экземпляр защищённой команды, которая всегда может быть выполнена при наличии разрешения текущему пользователю
        /// </summary>
        /// <param name="execute">Логика, выполняемая командой</param>									
        public ProtectedCommand(Action execute, string permissionName)
            : this(execute, permissionName, () => true)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр защищённой команды с проверкой возможности выполнения
        /// </summary>
        /// <param name="execute">Логика, выполняемая командой</param>
        /// <param name="canExecute">Метод, выполняющий проверку возможности выполнения команды</param>				 
        public ProtectedCommand(Action execute, string permissionName, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }
            if (string.IsNullOrWhiteSpace(permissionName))
            {
                throw new Exception("Попытка выполнить незарегистрированную команду.");
            }
			
            _execute = execute;
            _canExecute = canExecute;
            _permissionName = permissionName;
        }

        /// <summary>
        /// Генерируется при изменении свойства CanExecute
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Возвращает значение возможности выполнения команды в данный момент времени
        /// </summary>
        /// <param name="parameter">Этот параметр всегда будет проигнорирован (ставь NULL)</param> 
        public bool CanExecute(object parameter)
        {
            return IsAllowed() && _canExecute();
        }

        /// <summary>
        /// Возвращает наличие разрешения на выполнение команды у текущего пользователя
        /// <summary/>
        bool IsAllowed()
        {
            // Определяем разрешение по названию команды			
            try
            {
                // Загружаем одно разрешение из БД
                var permission = (from commandPermission in Session.DataModel.CommandPermissions
                                  where commandPermission.Command.Name == _permissionName
                                  select commandPermission).Single();

                // Возвращаем наличие разрешения у роли пользователя
                return Session.CurrentUser.Role.CommandPermissions.Contains(permission);
            }
            // Если результат не равен одному элементу, то бросается исключение. Возвращаем лож.
            catch
            {
				return false;
            }
        }

        /// <summary>
        /// Выполняет логику команды
        /// </summary>
        /// <param name="parameter">Этот параметр всегда будет проигнорирован (ставь NULL)</param>
        public void Execute(object parameter)
        {
            if (IsAllowed() && CanExecute(null))
                _execute();
        }

        /// <summary>
        /// Запускает событие изменения свойства CanExecute
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
