using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private ChangeFileNameViewModel changeFileName;
        private RemoveFilesViewModel removeFiles;
        private MenuViewModel menu;

        public MainWindow()
        {
            InitializeComponent();

            this.Closed += MainWindow_Close;
            this.Closing += MainWindow_Closing;

            #region INIT MODEL
            _data = new Data();

            #endregion

            #region INIT VIEWMODEL

            directoryControl = new DirectoryControlViewModel(_data);
            changeFileName = new ChangeFileNameViewModel(_data);
            removeFiles = new RemoveFilesViewModel(_data);
            menu = new MenuViewModel(_data, this);

            #endregion

            #region INIT VIEW

            this.DirectoryControlView.DataContext = directoryControl;
            this.ChangeFileNameView.DataContext = changeFileName;
            this.RemoveFilesView.DataContext = removeFiles;
            this.MenuView.DataContext = menu;

            #endregion
        }

        void MainWindow_Close(object sender, EventArgs e)
        {

        }

        void MainWindow_Closing(object sender, EventArgs e)
        {

        }
    }
}
