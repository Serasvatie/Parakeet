using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using FFManager.Model;
using Parakeet.Model;
using Parakeet.Properties;

namespace Parakeet.ViewModel.PrimaryWindow
{
    public class DirectoryControlViewModel : BaseNotifyPropertyChanged
    {
        private int selectedItem;

        private ICommand addDirectory;
        private ICommand deleteDirectory;
        private ICommand start;

        public DirectoryControlViewModel()
        {
            selectedItem = 0;
        }

        public static SerializableList<DirectoryModel> ListDirectory
        {
            get { return Data.getInstance().DirectoryModels; }
        }

        public int SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged(("SelectedItem"));
            }
        }

        public ICommand AddDirectory
        {
            get
            {
                if (this.addDirectory == null)
                    this.addDirectory = new RelayCommand(DoAddDirectory, CanAddDirectory);
                return addDirectory;
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
                if (this.deleteDirectory == null)
                    this.deleteDirectory = new RelayCommand(DoDeleteDirectory, CanDeleteDirectory);
                return deleteDirectory;
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
            get { return this.start ?? (this.start = new RelayCommand(DoStart, CanStart)); }
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