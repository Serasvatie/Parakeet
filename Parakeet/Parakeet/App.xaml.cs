using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Parakeet;

namespace Parakeet
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_StartUp(object sender, StartupEventArgs e)
        {
            Current.ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
