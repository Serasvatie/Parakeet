using Prism.Regions;
using System.Windows.Controls;

namespace Parakeet.Views.ResultWindow
{
	/// <summary>
	/// Interaction logic for ResultsView
	/// </summary>
	public partial class ResultsView : UserControl
	{
		private readonly IRegionManager regionManager;

		public ResultsView(IRegionManager regionManager)
		{
			InitializeComponent();

			Loaded += ResultsView_Loaded;
			Unloaded += ResultsView_Unloaded;
			this.regionManager = regionManager;
		}

		private void ResultsView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
		{
#warning Without this following shit codes, the regions doesn't work, not registering in this view.
			if (regionManager.Regions.ContainsRegionWithName("ResultsRegion"))
				regionManager.Regions.Remove("ResultsRegion");
		}

		private void ResultsView_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
#warning Without this following shit codes, the regions doesn't work, not registering in this view.
			if (!regionManager.Regions.ContainsRegionWithName("ResultsRegion"))
				RegionManager.SetRegionManager(ResultsRegion, regionManager);
		}
	}
}
