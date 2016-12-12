using System;
using System.ComponentModel;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows;
using System.Xml;

namespace Parakeet.Model
{
    // TO DO SERIALIZING

    public sealed class Data
    {
        public static string DirectoryToSave = "\\Parakeet\\";

        public static string FullPathSaveDirectory =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + DirectoryToSave;

        public SerializableList<ChangeRule> RenameRules;
        public SerializableList<RemoveRule> RemoveRules;
        public SerializableList<DirectoryModel> DirectoryModels;

        private string fileTitle;

        public FFManager.FFManager manager = new FFManager.FFManager();

        private static Data _instance;
        static readonly object instancelock = new object();

        private Data()
        {
            if (!Directory.Exists(FullPathSaveDirectory))
            {
                Directory.CreateDirectory(FullPathSaveDirectory);
                DirectorySecurity sec = Directory.GetAccessControl(FullPathSaveDirectory);
                SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                sec.AddAccessRule(new FileSystemAccessRule(everyone,
                    FileSystemRights.Modify | FileSystemRights.Synchronize,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                    PropagationFlags.None, AccessControlType.Allow));
                Directory.SetAccessControl(FullPathSaveDirectory, sec);
            }

            RenameRules = new SerializableList<ChangeRule>();
            DirectoryModels = new SerializableList<DirectoryModel>();
            RemoveRules = new SerializableList<RemoveRule>();
            manager.bwTask.RunWorkerCompleted += TaskCompleted;
        }

        private void TaskCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var res = e.Result as int?;
            if (e.Cancelled)
                MessageBox.Show(App.Current.MainWindow, "Action annulé par l'utiisateur !", "Résultat");
            else
                MessageBox.Show(App.Current.MainWindow, "Action éxecuté sur " + res + " éléments !", "Résultat");
        }

        public static Data getInstance()
        {
            if (_instance == null)
            {
                lock (instancelock)
                    if (_instance == null)
                        _instance = new Data();
            }
            return _instance;
        }

        public string FileTitle
        {
            get { return fileTitle; }
            set
            {
                fileTitle = value;
            }
        }

        public void ReadData()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            using (XmlReader xr = XmlReader.Create(FileTitle, settings))
            {
                DirectoryModels.ReadXml(xr);
                RenameRules.ReadXml(xr);
                RemoveRules.ReadXml(xr);
                xr.Close();
            }
        }

        public void WriteData()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            try
            {
                using (XmlWriter wr = XmlWriter.Create(FileTitle, settings))
                {
                    DirectoryModels.WriteXml(wr);
                    RenameRules.WriteXml(wr);
                    RemoveRules.WriteXml(wr);
                    wr.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur durant l'écriture du fichier.\n" + e.Message);
            }
        }
    }
}