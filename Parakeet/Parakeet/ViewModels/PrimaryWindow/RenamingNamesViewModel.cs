using Parakeet.Models;
using Parakeet.Models.Enums;
using Parakeet.Models.Inputs;
using Parakeet.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Parakeet.ViewModels.PrimaryWindow
{
	class RenamingNamesViewModel : BindableBase
	{
		private readonly IProjectHelper _projectHelper;

		private int _selectedItem;
		private string _changeName;
		private string _byName;

		public ICommand AddEntry { get; private set; }
		public ICommand DeleteEntry { get; private set; }
		public ICommand DoUp { get; private set; }
		public ICommand DoDown { get; private set; }

		#region Property
		public ObservableCollection<RenameRule> ListChangeRules
		{
			get { return _projectHelper.Project.RenameRules; }
		}

		public int SelectedIndex
		{
			get { return _selectedItem; }
			set { SetProperty(ref _selectedItem, value); }
		}

		public string ChangeName
		{
			get { return _changeName; }
			set { SetProperty(ref _changeName, value); }
		}

		public string ByName
		{
			get { return _byName; }
			set { SetProperty(ref _byName, value); }
		}

		#endregion

		public RenamingNamesViewModel(IProjectHelper projectHelper, IEventAggregator ea)
		{
			_projectHelper = projectHelper;

			ea.GetEvent<ProjectChangedEvent>().Subscribe(ProjectChanged);

			AddEntry = new DelegateCommand(DoAddRules, CanAddRules)
				.ObservesProperty(() => ChangeName).ObservesProperty(() => ByName);
			DeleteEntry = new DelegateCommand(DoDeleteRules, CanDeleteRules)
				.ObservesProperty(() => SelectedIndex);
			DoUp = new DelegateCommand(DoUpC, CanUp).ObservesProperty(() => SelectedIndex);
			DoDown = new DelegateCommand(DoDownC, CanDown).ObservesProperty(() => SelectedIndex);

			SelectedIndex = -1;
		}

		private void ProjectChanged()
		{
			RaisePropertyChanged("");
		}

		#region Commands
		private bool CanAddRules()
		{
			return !string.IsNullOrEmpty(ChangeName) && !string.IsNullOrEmpty(ByName);
		}

		private void DoAddRules()
		{
			var tmp = new RenameRule(ChangeName, ByName, true, Target.All);
			ChangeName = null;
			ByName = null;
			ListChangeRules.Add(tmp);
			SelectedIndex = -1;
		}

		private bool CanDeleteRules()
		{
			return SelectedIndex > 0;
		}

		private void DoDeleteRules()
		{
			ListChangeRules.RemoveAt(SelectedIndex);
			SelectedIndex = -1;
		}

		private bool CanUp()
		{
			return SelectedIndex >= 1;
		}

		private void DoUpC()
		{
			ListChangeRules.Move(SelectedIndex, SelectedIndex - 1);
		}

		private bool CanDown()
		{
			return SelectedIndex < ListChangeRules.Count - 1;
		}

		private void DoDownC()
		{
			ListChangeRules.Move(SelectedIndex, SelectedIndex + 1);
		}
		#endregion
	}
}