using Manager;
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
		public ResultWindow(object result)
		{
			InitializeComponent();
			List<DocDistResultModel> resDocDist = result as List<DocDistResultModel>;
			if (resDocDist != null)
			{
				foreach (var res in resDocDist)
				{
					Console.WriteLine(res.Distance);
					Console.WriteLine(res.Percentage);
					Console.WriteLine(res.First);
					Console.WriteLine(res.Second);
				}
			}
		}
	}
}
