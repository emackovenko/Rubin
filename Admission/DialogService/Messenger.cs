using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Admission.DialogService
{
	/// <summary>
	/// Отображает диалоговые сообщения пользователю
	/// </summary>
	public class Messenger
	{
		/// <summary>
		/// Вопрос об удалении элемента
		/// </summary>
		/// <returns></returns>
		public static bool RemoveQuestion()
		{
			return MessageBox.Show("Вы действительно хотите удалить эту запись?",
					"Подтвердите удаление",
					System.Windows.MessageBoxButton.YesNo,
					System.Windows.MessageBoxImage.Question) == System.Windows.MessageBoxResult.Yes;
		}


		public static void ValidationErrorMessage(string message)
		{
			MessageBox.Show(message, "Ошибка валидации данных", 
				MessageBoxButton.OK,
				MessageBoxImage.Error);
		}


        /// <summary>
        /// Вопрос об отправке заявления в архив
        /// </summary>
        /// <returns></returns>
        public static bool SendClaimToArchiveQuestion()
        {
            return MessageBox.Show("Вы действительно хотите отправить это заявление в архив?",
                    "Подтвердите удаление",
                    System.Windows.MessageBoxButton.YesNo,
                    System.Windows.MessageBoxImage.Question) == System.Windows.MessageBoxResult.Yes;
        }


		public static void FileNotExistingError()
		{
			MessageBox.Show("Указанный файл не найден.", "Ошибка",
				MessageBoxButton.OK,
				MessageBoxImage.Error);
		}


    }
}
