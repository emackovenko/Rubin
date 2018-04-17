using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Data;

namespace University
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Регистрация библиотек GemBox
            GemBox.Document.ComponentInfo.SetLicense("DH5L-PTFV-SL2S-5PCN");
            GemBox.Spreadsheet.SpreadsheetInfo.SetLicense("E43Y-75J1-FTBX-2T9U");

            using (var model = new UniversityModel())
            {
                model.Database.EnsureDeleted();
                model.Database.EnsureCreated();
            }
            var desktop = new MainWindow();
            desktop.ShowDialog();
            App.Current.Shutdown();
        }
    }
}
