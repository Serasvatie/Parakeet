using Parakeet.Models.Outputs;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;

namespace Parakeet.ViewModels.ResultWindow
{
	public class RemovedViewModel : BindableBase, INavigationAware
	{
		private List<RemoveResult> removeResults;
		public List<RemoveResult> RemoveResults
		{
			get { return removeResults; }
			set { SetProperty(ref removeResults, value); }
		}

		public RemovedViewModel()
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
			RemoveResults = resultsParameters.RemoveResults;
		}
	}
}
