using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Parakeet.Model;

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
            return false;
        }

        private void DoSaveFilesUnder()
        {
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