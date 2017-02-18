using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Manager.Manager;

namespace FFManager
{
    public class FFManager
    {
        private List<DirectoryModel> _directory = new List<DirectoryModel>();
        private List<RemoveRule> _removeRules = new List<RemoveRule>();
        private List<ChangeRule> _renameRules = new List<ChangeRule>();
        private bool _recursive;

        public event EventHandler IsBwStarted;
        public BackgroundWorker BwTask;

        public FFManager()
        {
            BwTask = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            BwTask.DoWork += ExecuteTask;
        }

        public void SettingLists(Dictionary<string, object> lists)
        {
            _recursive = (bool) lists["Recursive"];
            _directory = lists["Directories"] as List<DirectoryModel>;
            dynamic tmp;
            if (lists.TryGetValue("RemovingRules", out tmp))
                _removeRules = tmp as List<RemoveRule>;
            tmp = null;
            if (lists.TryGetValue("RenamingRules", out tmp))
                _renameRules = tmp as List<ChangeRule>;
        }

        private void ExecuteTask(object sender, DoWorkEventArgs e)
        {
            int res = 0;
            List<Tuple<int, string>> output = new List<Tuple<int, string>>();

            IsBwStarted?.Invoke(this, null);
            foreach (var des in _directory)
            {
                try
                {
                    if (!des.Activated)
                        continue;
                    res = RecursiveTask(des.Path);
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

        private int DoRemove(string target)
        {
            int res = 0;
            foreach (var val in _removeRules)
            {
                if (!val.IsActivated)
                    continue;
                if (val.IsExtension)
                {
                    if (Path.GetExtension(target) == "." + val.Strings)
                    {
                        File.Delete(target);
                        res++;
                    }
                    continue;
                }
                else if (Path.GetFileNameWithoutExtension(target).Contains(val.Strings))
                {
                    File.Delete(target);
                    res++;
                }
            }
            return res;
        }

        private int DoRename(string target)
        {
            int res = 0;
            foreach (var rule in _renameRules)
            {
                if (!rule.IsActivate)
                    continue;
                if (!Path.GetFileNameWithoutExtension(target).Contains(rule.Old))
                    return res;
                int index = target.LastIndexOf("\\", StringComparison.Ordinal);
                string newPath = target.Substring(0, index) + "\\" +
                                 Path.GetFileNameWithoutExtension(target).Replace(rule.Old, rule.New) +
                                 Path.GetExtension(target);
                if (newPath == target)
                {
                    continue;
                }
                if (Directory.Exists(target) && (rule.Target == Target.Folder || rule.Target == Target.All))
                {
                    Directory.Move(target, newPath);
                    res++;
                    continue;
                }
                if (File.Exists(target) && (rule.Target == Target.File || rule.Target == Target.All))
                {
                    File.Move(target, newPath);
                    res++;
                    continue;
                }
            }
            return res;
        }

        private int RecursiveTask(string path)
        {
            int res = 0;

            foreach (var f in Directory.GetFiles(path))
            {
                if (BwTask.CancellationPending)
                    return res;
                res += DoRemove(f);
                res += DoRename(f);
            }
            foreach (var d in Directory.GetDirectories(path))
            {
                if (BwTask.CancellationPending)
                    return res;
                if (_recursive)
                    res += RecursiveTask(d);
                res += DoRename(d);
            }
            return res;
        }
    }
}