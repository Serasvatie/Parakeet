using Parakeet.Models;
using Parakeet.Properties;
using Parakeet.Services;
using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Input;

namespace Parakeet.ViewModels.PrimaryWindow
{
	public class MenuViewModel : BindableBase
	{
		private bool _en;
		private bool _fr;
		private readonly IProjectHelper projectHelper;

		public ICommand NewFiles { get; private set; }
		public ICommand OpenFiles { get; private set; }
		public ICommand SaveFiles { get; private set; }
		public ICommand SaveFilesUnder { get; private set; }
		public ICommand Exit { get; private set; }
		public ICommand English { get; private set; }
		public ICommand French { get; private set; }

		public MenuViewModel(IProjectHelper projectHelper)
		{
			switch (Settings.Default.CultureInfo)
			{
				case "en-us":
					EnCheck = true;
					break;
				case "fr-fr":
					FrCheck = true;
					break;
			}

			NewFiles = new DelegateCommand(DoNewFiles);
			OpenFiles = new DelegateCommand(DoOpenFiles);
			SaveFiles = new DelegateCommand(DoSaveFiles);
			SaveFilesUnder = new DelegateCommand(DoSaveFilesUnder);
			Exit = new DelegateCommand(DoExit);
			English = new DelegateCommand(DoEnglish);
			French = new DelegateCommand(DoFrench);
			this.projectHelper = projectHelper;
		}

		private void DoNewFiles()
		{
			FileDialog _new = new SaveFileDialog();
			_new.AddExtension = true;
			_new.CheckPathExists = true;
			_new.DefaultExt = ".xml";
			_new.Filter = Strings.MenuViewModel_DoOpenFiles_Xml_files____xml____xml;
			_new.Title = Strings.MenuViewModel_DoNewFiles_Select_file_name___;
			_new.InitialDirectory = ProjectHelper.InitialSavingDirectory;
			_new.FileOk += NewFile;
			_new.ShowDialog();
		}

		private void NewFile(object sender, CancelEventArgs e)
		{
			FileDialog _new = (FileDialog)sender;
			projectHelper.New(_new.FileName);
		}

		private void DoOpenFiles()
		{
			OpenFileDialog open = new OpenFileDialog();
			open.Filter = Strings.MenuViewModel_DoOpenFiles_Xml_files____xml____xml;
			open.InitialDirectory = ProjectHelper.InitialSavingDirectory;
			open.Title = Strings.MenuViewModel_DoOpenFiles_Select_a_xml_file;
			open.FileOk += GettingFile;
			open.ShowDialog();
		}

		private void GettingFile(object sender, CancelEventArgs e)
		{
			FileDialog tmp = (FileDialog)sender;
			projectHelper.Load(tmp.FileName);
		}

		private void DoSaveFiles()
		{
			projectHelper.Save();
		}

		private void DoSaveFilesUnder()
		{
			SaveFileDialog save = new SaveFileDialog();
			save.AddExtension = true;
			save.CheckPathExists = true;
			save.DefaultExt = ".xml";
			save.Filter = Strings.MenuViewModel_DoSaveFilesUnder_Xml_files___xml______xml;
			save.Title = Strings.MenuViewModel_DoSaveFilesUnder_Sous____;
			save.FileOk += save_fileOk;
			save.InitialDirectory = ProjectHelper.InitialSavingDirectory;
			save.ShowDialog();
		}

		private void save_fileOk(object sender, CancelEventArgs e)
		{
			string fileTitle = ((FileDialog)sender).FileName;
			projectHelper.Save(fileTitle);
		}

		private void DoExit()
		{
			System.Windows.Application.Current.Shutdown();
		}

		private void DoEnglish()
		{
			EnCheck = true;
			FrCheck = false;
			CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-us");
			Settings.Default.CultureInfo = "en-us";
			System.Windows.MessageBox.Show(System.Windows.Application.Current.MainWindow, "Please restart Parakeet to make the modification take effect.", "English");
		}

		public bool EnCheck
		{
			get { return _en; }
			set { SetProperty(ref _en, value); }
		}

		private void DoFrench()
		{
			EnCheck = false;
			FrCheck = true;
			CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("fr-fr");
			Settings.Default.CultureInfo = "fr-fr";
			System.Windows.MessageBox.Show(System.Windows.Application.Current.MainWindow, "Redémarrer Parakeet pour que la modification prenne effet.", "Français");
		}

		public bool FrCheck
		{
			get { return _fr; }
			set { SetProperty(ref _fr, value); }
		}
	}
}