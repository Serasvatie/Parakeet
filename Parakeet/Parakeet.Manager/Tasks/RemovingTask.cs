using Parakeet.Models.Outputs;
using System.IO;
using System.Linq;

namespace Parakeet.Manager.Tasks
{
	internal class RemovingTask
	{
		private ManagerService managerService;

		public RemovingTask(ManagerService managerService)
		{
			this.managerService = managerService;
		}

		public void PerformTask()
		{
			if (managerService.ManagerData.Parameters.ShouldRemove)
			{
				var files = managerService.ManagerData.Paths.Where(path => path.Target == Models.Enums.Target.File).ToList();
				foreach (var path in files)
				{
					foreach (var rule in managerService.ProjectHelper.Project.RemoveRules)
					{
						if (!rule.IsActivated)
							continue;
						if (rule.IsExtension)
						{
							if (Path.GetExtension(path.CurrentPath) == "." + rule.Strings)
								DeletePath(path);
							continue;
						}
						else if (Path.GetFileNameWithoutExtension(path.CurrentPath).Contains(rule.Strings))
							DeletePath(path);
					}
				}
			}
		}

		private void DeletePath(PathData path)
		{
			File.Delete(path.CurrentPath);
			path.SetNewPath(null);
			managerService.Result.RemoveResults.Add(new RemoveResult(path));
		}
	}
}
