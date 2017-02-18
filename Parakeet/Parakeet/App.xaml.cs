using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Parakeet;
using Parakeet.Properties;

namespace Parakeet
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow main;

        private void Application_StartUp(object sender, StartupEventArgs e)
        {
            Current.ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;
            Debug.WriteLine(Settings.Default.CultureInfo);
            if (!string.IsNullOrEmpty(Settings.Default.CultureInfo))
                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.CultureInfo);
            main = new MainWindow();
            main.Show();
        }
    }
}
