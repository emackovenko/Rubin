using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using System.IO;
using Admission.ViewModel;
using Admission.View.Windows;
using System.Threading;

namespace Admission
{			   
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			//создаем глобальный обработчик исключений, который будет нам писать логи в папку данных приложения
			LogService logger = new LogService();
			App.Current.DispatcherUnhandledException += logger.Handler;

            // Регистрация библиотек GemBox
            GemBox.Document.ComponentInfo.SetLicense("DH5L-PTFV-SL2S-5PCN");
            GemBox.Spreadsheet.SpreadsheetInfo.SetLicense("E43Y-75J1-FTBX-2T9U");

            //авторизация и запуск главного окна при успехе	   
            base.OnStartup(e);


			var auth = new View.Windows.Auth();
			if (auth.ShowDialog() ?? false)
			{			 
				//создаем главное окно приложения
				DesktopWindow window = new DesktopWindow();
				MainWindow = window;

				//Закрываем заставку и показываем окно			   
				//threadSplashScreen.Abort();
				window.ShowDialog(); 				   
			}		  

			App.Current.Shutdown();
		}  				   
	}
}
