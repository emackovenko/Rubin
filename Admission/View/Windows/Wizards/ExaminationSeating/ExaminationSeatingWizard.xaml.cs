using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Pages = Admission.View.Windows.Wizards.ExaminationSeating.Pages;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;

namespace Admission.View.Windows.Wizards.ExaminationSeating
{
	/// <summary>
	/// Логика взаимодействия для ExaminationSeatingWizard.xaml
	/// </summary>
	public partial class ExaminationSeatingWizard : Window
	{

		List<Page> _pageList;

		public ExaminationSeatingWizard()
		{
			InitializeComponent();

			// Производим подписку данного представления на сообщения от мессенджера
			Messenger.Default.Register<string>(this, MessageHandler);

			// Создаем коллекцию страниц мастера
			_pageList = new List<Page>
			{
				new Pages.Welcome(),
				new Pages.TestPage()
			};

			// Выставляем каждой странице один контекст данных
			foreach (var page in _pageList)
			{
				page.DataContext = DataContext;
			}

			// Показываем первую страницу
			contentArea.Navigate(_pageList.First());
			
		}
		
		/// <summary>
		/// Обработчик получаемых сообщений от мессенджера. 
		/// Сообщения получаются как строки и там уже выбираются нужные.
		/// </summary>
		/// <param name="message">Содержимое сообщения</param>
		void MessageHandler(string message)
		{
			if (message == "FinishExaminationSeatingWizard")
			{
				this.DialogResult = true;
			}
		}


		public RelayCommand GoToNextPageCommand
		{
			get
			{
				return new RelayCommand(GoToNextPage, GoToNextPageCanExecute);
			}
		}

		public RelayCommand GoToPrevPageCommand
		{
			get
			{
				return new RelayCommand(GoToPrevPage, GoToPrevPageCanExecute);
			}
		}

		public RelayCommand CancelWizardCommand
		{
			get
			{
				return new RelayCommand(CancelWizard);
			}
		}

		void CancelWizard()
		{
			DialogResult = false;
		}

		void GoToNextPage()
		{
			// Получаем текущую страницу
			var currentPage = contentArea.Content as Page;

			// Находим ее индекс в коллекции страниц мастера по идентификатору
			int pageIndex = _pageList.FindIndex(c => (c.Uid == currentPage.Uid));

			// Переходим к следующей странице
			contentArea.Navigate(_pageList[pageIndex + 1]);
		}

		void GoToPrevPage()
		{
			// Получаем текущую страницу
			var currentPage = contentArea.Content as Page;

			// Находим ее индекс в коллекции страниц мастера по идентификатору
			int pageIndex = _pageList.FindIndex(c => (c.Uid == currentPage.Uid));

			// Переходим к следующей странице
			contentArea.Navigate(_pageList[pageIndex - 1]);
		}

		bool GoToNextPageCanExecute()
		{
			// Получаем текущую страницу
			var currentPage = contentArea.Content as Page;

			// Находим ее индекс в коллекции страниц мастера по идентификатору
			int pageIndex = _pageList.FindIndex(c => (c.Uid == currentPage.Uid));

			// Возвращаем утверждение того, что элемент не последний в коллекции
			return pageIndex != _pageList.Count - 1;
		}

		bool GoToPrevPageCanExecute()
		{
			// Получаем текущую страницу
			var currentPage = contentArea.Content as Page;

			// Находим ее индекс в коллекции страниц мастера по идентификатору
			int pageIndex = _pageList.FindIndex(c => (c.Uid == currentPage.Uid));

			// Возвращаем утверждение того, что элемент не первый в коллекции
			return pageIndex != 0;
		}

		private void nextButton_Click(object sender, RoutedEventArgs e)
		{
			if (GoToNextPageCanExecute())
			{
				GoToNextPage();
			}
		}

		private void prevCommand_Click(object sender, RoutedEventArgs e)
		{
			if (GoToPrevPageCanExecute())
			{
				GoToPrevPage();
			}
		}

		private void cancelButton_Click(object sender, RoutedEventArgs e)
		{
			CancelWizard();
		}
	}
}
