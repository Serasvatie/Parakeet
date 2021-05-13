using Parakeet.Models;
using Parakeet.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Input;

namespace Parakeet.ViewModels.PrimaryWindow
{
	public class StatusBarViewModel : BindableBase
	{
		private readonly IProjectHelper projectHelper;
		private readonly ManagerLauncher managerLauncher;
		private bool isManagerBusy;

		public bool IsManagerBusy
		{
			get { return isManagerBusy; }
			set { SetProperty(ref isManagerBusy, value); }
		}


		public ICommand CancelCommand { get; private set; }

		public StatusBarViewModel(IProjectHelper projectHelper, IEventAggregator ea, ManagerLauncher managerLauncher)
		{
			this.projectHelper = projectHelper;
			this.managerLauncher = managerLauncher;
			ea.GetEvent<ProjectChangedEvent>().Subscribe(ProjectChanged);
			ea.GetEvent<ManagerLaunchingEvent>().Subscribe(ManagerLaunching);
			ea.GetEvent<ManagerLaunchedEvent>().Subscribe(ManagerLaunched);
			ea.GetEvent<ManagerLaunchingProgressReportEvent>().Subscribe(ProgressReport);
			CancelCommand = new DelegateCommand(DoCancel, CanCancel).ObservesProperty(() => IsManagerBusy);
		}

		private void ProgressReport(int obj)
		{
			Progress = obj;
		}

		private void ManagerLaunching()
		{
			IsManagerBusy = true;
		}

		private void ManagerLaunched()
		{
			IsManagerBusy = false;
			Progress = 0;
		}

		private void DoCancel()
		{
			managerLauncher.CancelWorker();
		}

		private bool CanCancel()
		{
			return IsManagerBusy;
		}

		private void ProjectChanged()
		{
			RaisePropertyChanged("");
		}

		public string FileName => projectHelper.FileName;

		private int progress;
		public int Progress
		{
			get { return progress; }
			set { SetProperty(ref progress, value); }
		}
	}
}