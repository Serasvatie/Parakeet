using Manager;
using Parakeet.ViewModel.ResultWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Parakeet.View.ResultWindow
{
	/// <summary>
	/// Logique d'interaction pour ResultWindow.xaml
	/// </summary>
	public partial class ResultWindow : Window
	{
		private ResultWindowViewModel ResultViewModel;

		public ResultWindow(object result)
		{
			InitializeComponent();
			if (result is List<DocDistResultModel>)
				ResultViewModel = new ResultWindowViewModel(result as List<DocDistResultModel>);
			else
				Close();
			this.DocDist.DataContext = ResultViewModel;
		}
	}
}
