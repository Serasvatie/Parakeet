using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Manager
{
    public abstract class AManager
    {
        protected List<DirectoryModel> DirectoryCover = new List<DirectoryModel>();
        protected List<RemoveRule> RemoveRules = new List<RemoveRule>();
        protected List<ChangeRule> RenameRules = new List<ChangeRule>();
        protected List<SortByRule> SortRules = new List<SortByRule>();
        protected DocDistModel DocDistRules;

        protected bool Recursive;

        public event EventHandler IsBwStarted;
        public BackgroundWorker BwTask;

        public abstract void SettingList(Dictionary<string, object> lists);

        protected virtual void ExecuteTask(object sender, DoWorkEventArgs e)
        {
            IsBwStarted?.Invoke(this, null);
        }
    }
}