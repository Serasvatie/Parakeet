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
        private DirectoryControlViewModel directoryControl;
        private ChangeFileNameViewModel changeFileName;
        private RemoveFilesViewModel removeFiles;
        private MenuViewModel menu;
        private SortByViewModel sortBy;

        public MainWindow()
        {
            InitializeComponent();

            this.Closed += MainWindow_Close;
            this.Closing += MainWindow_Closing;

            try
            {
                Data.getInstance().FileTitle = Settings.Default.NameCurrentXmlFile;
                if (!string.IsNullOrEmpty(Data.getInstance().FileTitle))
                    Data.getInstance().ReadData();
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Erreur durant la lecture du fichier xml par défaut.");
            }

            #region INIT VIEWMODEL

            directoryControl = new DirectoryControlViewModel();
            changeFileName = new ChangeFileNameViewModel();
            removeFiles = new RemoveFilesViewModel();
            menu = new MenuViewModel(this);
            sortBy = new SortByViewModel();

            #endregion

            #region INIT VIEW

            this.DirectoryControlView.DataContext = directoryControl;
            this.ChangeFileNameView.DataContext = changeFileName;
            this.RemoveFilesView.DataContext = removeFiles;
            this.MenuView.DataContext = menu;
            this.SortByView.DataContext = sortBy;

            #endregion
        }

        void MainWindow_Close(object sender, EventArgs e)
        {

        }

        void MainWindow_Closing(object sender, EventArgs e)
        {
            Settings.Default.NameCurrentXmlFile = Data.getInstance().FileTitle;
            Settings.Default.Save();
            Console.WriteLine(Settings.Default.NameCurrentXmlFile);
        }
    }
}
