using Parakeet.Models.Outputs;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;

namespace Parakeet.ViewModels.ResultWindow
{
	public class DataViewModel : BindableBase, INavigationAware
	{
		public DataViewModel()
		{ }

		private List<PathData> pathsDatas;
		public List<PathData> PathsDatas
		{
			get { return pathsDatas; }
			set { SetProperty(ref pathsDatas, value); }
		}

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
			PathsDatas = resultsParameters.PathsDatas;
		}
	}
}
