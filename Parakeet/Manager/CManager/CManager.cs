using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
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

            Dictionary<string, int> first;
            Dictionary<string, int> second;
            List<DocDistResultModel> listResult = new List<DocDistResultModel>();

            foreach (var tmp in DirectoryCover)
            {
                if (tmp.Activated)
                    Search(tmp.Path);
            }
            foreach (var strToCheck in allData)
            {
                foreach (var strCompare in allData)
                {
                    if (strToCheck == strCompare)
                        continue;
                    first = ComputeFrequency(ParseDocument(Path.GetFileName(strToCheck)));
                    second = ComputeFrequency(ParseDocument(Path.GetFileName(strCompare)));
                    var dist = ComputeDistance(first, second);
                    var pourcent = 100 - (dist * 100 / Math.Acos(0));
                    if (pourcent >= DocDistRules.Threshold)
                        listResult.Add(new DocDistResultModel(strToCheck, strCompare, dist, pourcent));
                }
            }
            e.Result = listResult;
        }

        private double ComputeDistance(Dictionary<string, int> first, Dictionary<string, int> second)
        {
            int num = ComputeInnerProduc(first, second);
            double den = Math.Sqrt(ComputeInnerProduc(first, first) * ComputeInnerProduc(second, second));
            return Math.Acos(num / den);
        }

        private int ComputeInnerProduc(Dictionary<string, int> first, Dictionary<string, int> second)
        {
            int sum = 0;

            foreach (var key in first.Keys)
                if (second.ContainsKey(key))
                    sum += first[key] * second[key];
            return sum;
        }

        private Dictionary<string, int> ComputeFrequency(string elem)
        {
            Dictionary<string, int> stock = new Dictionary<string, int>();

            string[] tmp = elem.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tmp.Length; i++)
            {
                if (stock.ContainsKey(tmp[i]))
                    stock[tmp[i]]++;
                else
                    stock.Add(tmp[i], 1);
            }
            return stock;
        }

        private string ParseDocument(string elem)
        {
            Regex reg = new Regex("[^a-z0-9A-Z -]");
            return reg.Replace(elem, " ");
        }

        private void Search(string tmp)
        {
            if (DocDistRules.Target == Target.All || DocDistRules.Target == Target.Folder)
                allData.Add(tmp);
            if (DocDistRules.Target == Target.All || DocDistRules.Target == Target.File)
                foreach (var file in Directory.GetFiles(tmp))
                    allData.Add(file);
            if (DocDistRules.Target == Target.All || DocDistRules.Target == Target.Folder)
                foreach (var directory in Directory.GetDirectories(tmp))
                    Search(directory);
        }
    }
}
