using Parakeet.Models;
using Parakeet.Services;
using Prism.Events;
using Prism.Mvvm;

namespace Parakeet.ViewModels.PrimaryWindow
{
	public class StatusBarViewModel : BindableBase
	{
		private readonly IProjectHelper projectHelper;

		public StatusBarViewModel(IProjectHelper projectHelper, IEventAggregator ea)
		{
			this.projectHelper = projectHelper;

			ea.GetEvent<ProjectChangedEvent>().Subscribe(ProjectChanged);
		}

		private void ProjectChanged()
		{
			RaisePropertyChanged("");
		}

		public string FileName => projectHelper.FileName;

		public string StatusTask
		{
			get { return ""; }
		}

		public bool StatusValue
		{
			get { return false; }
		}
	}
}