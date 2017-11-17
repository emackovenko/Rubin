using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Astu;
using GalaSoft.MvvmLight;
using System.Windows.Controls;

namespace Contingent.DialogService
{
    public static class EditorInvoker
    {
        /// <summary>
        /// Открывает окно редактора для указанной сущности и возвращает результат модального закрытия
        /// </summary>
        /// <param name="entity">Редактируемая сущность</param>
        /// <returns></returns>
        public static bool ShowEditor(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            return ShowEditor(entity, GetEditingViewModel(entity));
        }

        /// <summary>
        /// Открывает окно редактора для указанной сущности с подготовленной моделью представления
        /// </summary>
        /// <param name="entity">Редактируемая сущность</param>
        /// <param name="vm">Модель представления для редактора</param>
        /// <returns></returns>
        public static bool ShowEditor(Entity entity, ViewModelBase vm)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (vm == null)
            {
                throw new ArgumentNullException("vm");
            }

            // получаем представление редактора
            var view = GetEditingControl(entity);

            // создаем окно редактора, кладем ему в ебало всю эту пиздобратию
            var editor = new EntityEditor(entity, view, vm, null);

            return editor.ShowDialog() ?? false;
        }


        /// <summary>
        /// Открывает окно редактора для указанной сущности и возвращает результат модального закрытия, игнорируя сохранение сущности
        /// </summary>
        /// <param name="entity">Редактируемая сущность</param>
        /// <returns></returns>
        public static bool ShowEditorWithoutSaving(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            return ShowEditorWithoutSaving(entity, GetEditingViewModel(entity));
        }

        /// <summary>
        /// Открывает окно редактора для указанной сущности с подготовленной моделью представления, игнорируя сохранение сущности
        /// </summary>
        /// <param name="entity">Редактируемая сущность</param>
        /// <param name="vm">Модель представления для редактора</param>
        /// <returns></returns>
        public static bool ShowEditorWithoutSaving(Entity entity, ViewModelBase vm)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (vm == null)
            {
                throw new ArgumentNullException("vm");
            }

            // получаем представление редактора
            var view = GetEditingControl(entity);

            // создаем окно редактора, кладем ему в ебало всю эту пиздобратию
            var editor = new EntityEditor(entity, view, vm, true);

            return editor.ShowDialog() ?? false;
        }

        /// <summary>
        /// Открывает окно редактора для указанной сущности. В логике окна будет обработано сохранение данной сущности в БД.
        /// </summary>
        /// <param name="entity">Редактируемая сущность</param>
        /// <param name="vm">Модель представления для редактора</param>
        /// <param name="saveAction">Делегат метода, обрабатывающего сохранение результата работы в редакторе</param>
        public static void ShowEditor(Entity entity, Action<Entity> saveAction)
        {
            ShowEditor(entity, GetEditingViewModel(entity), saveAction);
        }

        /// <summary>
        /// Открывает окно редактора для указанной сущности с подготовленной моделью представления. В логике окна будет обработано сохранение данной сущности в БД.
        /// </summary>
        /// <param name="entity">Редактируемая сущность</param>
        /// <param name="vm">Модель представления для редактора</param>
        /// <param name="saveAction">Делегат метода, обрабатывающего сохранение результата работы в редакторе</param>
        public static void ShowEditor(Entity entity, ViewModelBase vm, Action<Entity> saveAction)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (vm == null)
            {
                throw new ArgumentNullException("vm");
            }
            if (saveAction == null)
            {
                throw new ArgumentException("saveAction");
            }

            // получаем представление редактора
            var view = GetEditingControl(entity);

            // создаем окно редактора, кладем ему в ебало всю эту пиздобратию
            var editor = new EntityEditor(entity, view, vm, saveAction);
            editor.ShowDialog();
        }

        /// <summary>
        /// Возвращает контрол с содержимым редактора сущности
        /// </summary>
        /// <param name="entity">Редактируемая сущность</param>
        static UserControl GetEditingControl(Entity entity)
        {
            var entityType = entity.GetType();

            if (entityType == typeof(Student))
            {
                return new View.Workspaces.Students.Pages.Views.StudentView();
            }
            if (entityType == typeof(EnrollmentByUniversityTransferOrder))
            {
                return new View.Workspaces.Students.Pages.Views.EnrollmentByUniversityTransferOrderView();
            }
            if (entityType == typeof(EnrollmentOrder))
            {
                return new View.Workspaces.Students.Pages.Views.EnrollmentOrderView();
            }
            if (entityType == typeof(IdentityDocument))
            {
                return new View.Workspaces.Students.Pages.Views.IdentityDocumentView();
            }

            throw new NotImplementedException("Функционал для редактирования данной сущности не предусмотрен.");
        }

        /// <summary>
        /// Возвращает модель представления редактора сущности
        /// </summary>
        /// <param name="entity">Редактируемая сущность</param>
        static ViewModelBase GetEditingViewModel(Entity entity)
        {
            var entityType = entity.GetType();

            if (entityType == typeof(Student))
            {
                return new ViewModel.Workspaces.Students.StudentViewViewModel(entity as Student);
            }
            if (entityType == typeof(EnrollmentByUniversityTransferOrder))
            {
                return new ViewModel.Workspaces.Students.EnrollmentByUniversityTransferOrderViewModel(entity as EnrollmentByUniversityTransferOrder);
            }
            if (entityType == typeof(EnrollmentOrder))
            {
                return new ViewModel.Workspaces.Students.EnrollmentOrderViewModel(entity as EnrollmentOrder);
            }
            if (entityType == typeof(IdentityDocument))
            {
                return new ViewModel.Workspaces.Students.IdentityDocumentViewModel(entity as IdentityDocument);
            }

            throw new NotImplementedException("Функционал для редактирования данной сущности не предусмотрен.");
        }

    }
}
