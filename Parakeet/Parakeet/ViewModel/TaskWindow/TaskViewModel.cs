using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private View.TaskWindow.TaskWindow _taskWindow;
        private Dictionary<string, dynamic> _lists;

        private bool _isRecursive;
        private bool _isRemove;
        private bool _isRename;
        private bool _isSort;
        private bool _isChecking;

        private ICommand _startTask;
        private ICommand _cancelTasks;

        public TaskViewModel(View.TaskWindow.TaskWindow _taskWindow, Dictionary<string, dynamic> lists)
        {
            this._taskWindow = _taskWindow;
            this._taskWindow.Closed += OnClose;
            this._lists = lists;
            this.IsRecursive = Settings.Default.TaskRecursive;
            this.IsRemove = Settings.Default.TaskRemove;
            this.IsSort = Settings.Default.TaskSort;
            this.IsRename = Settings.Default.TaskRename;
            this.IsChecking = Settings.Default.TaskCheck;
        }

        private void OnClose(object sender, EventArgs e)
        {
            Settings.Default.TaskSort = _isSort;
            Settings.Default.TaskRename = _isRename;
            Settings.Default.TaskRecursive = _isRecursive;
            Settings.Default.TaskRemove = _isRemove;
            Settings.Default.TaskCheck = _isChecking;
        }

        public bool IsRecursive
        {
            get { return _isRecursive; }
            set
            {
                _isRecursive = value;
                OnPropertyChanged("IsRecursive");
            }
        }

        public bool IsRemove
        {
            get { return _isRemove; }
            set
            {
                _isRemove = value;
                OnPropertyChanged("IsRemove");
            }
        }

        public bool IsRename
        {
            get { return _isRename; }
            set
            {
                _isRename = value;
                OnPropertyChanged("IsRename");
            }
        }

        public bool IsSort
        {
            get { return _isSort; }
            set
            {
                _isSort = value;
                OnPropertyChanged("IsSort");
            }
        }

        public bool IsChecking
        {
            get { return _isChecking; }
            set
            {
                Debug.WriteLine(value);
                _isChecking = value;
                OnPropertyChanged("IsChecking");
            }
        }

        public ICommand StartTask
        {
            get { return _startTask ?? (_startTask = new RelayCommand(DoStartTask, CanStartTask)); }
        }

        private bool CanStartTask()
        {
            return IsRename || IsRemove || IsSort || IsChecking;
        }

        private void DoStartTask()
        {
            _lists.Add("Recursive", IsRecursive);
            if (!IsRemove)
                _lists.Remove("RemovingRules");
            if (!IsRename)
                _lists.Remove("RenamingRules");
            if (!IsSort)
                _lists.Remove("SortingRules");
            if (!IsChecking)
                _lists.Remove("DocDistRules");
            Data.GetInstance().FFManager.SettingLists(_lists);
            Data.GetInstance().SManager.SettingList(_lists);
            Data.GetInstance().CManager.SettingList(_lists);
            if (IsRemove || IsRename)
                Data.GetInstance().FFManager.BwTask.RunWorkerAsync();
            if (IsSort)
                Data.GetInstance().SManager.BwTask.RunWorkerAsync();
            if (IsChecking)
            {
                Debug.WriteLine("gfdgfdvfdfdgvfdgsdf");
                Data.GetInstance().CManager.BwTask.RunWorkerAsync();

            }
            _taskWindow.Close();
        }

        public ICommand CancelTask
        {
            get { return _cancelTasks ?? (_cancelTasks = new RelayCommand(DoCancelTask, () => true)); }
        }

        private void DoCancelTask()
        {
            _taskWindow.Close();
        }
    }
}