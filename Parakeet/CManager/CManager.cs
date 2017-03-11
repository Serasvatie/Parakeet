using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager;
using Manager.Manager;

namespace CManager
{
    public class CManager : AManager
    {
        private List<string> allData;

        public CManager()
        {
            BwTask = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            BwTask.DoWork += ExecuteTask;
            allData = new List<string>();
        }

        public override void SettingList(Dictionary<string, object> lists)
        {
            DirectoryCover = lists["Directories"] as List<DirectoryModel>;
            dynamic tmp;
            if (lists.TryGetValue("DocDistRules", out tmp))
                DocDistRules = tmp as DocDistModel;
        }

        protected override void ExecuteTask(object sender, DoWorkEventArgs e)
        {
            base.ExecuteTask(sender, e);

            foreach (var tmp in DirectoryCover)
            {
                if (tmp.Activated)
                    Search(tmp.Path);
            }
        }

        private void Search(string tmp)
        {
            if (DocDistRules.Target == Target.All || DocDistRules.Target == Target.Folder)
                allData.Add(tmp);
            if (DocDistRules.Target == Target.All || DocDistRules.Target == Target.File)
                foreach (var file in Directory.GetFiles(tmp))
                    allData.Add(tmp);
            if (DocDistRules.Target == Target.All || DocDistRules.Target == Target.Folder)
                foreach (var directory in Directory.GetDirectories(tmp))
                    Search(directory);
        }
    }
}
