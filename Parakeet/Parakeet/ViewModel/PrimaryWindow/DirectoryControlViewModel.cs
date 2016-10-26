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
    public class DirectoryControlViewModel : BaseNotifyPropertyChanged, IDisposable
    {
        private Data _data;
        private ObservableCollection<DirectoryModel> directories;

        private ICommand addDirectory;
        private ICommand deleteDirectory;
        private ICommand start;

        public DirectoryControlViewModel(Data data)
        {
            _data = data;
            directories = new ObservableCollection<DirectoryModel>(data.Path.Path);
        }

        public ObservableCollection<DirectoryModel> ListDirectory
        {
            get { return directories; }
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
            var dialog = new FolderBrowserDialog();
            dialog.Description = Resources.DirectoryControlViewModel_DoAddDirectory_Select_a_folder_to_add____;
            DialogResult result = dialog.ShowDialog();
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
            return true;
        }

        private void DoDeleteDirectory()
        {
            return;
        }

        public ICommand Start
        {
            get
            {
                if (this.start == null)
                    this.start = new RelayCommand(DoStart, CanStart);
                return
                    this.start;
            }
        }

        private bool CanStart()
        {
            return true;
        }

        private void DoStart()
        {
            return;
        }

        public void Dispose()
        {
        }
    }
}