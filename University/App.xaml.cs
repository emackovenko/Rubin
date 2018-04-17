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

            using (var model = new UniversityModel())
            {
                model.Database.EnsureDeleted();
                model.Database.EnsureCreated();
                var a = new Country();
                a.Name = "Россия";
                model.Countries.Add(a);
                model.SaveChanges();
            }

            var desktop = new MainWindow();
            desktop.ShowDialog();
            App.Current.Shutdown();
        }
    }
}
