using Parakeet.Models.Outputs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Parakeet.Manager.Tasks
{
	internal class DocDistTask
	{
		private ManagerService managerService;

		public DocDistTask(ManagerService managerService)
		{
			this.managerService = managerService;
		}

		public void PerformTask()
		{
			if (managerService.ManagerData.Parameters.ShouldDocDist)
			{
				Dictionary<string, int> first;
				Dictionary<string, int> second;

				List<PathData> targets;
				if (managerService.ProjectHelper.Project.DocDist.Target == Models.Enums.Target.All)
					targets = managerService.ManagerData.Paths;
				else
					targets = managerService.ManagerData.Paths.Where(paths => paths.Target == managerService.ProjectHelper.Project.DocDist.Target).ToList();
				foreach (var firstPath in targets)
				{
					if (!firstPath.IsPathValid())
						continue;
					foreach (var secondPath in targets)
					{
						if (!secondPath.IsPathValid())
							continue;
						if (firstPath == secondPath)
							continue;

						first = ComputeFrequency(ParseDocument(Path.GetFileName(firstPath.CurrentPath)));
						second = ComputeFrequency(ParseDocument(Path.GetFileName(secondPath.CurrentPath)));
						var dist = ComputeDistance(first, second);
						var pourcent = Math.Floor(100 - (dist * 100 / Math.Acos(0)));
						if (pourcent >= managerService.ProjectHelper.Project.DocDist.Threshold)
						{
							DocDistResultModel res = new DocDistResultModel(firstPath, secondPath, dist, pourcent);
							if (!managerService.Result.DocDistResults.Any(o => o.Equals(res)))
								managerService.Result.DocDistResults.Add(res);
						}
					}
				}
			}
		}

		private double ComputeDistance(Dictionary<string, int> first, Dictionary<string, int> second)
		{
			int num = ComputeInnerProduct(first, second);
			double den = Math.Sqrt(ComputeInnerProduct(first, first) * ComputeInnerProduct(second, second));
			return Math.Acos(num / den);
		}

		private int ComputeInnerProduct(Dictionary<string, int> first, Dictionary<string, int> second)
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
			Regex reg = new Regex("[^a-z0-9A-Z ]");
			return reg.Replace(elem, " ");
		}

	}
}
