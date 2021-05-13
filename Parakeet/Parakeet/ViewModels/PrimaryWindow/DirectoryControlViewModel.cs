using Parakeet.Models;
using Parakeet.Models.Inputs;
using Parakeet.Properties;
using Parakeet.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;

namespace Parakeet.ViewModels.PrimaryWindow
{
	public class DirectoryControlViewModel : BindableBase
	{
		private readonly IProjectHelper _projectHelper;
		private readonly IEventAggregator ea;
		private readonly IDialogService dialogService;
		private readonly ManagerLauncher managerLauncher;
		private int _selectedItem;

		public ICommand AddEntry { get; private set; }
		public ICommand DeleteEntry { get; private set; }
		public ICommand Start { get; private set; }
		public ICommand DoUp { get; private set; }
		public ICommand DoDown { get; private set; }

		public ObservableCollection<PathModel> ListDirectory
		{
			get { return _projectHelper.Project.PathModels; }
		}

		public int SelectedItem
		{
			get { return _selectedItem; }
			set { SetProperty(ref _selectedItem, value); }
		}

		public DirectoryControlViewModel(IProjectHelper projectHelper, IEventAggregator ea, IDialogService dialogService, ManagerLauncher managerLauncher)
		{
			_projectHelper = projectHelper;
			this.ea = ea;
			this.dialogService = dialogService;
			this.managerLauncher = managerLauncher;
			ea.GetEvent<ProjectChangedEvent>().Subscribe(ProjectChanged);

			AddEntry = new DelegateCommand(DoAddPath);
			DeleteEntry = new DelegateCommand(DoDeletePath, CanDeletePath).ObservesProperty(() => SelectedItem);
			Start = new DelegateCommand(DoStart, CanStart).ObservesProperty(() => ListDirectory.Count);
			DoUp = new DelegateCommand(DoUpC, CanUp).ObservesProperty(() => SelectedItem);
			DoDown = new DelegateCommand(DoDownC, CanDown).ObservesProperty(() => SelectedItem);

			SelectedItem = -1;
		}

		private void ProjectChanged()
		{
			RaisePropertyChanged("");
		}

		private void DoAddPath()
		{
			var dialog = new FolderBrowserDialog
			{
				Description = Strings.DirectoryControlViewModel_DoAddDirectory_Select_a_folder_to_add____,
				ShowNewFolderButton = false
			};
			System.Windows.Forms.DialogResult result = dialog.ShowDialog();
			if (result != System.Windows.Forms.DialogResult.OK)
				return;
			var tmp = new PathModel(dialog.SelectedPath, true);
			ListDirectory.Add(tmp);
			SelectedItem = -1;
		}

		private bool CanDeletePath()
		{
			return SelectedItem >= 0;
		}

		private void DoDeletePath()
		{
			ListDirectory.RemoveAt(SelectedItem);
			SelectedItem = -1;
		}

		private bool CanStart()
		{
			return ListDirectory.Count > 0;
		}

		private void DoStart()
		{
			//using the dialog service as-is
			dialogService.ShowDialog("TaskDialog", new DialogParameters(), r =>
			{
				if (r.Result == ButtonResult.OK)
					this.ea.GetEvent<ManagerLaunchEvent>().Publish();
			});
		}

		private bool CanUp()
		{
			return SelectedItem >= 1;
		}

		private void DoUpC()
		{
			ListDirectory.Move(SelectedItem, SelectedItem - 1);
		}

		private bool CanDown()
		{
			return SelectedItem < ListDirectory.Count - 1;
		}

		private void DoDownC()
		{
			ListDirectory.Move(SelectedItem, SelectedItem + 1);
		}
	}
}