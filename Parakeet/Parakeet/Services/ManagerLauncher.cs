using Parakeet.Manager;
using Parakeet.Models.Inputs;
using Parakeet.Models.Outputs;
using Parakeet.Properties;
using Prism.Events;
using Prism.Services.Dialogs;
using System.ComponentModel;

namespace Parakeet.Services
{
	public class ManagerLauncher
	{

		private BackgroundWorker _backgroundWorker;
		private readonly ManagerService manager;
		private readonly IDialogService dialogService;

		public ManagerLauncher(IEventAggregator ea, ManagerService manager, IDialogService dialogService)
		{
			ea.GetEvent<ManagerLaunchEvent>().Subscribe(LaunchEvent);
			this.manager = manager;
			this.dialogService = dialogService;
		}

		private void LaunchEvent()
		{
			_backgroundWorker = new BackgroundWorker()
			{
				WorkerReportsProgress = true,
				WorkerSupportsCancellation = true,
			};
			_backgroundWorker.DoWork += ExecuteManager;
			_backgroundWorker.RunWorkerCompleted += ManagerCompleted;
			_backgroundWorker.RunWorkerAsync();
		}

		private void ManagerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			dialogService.ShowDialog("ResultsDialog", new ResultsDialogParameters(e.Result as ResultOutput), r => { });
		}

		private void ExecuteManager(object sender, DoWorkEventArgs e)
		{
			var parameters = new LauncherParameter()
			{
				IsRecursive = Settings.Default.TaskRecursive,
				ShouldDocDist = Settings.Default.TaskCheck,
				ShouldRemove = Settings.Default.TaskRemove,
				ShouldRename = Settings.Default.TaskRename,
				ShouldSort = Settings.Default.TaskSort,
			};
			manager.StartManaging(e, parameters);
		}
	}
}
