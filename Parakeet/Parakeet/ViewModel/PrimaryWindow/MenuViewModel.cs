using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Input;
using Parakeet.Model;
using Parakeet.Properties;

namespace Parakeet.ViewModel.PrimaryWindow
{
    public class MenuViewModel
    {
        private MainWindow mainWindow;

        private ICommand newFiles;
        private ICommand openFiles;
        private ICommand saveFiles;
        private ICommand saveFilesUnder;
        private ICommand exit;

        public MenuViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public ICommand NewFiles
        {
            get { return newFiles ?? (newFiles = new RelayCommand(DoNewFiles, CanNewFiles)); }
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
            _new.Filter = "Xml files (*.xml)|*.xml";
            _new.Title = Resources.MenuViewModel_DoNewFiles_Select_file_name___;
            _new.InitialDirectory = Data.FullPathSaveDirectory;
            _new.FileOk += NewFile;
            _new.ShowDialog();
        }

        private void NewFile(object sender, CancelEventArgs e)
        {
            FileDialog _new = (FileDialog)sender;
            Data.getInstance().FileTitle = _new.FileName;
            Data.getInstance().DirectoryModels.Clear();
            Data.getInstance().RemoveRules.Clear();
            Data.getInstance().RenameRules.Clear();
        }

        public ICommand OpenFiles
        {
            get { return openFiles ?? (openFiles = new RelayCommand(DoOpenFiles, CanOpenFiles)); }
        }

        private bool CanOpenFiles()
        {
            return true;
        }

        private void DoOpenFiles()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Xml files (*.xml)|*.xml";
            open.InitialDirectory = Data.FullPathSaveDirectory;
            open.Title = "Select a xml file";
            open.FileOk += gettingFile;
            open.ShowDialog();
        }
        private void gettingFile(object sender, CancelEventArgs e)
        {
            FileDialog tmp = (FileDialog)sender;
            var data = Data.getInstance();
            data.FileTitle = tmp.FileName;
            data.DirectoryModels.Clear();
            data.RemoveRules.Clear();
            data.RenameRules.Clear();
            data.ReadData();
        }

        public ICommand SaveFiles
        {
            get { return saveFiles ?? (saveFiles = new RelayCommand(DoSaveFiles, CanSaveFiles)); }
        }

        private bool CanSaveFiles()
        {
            return true;
        }

        private void DoSaveFiles()
        {
            Data.getInstance().WriteData();
        }

        public ICommand SaveFilesUnder
        {
            get { return saveFilesUnder ?? (saveFilesUnder = new RelayCommand(DoSaveFilesUnder, CanSaveFilesUnder)); }
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
            var data = Data.getInstance();
            data.FileTitle = fileTitle;
            data.WriteData();
        }

        public ICommand Exit
        {
            get { return exit ?? (exit = new RelayCommand(DoExit, CanExit)); }
        }

        private bool CanExit()
        {
            return true;
        }

        private void DoExit()
        {
            mainWindow.Close();
        }
    }
}