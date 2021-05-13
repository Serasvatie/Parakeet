using Parakeet.Models.Outputs;
using Parakeet.Properties;
using Parakeet.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;

namespace Parakeet.ViewModels.ResultWindow
{
	public class ResultsViewModel : BindableBase, IDialogAware
	{
		private ResultOutput _resultOutput;
		private readonly IRegionManager regionManager;

		public string Title => Strings.ResultsView_Title;

		public ICommand DeletedCommand { get; private set; }
		public ICommand RenamedCommand { get; private set; }
		public ICommand SortedCommand { get; private set; }
		public ICommand DocDistCommand { get; private set; }
		public ICommand DataCommand { get; private set; }
		public ICommand CloseDialogCommand { get; private set; }
		public event Action<IDialogResult> RequestClose;

		public ResultOutput ResultOutput
		{
			get { return _resultOutput; }
			set { SetProperty(ref _resultOutput, value); }
		}

		public ResultsViewModel(IRegionManager regionManager)
		{
			this.regionManager = regionManager;
			CloseDialogCommand = new DelegateCommand(CloseDialog);
			DeletedCommand = new DelegateCommand(ShowDeleted, CanShowDeleted).ObservesProperty(() => ResultOutput);
			RenamedCommand = new DelegateCommand(ShowRenamed, CanShowRenamed).ObservesProperty(() => ResultOutput);
			SortedCommand = new DelegateCommand(ShowSorted, CanShowSorted).ObservesProperty(() => ResultOutput);
			DocDistCommand = new DelegateCommand(ShowDocDist, CanShowDocDist).ObservesProperty(() => ResultOutput);
			DataCommand = new DelegateCommand(ShowData);
		}

		#region Command
		private void ShowData()
		{
			NavigateResultsRegion("DataView");
		}

		private void ShowDocDist()
		{
			NavigateResultsRegion("DocDistResultView");
		}

		private bool CanShowDocDist()
		{
			return ResultOutput == null ? false : ResultOutput.DocDistResults.Count != 0;
		}

		private bool CanShowSorted()
		{
			return ResultOutput == null ? false : ResultOutput.SortingResults.Count != 0;
		}

		private void ShowSorted()
		{
			NavigateResultsRegion("SortedView");
		}

		private void ShowRenamed()
		{
			NavigateResultsRegion("RenamedView");
		}

		private bool CanShowRenamed()
		{
			return ResultOutput == null ? false : ResultOutput.RenameResults.Count != 0;
		}

		private bool CanShowDeleted()
		{
			return ResultOutput == null ? false : ResultOutput.RemoveResults.Count != 0;
		}

		private void ShowDeleted()
		{
			NavigateResultsRegion("RemovedView");
		}

		private void CloseDialog()
		{
			RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
		}

		public bool CanCloseDialog()
		{
			return true;
		}

		private void NavigateResultsRegion(string viewName)
		{
			var navParams = new NavigationParameters();
			navParams.Add("ResultOutput", ResultOutput);
			regionManager.RequestNavigate("ResultsRegion", viewName, navParams);
		}
		#endregion

		#region IDialogAware
		public void OnDialogClosed()
		{ }

		public void OnDialogOpened(IDialogParameters parameters)
		{
			var resultParams = parameters as ResultsDialogParameters;
			ResultOutput = resultParams.Result;
		}
		#endregion
	}
}
