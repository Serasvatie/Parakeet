using Parakeet.Models.Outputs;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;

namespace Parakeet.ViewModels.ResultWindow
{
	public class RenamedViewModel : BindableBase, INavigationAware
	{
		private List<RenameResult> renamedResults;
		public List<RenameResult> RenamedResults
		{
			get { return renamedResults; }
			set { SetProperty(ref renamedResults, value); }
		}

		public RenamedViewModel()
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
			RenamedResults = resultsParameters.RenameResults;
		}
	}
}
