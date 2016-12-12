using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Parakeet.Model;
using Parakeet.Properties;

namespace Parakeet.ViewModel.TaskWindow
{
    public class TaskViewModel : BaseNotifyPropertyChanged
    {
        private View.TaskWindow.TaskWindow taskWindow;
        private Dictionary<string, dynamic> lists;

        private bool isRecursive;
        private bool isRemove;
        private bool isRename;
        private bool isSort;

        private ICommand startTask;
        private ICommand cancelTasks;

        public TaskViewModel(View.TaskWindow.TaskWindow _taskWindow, Dictionary<string, dynamic> lists)
        {
            taskWindow = _taskWindow;
            taskWindow.Closed += OnClose;
            this.lists = lists;
            this.IsRecursive = Settings.Default.TaskRecursive;
            this.IsRemove = Settings.Default.TaskRemove;
            this.IsSort = Settings.Default.TaskSort;
            this.IsRename = Settings.Default.TaskRename;
        }

        private void OnClose(object sender, EventArgs e)
        {
            Settings.Default.TaskSort = isSort;
            Settings.Default.TaskRename = isRename;
            Settings.Default.TaskRecursive = isRecursive;
            Settings.Default.TaskRemove = isRemove;
        }

        public bool IsRecursive
        {
            get { return isRecursive; }
            set
            {
                isRecursive = value;
                OnPropertyChanged("IsRecursive");
            }
        }

        public bool IsRemove
        {
            get { return isRemove; }
            set
            {
                isRemove = value;
                OnPropertyChanged("IsRemove");
            }
        }

        public bool IsRename
        {
            get { return isRename; }
            set
            {
                isRename = value;
                OnPropertyChanged("IsRename");
            }
        }

        public bool IsSort
        {
            get { return isSort; }
            set
            {
                isSort = value;
                OnPropertyChanged("IsSort");
            }
        }

        public ICommand StartTask
        {
            get { return startTask ?? (startTask = new RelayCommand(DoStartTask, CanStartTask)); }
        }

        private bool CanStartTask()
        {
            return IsRename || IsRemove || IsSort;
        }

        private void DoStartTask()
        {
            lists.Add("Recursive", IsRecursive);
            if (!IsRemove)
                lists.Remove("RemovingRules");
            if (!IsRename)
                lists.Remove("RenamingRules");
            if (!IsSort)
                lists.Remove("SortingRules");
            Data.getInstance().manager.SettingLists(lists);
            Data.getInstance().manager.bwTask.RunWorkerAsync();
            MainWindow.statusbar.Refresh();
        }

        public ICommand CancelTask
        {
            get { return cancelTasks ?? (cancelTasks = new RelayCommand(DoCancelTask, () => true)); }
        }

        private void DoCancelTask()
        {
            taskWindow.Close();
        }
    }
}