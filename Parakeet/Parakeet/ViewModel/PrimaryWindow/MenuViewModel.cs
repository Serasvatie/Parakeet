using Parakeet.Model;
using Parakeet.Properties;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Input;

namespace Parakeet.ViewModel.PrimaryWindow
{
    public class MenuViewModel : BaseNotifyPropertyChanged
    {
        private MainWindow _mainWindow;

        private bool _en;
        private bool _fr;

        private ICommand _newFiles;
        private ICommand _openFiles;
        private ICommand _saveFiles;
        private ICommand _saveFilesUnder;
        private ICommand _exit;
        private ICommand _english;
        private ICommand _french;

        public MenuViewModel(MainWindow mainWindow)
        {
            this._mainWindow = mainWindow;
            switch (Settings.Default.CultureInfo)
            {
                case "en":
                    EnCheck = true;
                    break;
                case "fr":
                    FrCheck = true;
                    break;
            }
        }

        public ICommand NewFiles
        {
            get { return _newFiles ?? (_newFiles = new RelayCommand(DoNewFiles, CanNewFiles)); }
        }

        private bool CanNewFiles()
        {
            return true;
        }

        private void DoNewFiles()
        {
            FileDialog _new = new SaveFileDialog();
            _new.AddExtension = true;
            _new.CheckPathExists = true;
            _new.DefaultExt = ".xml";
            _new.Filter = Resources.MenuViewModel_DoOpenFiles_Xml_files____xml____xml;
            _new.Title = Resources.MenuViewModel_DoNewFiles_Select_file_name___;
            _new.InitialDirectory = Data.FullPathSaveDirectory;
            _new.FileOk += NewFile;
            _new.ShowDialog();
        }

        private void NewFile(object sender, CancelEventArgs e)
        {
            FileDialog _new = (FileDialog)sender;
            Data.GetInstance().FileTitle = _new.FileName;
            Data.GetInstance().DirectoryModels.Clear();
            Data.GetInstance().RemoveRules.Clear();
            Data.GetInstance().RenameRules.Clear();
            StatusBarViewModel.GetInstance().RunRefresh();
        }

        public ICommand OpenFiles
        {
            get { return _openFiles ?? (_openFiles = new RelayCommand(DoOpenFiles, CanOpenFiles)); }
        }

        private bool CanOpenFiles()
        {
            return true;
        }

        private void DoOpenFiles()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = Resources.MenuViewModel_DoOpenFiles_Xml_files____xml____xml;
            open.InitialDirectory = Data.FullPathSaveDirectory;
            open.Title = Resources.MenuViewModel_DoOpenFiles_Select_a_xml_file;
            open.FileOk += GettingFile;
            open.ShowDialog();
            StatusBarViewModel.GetInstance().RunRefresh();
        }

        private void GettingFile(object sender, CancelEventArgs e)
        {
            FileDialog tmp = (FileDialog)sender;
            var data = Data.GetInstance();
            data.FileTitle = tmp.FileName;
            data.DirectoryModels.Clear();
            data.RemoveRules.Clear();
            data.RenameRules.Clear();
            data.ReadData();
        }

        public ICommand SaveFiles
        {
            get { return _saveFiles ?? (_saveFiles = new RelayCommand(DoSaveFiles, CanSaveFiles)); }
        }

        private bool CanSaveFiles()
        {
            return !string.IsNullOrEmpty(Data.GetInstance().FileTitle);
        }

        private void DoSaveFiles()
        {
            Data.GetInstance().WriteData();
            StatusBarViewModel.GetInstance().RunRefresh();
        }

        public ICommand SaveFilesUnder
        {
            get { return _saveFilesUnder ?? (_saveFilesUnder = new RelayCommand(DoSaveFilesUnder, CanSaveFilesUnder)); }
        }

        private bool CanSaveFilesUnder()
        {
            return true;
        }

        private void DoSaveFilesUnder()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.AddExtension = true;
            save.CheckPathExists = true;
            save.DefaultExt = ".xml";
            save.Filter = Resources.MenuViewModel_DoSaveFilesUnder_Xml_files___xml______xml;
            save.Title = Resources.MenuViewModel_DoSaveFilesUnder_Sous____;
            save.FileOk += save_fileOk;
            save.InitialDirectory = Data.FullPathSaveDirectory;
            save.ShowDialog();
        }

        private void save_fileOk(object sender, CancelEventArgs e)
        {
            string fileTitle = ((FileDialog)sender).FileName;
            var data = Data.GetInstance();
            data.FileTitle = fileTitle;
            data.WriteData();
            StatusBarViewModel.GetInstance().RunRefresh();
        }

        public ICommand Exit
        {
            get { return _exit ?? (_exit = new RelayCommand(DoExit, CanExit)); }
        }

        private bool CanExit()
        {
            return true;
        }

        private void DoExit()
        {
            _mainWindow.Close();
        }

        public ICommand English
        {
            get { return this._english ?? (this._english = new RelayCommand(DoEnglish)); }
        }

        private void DoEnglish()
        {
            EnCheck = true;
            FrCheck = false;
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en");
            Settings.Default.CultureInfo = "en";
            System.Windows.MessageBox.Show(System.Windows.Application.Current.MainWindow, "Please restart Parakeet to make the modification take effect.", "English");
        }

        public bool EnCheck
        {
            get { return _en; }
            set
            {
                _en = value;
                OnPropertyChanged("EnCheck");
            }
        }

        public ICommand French
        {
            get { return this._french ?? (this._french = new RelayCommand(DoFrench)); }
        }

        private void DoFrench()
        {
            EnCheck = false;
            FrCheck = true;
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("fr");
            Settings.Default.CultureInfo = "fr";
            System.Windows.MessageBox.Show(System.Windows.Application.Current.MainWindow, "Redémarrer Parakeet pour que la modification prenne effet.", "Français");
        }

        public bool FrCheck {
            get { return _fr; }
            set
            {
                _fr = value;
                OnPropertyChanged("FrCheck");
            }
        }
    }
}