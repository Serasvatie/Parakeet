using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Windows.Input;
using Parakeet.Model;
using Parakeet.Properties;

namespace Parakeet.ViewModel.PrimaryWindow
{
    public class DirectoryControlViewModel : BaseNotifyPropertyChanged
    {
        private Data _data;
        private static ObservableCollection<DirectoryModel> directories;
        private int selectedItem;

        private ICommand addDirectory;
        private ICommand deleteDirectory;
        private ICommand start;

        public DirectoryControlViewModel(Data data)
        {
            _data = data;
            selectedItem = 0;
            directories = new ObservableCollection<DirectoryModel>();
        }

        public static ObservableCollection<DirectoryModel> ListDirectory
        {
            get { return directories; }
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
            return directories.Count > 0 && SelectedItem > 0;
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
            return directories.Count > 0;
        }

        private void DoStart()
        {
            var tmp = new Dictionary<string, dynamic>
            {
                {"Directory", ListDirectory},
                {"RemovingRules", RemoveFilesViewModel.ListRules},
                {"RenamingRules", ChangeFileNameViewModel.ListChangeRules},
                {"SortingRules", SortByViewModel.ListRules}
            };
            var taskWindow = new View.TaskWindow.TaskWindow(tmp);
            taskWindow.ShowDialog();
        }
    }
}