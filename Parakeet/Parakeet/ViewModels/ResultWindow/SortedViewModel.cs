using Parakeet.Models.Outputs;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;

namespace Parakeet.ViewModels.ResultWindow
{
	public class SortedViewModel : BindableBase, INavigationAware
	{
		private List<SortResult> sortResults;
		public List<SortResult> SortResults
		{
			get { return sortResults; }
			set { SetProperty(ref sortResults, value); }
		}

		private List<FolderCreationResult> folderCreated;
		public List<FolderCreationResult> FolderCreated
		{
			get { return folderCreated; }
			set { SetProperty(ref folderCreated, value); }
		}

		public SortedViewModel()
		{ }

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			var resultsParameters = navigationContext.Parameters["ResultOutput"] as ResultOutput;
			SortResults = resultsParameters.SortingResults;
			FolderCreated = resultsParameters.FolderCreationResults;
		}
	}
}