using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Parakeet.Model;
using Parakeet.ViewModel.PrimaryWindow;

namespace Parakeet
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Data _data;
        private DirectoryControlViewModel directoryControl;
        public MainWindow()
        {
            InitializeComponent();

            #region INIT MODEL
            _data = new Data();

            #endregion

            #region INIT VIEWMODEL

            directoryControl = new DirectoryControlViewModel(_data);

            #endregion

            #region INIT VIEW

            this.DirectoryControlView.DataContext = directoryControl;

            #endregion
        }
    }
}
