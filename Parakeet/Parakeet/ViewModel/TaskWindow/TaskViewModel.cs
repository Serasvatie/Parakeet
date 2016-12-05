﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
            this.lists = lists;
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
            var manager = new FFManager.FFManager();
            manager.SettingLists(lists);
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