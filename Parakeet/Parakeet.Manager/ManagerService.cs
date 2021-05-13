using Parakeet.Manager.Tasks;
using Parakeet.Models;
using Parakeet.Models.Outputs;
using System.IO;

namespace Parakeet.Manager
{

	public class ManagerService
	{
		private RemovingTask RemovingTask;
		private RenameTask RenameTask;
		private SortingTask SortTask;
		private DocDistTask DocDistTask;

		internal readonly IProjectHelper ProjectHelper;
		internal ManagerData ManagerData { get; private set; }
		internal ResultOutput Result { get; private set; }


		public ManagerService(IProjectHelper projectHelper)
		{
			this.ProjectHelper = projectHelper;
			Result = new ResultOutput();
			RemovingTask = new RemovingTask(this);
			RenameTask = new RenameTask(this);
			SortTask = new SortingTask(this);
			DocDistTask = new DocDistTask(this);
		}

		public void StartManaging(System.ComponentModel.DoWorkEventArgs e, Models.Inputs.LauncherParameter parameters)
		{
			ManagerData = new ManagerData(parameters);
			Result = new ResultOutput();

			DiscoveringPaths();
			RemovingTask.PerformTask();
			// After performing removing task, be sure to use IsPathValid for each PathsData
			RenameTask.PerformTask();
			SortTask.PerformTask();
			DocDistTask.PerformTask();

			Result.PathsDatas = ManagerData.Paths;
			e.Result = Result;
		}

		internal void UpdatePathsDataFromDirectoryMoving(string oldPath, string newPath)
		{
			foreach (var path in ManagerData.Paths)
			{
				if (path.ParentPath.StartsWith(oldPath))
					path.UpdateParent(oldPath, newPath);
			}
		}


		private void DiscoveringPaths()
		{
			foreach (var path in ProjectHelper.Project.PathModels)
			{
				if (path.Activated)
					RecursiveDiscoveryPaths(path.Path, true);
			}
		}

		private void RecursiveDiscoveryPaths(string path, bool isFromMainDirectories)
		{
			foreach (var file in Directory.GetFiles(path))
			{
				ManagerData.Paths.Add(new PathData(file, path, Models.Enums.Target.File, isFromMainDirectories));
			}
			foreach (var directory in Directory.GetDirectories(path))
			{
				ManagerData.Paths.Add(new PathData(directory, path, Models.Enums.Target.Folder, isFromMainDirectories));
				if (ManagerData.Parameters.IsRecursive)
					RecursiveDiscoveryPaths(directory, false);
			}
		}
	}
}
