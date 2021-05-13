using Parakeet.Models.Inputs;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Parakeet.Models
{
	public class Project : BindableBase
	{
		private ObservableCollection<RenameRule> _renameRules;
		private ObservableCollection<RemoveRule> _removeRules;
		private ObservableCollection<PathModel> _pathModels;
		private ObservableCollection<SortByRule> _sortingRules;
		private DocDistModel _docDist;

		public ObservableCollection<RenameRule> RenameRules
		{
			get { return _renameRules; }
			set { SetProperty(ref _renameRules, value); }
		}

		public ObservableCollection<RemoveRule> RemoveRules
		{
			get { return _removeRules; }
			set { SetProperty(ref _removeRules, value); }
		}

		public ObservableCollection<PathModel> PathModels
		{
			get { return _pathModels; }
			set { SetProperty(ref _pathModels, value); }
		}

		public ObservableCollection<SortByRule> SortingRules
		{
			get { return _sortingRules; }
			set { SetProperty(ref _sortingRules, value); }
		}


		public DocDistModel DocDist
		{
			get { return _docDist; }
			set { SetProperty(ref _docDist, value); }
		}

		public Project()
		{
			DocDist = new DocDistModel();
			RenameRules = new ObservableCollection<RenameRule>();
			RemoveRules = new ObservableCollection<RemoveRule>();
			PathModels = new ObservableCollection<PathModel>();
			SortingRules = new ObservableCollection<SortByRule>();
		}
	}
}
