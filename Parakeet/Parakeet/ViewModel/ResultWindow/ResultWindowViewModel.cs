﻿using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parakeet.ViewModel.ResultWindow
{
	class ResultWindowViewModel
	{
		private List<ItemDocDistResultViewModel> _list;

		public ResultWindowViewModel(List<DocDistResultModel> data)
		{
			_list = data.Select(x => new ItemDocDistResultViewModel(x)).ToList();
		}

		public List<ItemDocDistResultViewModel> DocDistResult
		{
			get { return _list; }
		}
	}
}