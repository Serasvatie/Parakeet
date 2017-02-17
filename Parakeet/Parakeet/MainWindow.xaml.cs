using System;
using System.Windows;
using Parakeet.Model;
using Parakeet.Properties;
using Parakeet.ViewModel.PrimaryWindow;

namespace Parakeet
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DirectoryControlViewModel _directoryControl;
        private ChangeFileNameViewModel _changeFileName;
        private RemoveFilesViewModel _removeFiles;
        private MenuViewModel _menu;
        private SortByViewModel _sortBy;

        public MainWindow()
        {
            InitializeComponent();

            this.Closed += MainWindow_Close;
            this.Closing += MainWindow_Closing;

            try
            {
                Data.GetInstance().FileTitle = Settings.Default.NameCurrentXmlFile;
                if (!string.IsNullOrEmpty(Data.GetInstance().FileTitle))
                    Data.GetInstance().ReadData();
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Erreur durant la lecture du fichier xml par défaut.");
            }

            #region INIT VIEWMODEL

            _directoryControl = new DirectoryControlViewModel();
            _changeFileName = new ChangeFileNameViewModel();
            _removeFiles = new RemoveFilesViewModel();
            _menu = new MenuViewModel(this);
            _sortBy = new SortByViewModel();

            #endregion

            #region INIT VIEW

            this.DirectoryControlView.DataContext = _directoryControl;
            this.ChangeFileNameView.DataContext = _changeFileName;
            this.RemoveFilesView.DataContext = _removeFiles;
            this.MenuView.DataContext = _menu;
            this.SortByView.DataContext = _sortBy;
            this.StatusBarView.DataContext = StatusBarViewModel.GetInstance();

            #endregion
        }

        void MainWindow_Close(object sender, EventArgs e)
        {

        }

        void MainWindow_Closing(object sender, EventArgs e)
        {
            Settings.Default.NameCurrentXmlFile = Data.GetInstance().FileTitle;
            Settings.Default.Save();
        }
    }
}
