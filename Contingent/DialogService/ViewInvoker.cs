using CommonMethods.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Contingent.DialogService
{
    /// <summary>
    /// Сервис взаимодействия между ViewModel и дочерними View
    /// </summary>
    public static class ViewInvoker
    {
        /// <summary>
        /// Открывает редактор в диалоговом режиме и возвращает результат закрытия окна
        /// </summary>
        /// <param name="contentType">Тип редактора</param>
        /// <param name="viewModel">Контекст данных, передаваемый в редактор</param>  
        public static bool ShowEditor(EditingContent contentType, object viewModel)
        {
            Editor editor = new Editor(GetEditingContent(contentType), viewModel);
            return editor.ShowDialog() ?? false;
        }

        /// <summary>
        /// Возвращает контрол с содержимым редактора
        /// </summary>
        /// <param name="contentType">Тип редактирующего содержимого</param>	   
        static UserControl GetEditingContent(EditingContent contentType)
        {
            switch (contentType)
            {
                case EditingContent.StudentView:
                    return new Contingent.View.Workspaces.Students.Pages.Views.StudentView();
                default:
                    throw new Exception("Указанный редактор не зарегистрирован в DialogService");
            }
        }
    }
}
