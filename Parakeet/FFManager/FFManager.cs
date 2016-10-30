using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace FFManager
{
    public class FFManager
    {
        private List<DirectoryModel> _directory;
        private List<RemoveRule> _removeRules;
        private List<ChangeRule> _renameRules;
        private bool _recursive;

        public FFManager()
        {
        }

        public void SettingLists(Dictionary<string, object> lists)
        {
            dynamic tmp;
            lists.TryGetValue("Recursive", out tmp);
            _recursive = tmp ? tmp : false;
            lists.TryGetValue("Directories", out tmp);
            _directory = tmp as List<DirectoryModel>;
            lists.TryGetValue("RemovingRules", out tmp);
            _removeRules = tmp as List<RemoveRule>;
            lists.TryGetValue("RenamingRules", out tmp);
            _renameRules = tmp as List<ChangeRule>;
        }

        private int? DoRemove(string target)
        {
            int? res = null;
            foreach (var val in _removeRules)
            {
                if (!val.IsActivated)
                    continue;
                if (val.IsExtension)
                {
                    if (Path.GetExtension(target) == val.Strings)
                    {
                        File.Delete(target);
                        res++;
                    }
                    continue;
                }
                if (target.Contains(val.Strings))
                {
                    File.Delete(target);
                    res++;
                }
            }
            return res;
        }

        private int? DoRename(string target)
        {
            int? res = null;
            foreach (var rule in _renameRules)
            {
                if (!rule.IsActivate)
                    continue;
                int index = target.LastIndexOf("\\", StringComparison.Ordinal);
                string newPath = target.Substring(0, index) + target.Substring(index).Replace(rule.Old, rule.New);
                if (newPath == target)
                {
                    continue;
                }
                if (Directory.Exists(target) && (rule.Target == Target.Folder || rule.Target == Target.All))
                {
                    File.Move(target, newPath);
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

        private int? DoSort()
        {
            return 0;
        }

        private int? RecursiveTask(string Path)
        {
            int? res = null;
            foreach (var d in Directory.GetDirectories(Path))
            {
                foreach (var f in Directory.GetFiles(Path))
                {
                    res += DoRemove(f);
                    res += DoRename(f);
                }
                if (_recursive)
                    RecursiveTask(d);
                res += DoRename(d);
            }
            return res;
        }

        private int? EachElement()
        {
            int? res = null;

            foreach (var des in _directory)
            {
                if (!des.Activated)
                    continue;
                res = RecursiveTask(des.Path);
            }
            return res;
        }

        public int? ExecuteTask()
        {
            return EachElement();
        }
    }
}