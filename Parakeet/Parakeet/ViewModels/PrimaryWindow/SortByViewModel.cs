using Parakeet.Models;
using Parakeet.Models.Inputs;
using Parakeet.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Parakeet.ViewModels.PrimaryWindow
{
	public class SortByViewModel : BindableBase
	{
		private int _selectedIndex;
		private string _strings;
		private readonly IProjectHelper projectHelper;

		public ICommand AddEntry { get; private set; }
		public ICommand DeleteEntry { get; private set; }
		public ICommand DoUp { get; private set; }
		public ICommand DoDown { get; private set; }

		public ObservableCollection<SortByRule> ListRules
		{
			get { return projectHelper.Project.SortingRules; }
		}

		public int SelectedIndex
		{
			get { return _selectedIndex; }
			set { SetProperty(ref _selectedIndex, value); }
		}

		public string Strings
		{
			get { return _strings; }
			set { SetProperty(ref _strings, value); }
		}

		public SortByViewModel(IProjectHelper projectHelper, IEventAggregator ea)
		{
			SelectedIndex = 0;
			Strings = "";
			this.projectHelper = projectHelper;

			ea.GetEvent<ProjectChangedEvent>().Subscribe(ProjectChanged);

			AddEntry = new DelegateCommand(DoAddRules, CanAddRules)
				.ObservesProperty(() => Strings);
			DeleteEntry = new DelegateCommand(DoDeleteRules, CanDeleteRules)
				.ObservesProperty(() => SelectedIndex);
			DoUp = new DelegateCommand(DoUpC, CanUp)
				.ObservesProperty(() => SelectedIndex);
			DoDown = new DelegateCommand(DoDownC, CanDown)
				.ObservesProperty(() => SelectedIndex);
		}

		private void ProjectChanged()
		{
			RaisePropertyChanged("");
		}

		private bool CanAddRules()
		{
			return !string.IsNullOrEmpty(Strings);
		}

		private void DoAddRules()
		{
			var tmp = new SortByRule(Strings, true);
			ListRules.Add(tmp);
			Strings = null;
			SelectedIndex = -1;
		}

		private bool CanDeleteRules()
		{
			return SelectedIndex >= 0;
		}

		private void DoDeleteRules()
		{
			ListRules.RemoveAt(SelectedIndex);
			SelectedIndex = -1;
		}

		private bool CanUp()
		{
			return SelectedIndex >= 1;
		}

		private void DoUpC()
		{
			ListRules.Move(SelectedIndex, SelectedIndex - 1);
		}

		private bool CanDown()
		{
			return SelectedIndex < ListRules.Count - 1;
		}

		private void DoDownC()
		{
			ListRules.Move(SelectedIndex, SelectedIndex + 1);
		}
	}
}