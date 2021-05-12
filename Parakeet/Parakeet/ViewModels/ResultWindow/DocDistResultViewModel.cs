using Parakeet.Models.Outputs;
using Prism.Mvvm;
using Prism.Regions;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace Parakeet.ViewModels.ResultWindow
{
	public class DocDistResultViewModel : BindableBase, INavigationAware
	{
		private ListCollectionView docDistResults;
		public ListCollectionView DocDistResults
		{
			get { return docDistResults; }
			set { SetProperty(ref docDistResults, value); }
		}

		public DocDistResultViewModel()
		{
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
			DocDistResults = new ListCollectionView(resultsParameters.DocDistResults.ToList());
			DocDistResults.SortDescriptions.Add(new SortDescription("Percentage", ListSortDirection.Descending));
			DocDistResults.GroupDescriptions.Add(new PropertyGroupDescription("Percentage"));
		}
	}
}