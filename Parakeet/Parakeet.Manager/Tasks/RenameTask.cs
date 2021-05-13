using Parakeet.Models.Enums;
using Parakeet.Models.Inputs;
using Parakeet.Models.Outputs;
using System;
using System.Diagnostics;
using System.IO;

namespace Parakeet.Manager.Tasks
{
	internal class RenameTask
	{
		private ManagerService managerService;
		private int id = 0;

		public RenameTask(ManagerService managerService)
		{
			this.managerService = managerService;
		}

		public void PerformTask()
		{
			if (managerService.ManagerData.Parameters.ShouldRename)
			{
				var paths = GeneratePaths();
				for (int i = 0; i < paths.Length; i++)
				{
					var path = paths[i];
					if (!path.IsPathValid())
						continue;
					foreach (var rule in managerService.ProjectHelper.Project.RenameRules)
					{
						if (!rule.IsActivated)
							continue;

						// Check if old exist
						if (!Path.GetFileNameWithoutExtension(path.CurrentPath).Contains(rule.Old))
							continue;

						int index = path.CurrentPath.LastIndexOf("\\", StringComparison.Ordinal);
						// Path.Join (Path, filename or foldername) + Extension if filename
						string newPath = Path.Join(path.CurrentPath.Substring(0, index),
												   Path.GetFileNameWithoutExtension(path.CurrentPath).Replace(rule.Old, rule.New))
										+ Path.GetExtension(path.CurrentPath);
						if (newPath == path.CurrentPath)
							continue;

						if (path.Target == Target.Folder && (rule.Target == Target.Folder || rule.Target == Target.All))
						{
							var oldPath = path.CurrentPath;

							Directory.Move(path.CurrentPath, newPath);
							LogResult(rule, oldPath, newPath, path.Target);
							path.SetNewPath(newPath);

							managerService.UpdatePathsDataFromDirectoryMoving(oldPath, newPath);
							paths = GeneratePaths();
							i = 0;
							break;
						}
						if (path.Target == Target.File && (rule.Target == Target.File || rule.Target == Target.All))
						{
							try
							{
								File.Move(path.CurrentPath, newPath);
								LogResult(rule, path.CurrentPath, newPath, path.Target);
								path.SetNewPath(newPath);
							}
							catch (Exception ex)
							{
								Debug.WriteLine(ex);
							}
						}
					}
				}
			}
		}

		private void LogResult(RenameRule rule, string oldPath, string newPath, Target type)
		{
			managerService.Result.RenameResults.Add(new RenameResult()
			{
				Id = id++,
				Rule = rule,
				OldPath = oldPath,
				NewPath = newPath,
				Type = type
			});
		}

		private PathData[] GeneratePaths()
		{
			return managerService.ManagerData.Paths.ToArray();
		}
	}
}
