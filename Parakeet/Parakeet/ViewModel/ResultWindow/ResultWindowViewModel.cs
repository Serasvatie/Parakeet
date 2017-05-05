using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Parakeet.ViewModel.ResultWindow
{
	class ResultWindowViewModel
	{
		private ListCollectionView _list;

		public ResultWindowViewModel(List<DocDistResultModel> data)
		{
			_list = new ListCollectionView(data.Select(x => new ItemDocDistResultViewModel(x)).ToList());
			_list.GroupDescriptions.Add(new PropertyGroupDescription("Percentage"));
		}

		public ListCollectionView DocDistResult
		{
			get { return _list; }
		}
	}
}
