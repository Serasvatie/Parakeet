using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using Manager.Manager;
using Parakeet.Model;
using Parakeet.Properties;

namespace Parakeet.ViewModel.PrimaryWindow
{
    public class DirectoryControlViewModel : BaseNotifyPropertyChanged
    {
        private int _selectedItem;

        private ICommand _addDirectory;
        private ICommand _deleteDirectory;
        private ICommand _start;

        public DirectoryControlViewModel()
        {
            _selectedItem = 0;
        }

        public static SerializableList<DirectoryModel> ListDirectory
        {
            get { return Data.GetInstance().DirectoryModels; }
        }

        public int SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(("SelectedItem"));
            }
        }

        public ICommand AddDirectory
        {
            get
            {
                if (this._addDirectory == null)
                    this._addDirectory = new RelayCommand(DoAddDirectory, CanAddDirectory);
                return _addDirectory;
            }
        }

        private bool CanAddDirectory()
        {
            return true;
        }

        private void DoAddDirectory()
        {
            var dialog = new FolderBrowserDialog
            {
                Description = Resources.DirectoryControlViewModel_DoAddDirectory_Select_a_folder_to_add____,
                ShowNewFolderButton = false
            };
            DialogResult result = dialog.ShowDialog();
            if (result != DialogResult.OK)
                return;
            var tmp = new DirectoryModel(dialog.SelectedPath, true);
            ListDirectory.Add(tmp);
        }

        public ICommand DeleteDirectory
        {
            get
            {
                if (this._deleteDirectory == null)
                    this._deleteDirectory = new RelayCommand(DoDeleteDirectory, CanDeleteDirectory);
                return _deleteDirectory;
            }
        }

        private bool CanDeleteDirectory()
        {
            return ListDirectory.Count > 0;
        }

        private void DoDeleteDirectory()
        {
            ListDirectory.RemoveAt(SelectedItem);
            SelectedItem = 0;
        }

        public ICommand Start
        {
            get { return this._start ?? (this._start = new RelayCommand(DoStart, CanStart)); }
        }

        private bool CanStart()
        {
            return ListDirectory.Count > 0;
        }

        private void DoStart()
        {
            var tmp = new Dictionary<string, dynamic>
            {
                {"Directories", ListDirectory.ToList()},
                {"RemovingRules", RemoveFilesViewModel.ListRules.ToList()},
                {"RenamingRules", ChangeFileNameViewModel.ListChangeRules.ToList()},
                {"SortingRules", SortByViewModel.ListRules.ToList()}
            };
            var taskWindow = new View.TaskWindow.TaskWindow(tmp);
            taskWindow.ShowDialog();
        }
    }
}