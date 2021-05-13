using AutoMapper;
using Parakeet.Manager;
using Parakeet.Models;
using Parakeet.Properties;
using Parakeet.Services;
using Parakeet.ViewModels.ResultWindow;
using Parakeet.ViewModels.TaskWindow;
using Parakeet.Views;
using Parakeet.Views.PrimaryWindow;
using Parakeet.Views.ResultWindow;
using Parakeet.Views.TaskWindow;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace Parakeet
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : PrismApplication
	{
		protected override Window CreateShell()
		{
			var w = Container.Resolve<MainWindow>();
			return w;
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterSingleton<IProjectHelper, ProjectHelper>();


			containerRegistry.RegisterSingleton<IMapper>(() =>
			{
				var config = new MapperConfiguration(MappingProfile);
				return config.CreateMapper();
			});

			containerRegistry.RegisterForNavigation<RemovedView, RemovedViewModel>();
			containerRegistry.RegisterForNavigation<RenamedView, RenamedViewModel>();
			containerRegistry.RegisterForNavigation<SortedView, SortedViewModel>();
			containerRegistry.RegisterForNavigation<DocDistResultView, DocDistResultViewModel>();
			containerRegistry.RegisterForNavigation<DataView, DataViewModel>();

			containerRegistry.RegisterDialog<TaskWindow, TaskViewModel>("TaskDialog");
			containerRegistry.RegisterDialog<ResultsView, ResultsViewModel>("ResultsDialog");

			containerRegistry.Register<ManagerService>();
			containerRegistry.Register<ManagerLauncher>();
		}

		private void MappingProfile(IMapperConfigurationExpression obj)
		{ }

		protected override void Initialize()
		{
			base.Initialize();

			if (!string.IsNullOrEmpty(Settings.Default.CultureInfo))
			{
				Strings.Culture = new CultureInfo(Settings.Default.CultureInfo);
				Thread.CurrentThread.CurrentCulture = new CultureInfo(Settings.Default.CultureInfo);
				Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.CultureInfo);
			}
		}

		protected override void OnInitialized()
		{
			base.OnInitialized();

			var regionManager = Container.Resolve<IRegionManager>();
			regionManager.RegisterViewWithRegion("PathRegistering", typeof(DirectoryControlView));
			regionManager.RegisterViewWithRegion("RemoveFilesRegion", typeof(RemoveFilesView));
			regionManager.RegisterViewWithRegion("RenamingRegion", typeof(RenamingNamesView));
			regionManager.RegisterViewWithRegion("SortingRegion", typeof(SortByView));
			regionManager.RegisterViewWithRegion("DocDistRegion", typeof(DocDistView));


			try
			{
				if (!string.IsNullOrEmpty(Settings.Default.NameCurrentXmlFile))
				{
					var projHelper = Container.Resolve<IProjectHelper>();
					projHelper.Load(Settings.Default.NameCurrentXmlFile);
				}
			}
			catch
			{
				MessageBox.Show(Strings.AppReadingError, Strings.MainWindow_MainWindow_Error_File_Reading);
			}
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);

			Settings.Default.Save();
		}
	}
}
