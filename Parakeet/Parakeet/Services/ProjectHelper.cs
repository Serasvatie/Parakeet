using Parakeet.Models;
using Parakeet.Properties;
using Prism.Events;
using System;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace Parakeet.Services
{
	public class ProjectHelper : IProjectHelper
	{
		private static string DirectoryToSave = "\\Parakeet2\\";

		public static string InitialSavingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + DirectoryToSave;

		private readonly IEventAggregator ea;

		private string _fileName;
		public string FileName
		{
			get => _fileName;
			private set
			{
				_fileName = value;
				Settings.Default.NameCurrentXmlFile = _fileName;
			}
		}
		public Project Project { get; private set; }

		public ProjectHelper(IEventAggregator ea)
		{
			Project = new Project();
			this.ea = ea;
		}

		public void New(string filename)
		{
			FileName = filename;
			Project = new Project();
			ea.GetEvent<ProjectChangedEvent>().Publish();
		}

		public void Load(string fileName)
		{
			FileName = fileName;
			ReadData();
			ea.GetEvent<ProjectChangedEvent>().Publish();
		}

		public void Save()
		{
			WriteData();
		}

		public void Save(string fileTitle)
		{
			FileName = fileTitle;
			WriteData();
		}

		private void ReadData()
		{
			XmlReaderSettings settings = new XmlReaderSettings();
			XmlSerializer serializer = new XmlSerializer(typeof(Project));

			using XmlReader xr = XmlReader.Create(FileName, settings);

			Project = serializer.Deserialize(xr) as Project;
			xr.Close();
		}

		private void WriteData()
		{
			XmlWriterSettings settings = new();
			XmlSerializer serializer = new(typeof(Project));
			settings.Indent = true;
			try
			{
				using XmlWriter wr = XmlWriter.Create(FileName, settings);

				serializer.Serialize(wr, Project);
				wr.Close();
			}
			catch (Exception e)
			{
				MessageBox.Show(Strings.Data_WriteData_Error + e.Message);
			}
		}
	}
}
