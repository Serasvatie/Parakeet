using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Parakeet.Model;
using Parakeet.Properties;

namespace Parakeet.ViewModel.PrimaryWindow
{
    public class MenuViewModel
    {
        private Data _data;
        private MainWindow mainWindow;

        private ICommand newFiles;
        private ICommand openFiles;
        private ICommand saveFiles;
        private ICommand saveFilesUnder;
        private ICommand exit;

        public MenuViewModel(Data data, MainWindow mainWindow)
        {
            _data = data;
            this.mainWindow = mainWindow;
        }

        public ICommand NewFiles
        {
            get { return newFiles ?? (newFiles = new RelayCommand(DoNewFiles, CanNewFiles)); }
        }

        private bool CanNewFiles()
        {
            return false;
        }

        private void DoNewFiles()
        {
        }

        public ICommand OpenFiles
        {
            get { return openFiles ?? (openFiles = new RelayCommand(DoOpenFiles, CanOpenFiles)); }
        }

        private bool CanOpenFiles()
        {
            return false;
        }

        private void DoOpenFiles()
        {
        }

        public ICommand SaveFiles
        {
            get { return saveFiles ?? (saveFiles = new RelayCommand(DoSaveFiles, CanSaveFiles)); }
        }

        private bool CanSaveFiles()
        {
            return false;
        }

        private void DoSaveFiles()
        {
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
            save.FileOk += new CancelEventHandler(save_fileOk);
            save.InitialDirectory = Environment.SpecialFolder.MyComputer.ToString();
            save.ShowDialog();
        }

        private void save_fileOk(object sender, CancelEventArgs e)
        {
            string fileTitle = ((FileDialog)sender).FileName;
            var data = new Data(fileTitle, ChangeFileNameViewModel.ListChangeRules,
                RemoveFilesViewModel.ListRules, DirectoryControlViewModel.ListDirectory);
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