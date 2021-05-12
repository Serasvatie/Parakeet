using Parakeet.Models.Outputs;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Parakeet.Manager.Tasks
{
	internal class SortingTask
	{
		private ManagerService managerService;

		public SortingTask(ManagerService managerService)
		{
			this.managerService = managerService;
		}

		public void PerformTask()
		{
			PathData[] GenerateSubOfRootDirectories()
			{
				return managerService.ManagerData.Paths.Where(path => path.IsFromMainDirectories == true).ToArray();
			}

			if (managerService.ManagerData.Parameters.ShouldSort)
			{
				PathData[] subOfRootDirectories = GenerateSubOfRootDirectories();
				for (int i = 0; i < subOfRootDirectories.Length; i++)
				{
					var path = subOfRootDirectories[i];
					if (!path.IsPathValid())
						continue;
					var newDir = DoCheckRules(path.CurrentPath);
					if (!string.IsNullOrEmpty(newDir))
					{
						MovePath(path, newDir);
						subOfRootDirectories = GenerateSubOfRootDirectories();
						i = 0;
					}
				}
			}
		}

		private void MovePath(PathData path, string newDir)
		{
			// Build destination folder
			string destinationFolder = Path.Join(path.ParentPath, newDir);

			if (!managerService.ManagerData.Paths.Any(path => path.CurrentPath == destinationFolder))
			{
				Directory.CreateDirectory(destinationFolder);
				managerService.ManagerData.Paths.Add(new PathData(destinationFolder, path.ParentPath, Models.Enums.Target.Folder, true));
				managerService.Result.FolderCreationResults.Add(new FolderCreationResult
				{
					FolderCreatedPath = destinationFolder,
				});
			}

			var newPath = Path.Join(destinationFolder, Path.GetFileName(path.CurrentPath));
			Directory.Move(path.CurrentPath, newPath);
			managerService.Result.SortingResults.Add(new SortResult()
			{
				NewPath = newPath,
				OldPath = path.CurrentPath,
				Type = path.Target,
				Destination = destinationFolder,
			});
			path.MovePath(destinationFolder, newPath, false);
		}

		private string DoCheckRules(string p)
		{
			foreach (var rule in managerService.ProjectHelper.Project.SortingRules)
			{
				if (!rule.IsActivated)
					continue;

				var tmp = Regex.Match(Path.GetFileNameWithoutExtension(p), rule.Strings).Groups[0].Value;
				if (tmp != "")
				{
					return tmp;
				}
			}
			return null;
		}
	}
}
