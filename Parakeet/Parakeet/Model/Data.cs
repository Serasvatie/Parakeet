using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Xml;

namespace Parakeet.Model
{
    // TO DO SERIALIZING

    public sealed class Data
    {
        public static string DirectoryToSave = "\\Parakeet\\";

        public static string FullPathSaveDirectory =
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + DirectoryToSave;

        public SerializableList<ChangeRule> RenameRules;
        public SerializableList<RemoveRule> RemoveRules;
        public SerializableList<DirectoryModel> DirectoryModels;

        private string fileTitle;

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

        public
        string FileTitle
        {
            get
            {
                return fileTitle;
            }

            set
            {
                //if (!File.Exists(fileTitle))
                //{
                //    File.Create(fileTitle);
                //    SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                //    FileSecurity sec = new FileSecurity();
                //    sec.AddAccessRule(new FileSystemAccessRule(everyone,
                //        FileSystemRights.Modify | FileSystemRights.Synchronize,
                //        InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                //        PropagationFlags.None, AccessControlType.Allow));
                //    File.SetAccessControl(fileTitle, sec);
                //}
                fileTitle = value;
            }
        }


        public
        void WriteData()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent
            = true;
            settings.ConformanceLevel
            =
            ConformanceLevel.Fragment;
            using (
            XmlWriter wr = XmlWriter.Create(FileTitle, settings))
            {
                DirectoryModels.WriteXml(wr);
                RenameRules.WriteXml(wr);
                RemoveRules.WriteXml(wr);
                wr.Close();
            }
        }
    }

}