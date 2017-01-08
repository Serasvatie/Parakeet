using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using Manager.Manager;

namespace SManager
{
    public class SManager : AManager
    {
        private string[] _input;
        private string[] _output;

        public SManager()
        {
            BwTask = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            BwTask.DoWork += ExecuteTask;
        }

        public override void SettingList(Dictionary<string, object> lists)
        {
            DirectoryCover = lists["Directories"] as List<DirectoryModel>;
            dynamic tmp;
            if (lists.TryGetValue("SortingRules", out tmp))
                SortRules = tmp as List<SortByRule>;
        }

        protected override void ExecuteTask(object sender, DoWorkEventArgs e)
        {
            base.ExecuteTask(sender, e);

            int res = 0;
            List<Tuple<int, string>> output = new List<Tuple<int, string>>();

            foreach (var des in DirectoryCover)
            {
                try
                {
                    if (!des.Activated)
                        continue;
                    DoSort(des.Path);
                }
                catch (Exception ex)
                {
                    output.Add(new Tuple<int, string>(-1, ex.Message));
                }
                if (BwTask.CancellationPending)
                {
                    e.Cancel = true;
                    e.Result = res;
                    return;
                }
                e.Result = res;
            }

        }

        private void DoSort(string mainDirectory)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(mainDirectory);

            var directories = from d in directoryInfo.GetDirectories("*", SearchOption.AllDirectories)
                              where (d.Attributes & (FileAttributes.)
            _input = Directory.GetDirectories(mainDirectory);
            _output = Directory.GetDirectories(mainDirectory);


        }
    }
}