using System;
using System.Collections.Generic;
using System.ComponentModel;
using Manager.Manager;

namespace SManager
{
    public class SManager
    {
        private List<SortByRule> _sort = new List<SortByRule>();
        private List<DirectoryModel> _directory = new List<DirectoryModel>();

        public event EventHandler IsBwStarted;
        public BackgroundWorker BwTask;

        public SManager()
        {
            BwTask = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            BwTask.DoWork += ExecuteTask;
        }

        public void SettingList(Dictionary<string, object> lists)
        {
            _directory = lists["Directories"] as List<DirectoryModel>;
            dynamic tmp;
            if (lists.TryGetValue("SortingRules", out tmp))
                _sort = tmp as List<SortByRule>;
        }

        private void ExecuteTask(object sender, DoWorkEventArgs e)
        {
        }
    }
}