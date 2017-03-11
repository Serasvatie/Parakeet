using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.Manager;

namespace CManager
{
    public class CManager : AManager
    {
        public CManager()
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
        }

        protected override void ExecuteTask(object sender, DoWorkEventArgs e)
        {
            base.ExecuteTask(sender, e);


        }
    }
}
