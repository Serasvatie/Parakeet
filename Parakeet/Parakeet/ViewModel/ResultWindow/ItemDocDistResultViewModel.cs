using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager;
using System.IO;

namespace Parakeet.ViewModel.ResultWindow
{
	public class ItemDocDistResultViewModel
	{
		private DocDistResultModel _model;

		public ItemDocDistResultViewModel(DocDistResultModel result)
		{
			_model = result;
		}

		public string First
		{
			get { return _model.First; }
		}

		public string Second
		{
			get { return _model.Second; }
		}

		public double Distance
		{
			get { return _model.Distance; }
		}

		public double Percentage
		{
			get { return _model.Percentage; }
		}

		public string Title
		{
			get
			{
				return Path.GetFileNameWithoutExtension(_model.First);
			}
		}
	}
}
