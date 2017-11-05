using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Contingent.View.Windows;
using Contingent.ViewModel;

namespace Contingent
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //создаем глобальный обработчик исключений, который будет нам писать логи в папку данных приложения
            LogWriter logger = new LogWriter();
            App.Current.DispatcherUnhandledException += logger.Handler;

            // Регистрация библиотек GemBox
            GemBox.Document.ComponentInfo.SetLicense("DH5L-PTFV-SL2S-5PCN");
            GemBox.Spreadsheet.SpreadsheetInfo.SetLicense("E43Y-75J1-FTBX-2T9U");

            //авторизация и запуск главного окна при успехе	   
            base.OnStartup(e);

            //авторизация и запуск главного окна при успехе	
            var auth = new AuthWindow();
            if (auth.ShowDialog() ?? false)
            {
                //создаем главное окно приложения
                DesktopWindow window = new DesktopWindow();
                MainWindow = window;
                window.ShowDialog();
            }

            App.Current.Shutdown();
        }
    }
}
