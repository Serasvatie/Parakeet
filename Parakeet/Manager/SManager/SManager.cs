using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
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
                    res = DoSort(des.Path);
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

        private int DoSort(string mainDirectory)
        {
            int res = 0;
            _input = Directory.GetDirectories(mainDirectory);
            for (int i = 0; i < _input.Length; i++)
            {
                var nDir = DoCheckRules(_input[i]);
                if (nDir != "")
                {
                    moveDirectory(mainDirectory, _input[i], nDir);
                    res++;
                    int save = _input.Length;
                    _input = Directory.GetDirectories(mainDirectory);
                    if (save != _input.Length)
                        i = 0;
                }
            }
            return res;
        }

        private void moveDirectory(string mainDirectory, string target, string ndir)
        {
            if (Directory.Exists(mainDirectory + ndir))
            {
                Directory.Move(target, mainDirectory + "\\" + ndir + Path.GetFileName(target));
            }
            else
            {
                Directory.CreateDirectory(mainDirectory + "\\" + ndir);
                Directory.Move(target, mainDirectory + "\\" + ndir + Path.GetFileName(target));
            }
        }

        private string DoCheckRules(string p)
        {
            foreach (var rule in SortRules)
            {
                if (!rule.IsActivated)
                    continue;
                var tmp = Regex.Match(Path.GetFileNameWithoutExtension(p), rule.Strings).Groups[0].Value;
                if (tmp != "")
                    return tmp;
            }
            return "";
        }
    }
}