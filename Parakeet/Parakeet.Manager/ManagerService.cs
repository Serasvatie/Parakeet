using Parakeet.Manager.Tasks;
using Parakeet.Models;
using Parakeet.Models.Outputs;
using System;
using System.IO;

namespace Parakeet.Manager
{

	public class ManagerService
	{
		private RemovingTask RemovingTask;
		private RenameTask RenameTask;
		private SortingTask SortTask;
		private DocDistTask DocDistTask;
		private int currentProgress;

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

		public void StartManaging(System.ComponentModel.BackgroundWorker _backgroundWorker, System.ComponentModel.DoWorkEventArgs e, Models.Inputs.LauncherParameter parameters)
		{
			currentProgress = 0;
			ManagerData = new ManagerData(parameters);
			Result = new ResultOutput();

			DiscoveringPaths();
			ReportProgress(_backgroundWorker, 20);
			HandleTaskWithCancellation(_backgroundWorker,
				() => RemovingTask.PerformTask(),
				// After performing removing task, be sure to use IsPathValid for each PathsData
				() => RenameTask.PerformTask(),
				() => SortTask.PerformTask(),
				() => DocDistTask.PerformTask()
				);

			Result.PathsDatas = ManagerData.Paths;
			e.Result = Result;
		}

		private void HandleTaskWithCancellation(System.ComponentModel.BackgroundWorker _backgroundWorker, params Action[] tasks)
		{
			for (int i = 0; i < tasks.Length; i++)
			{
				var task = tasks[i];
				if (_backgroundWorker.CancellationPending)
					break;
				task.Invoke();
				ReportProgress(_backgroundWorker, i * 80 / tasks.Length);
			}
		}

		private void ReportProgress(System.ComponentModel.BackgroundWorker _backgroundWorker, int percentageToAdd)
		{
			currentProgress += percentageToAdd;
			_backgroundWorker.ReportProgress(currentProgress);
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
