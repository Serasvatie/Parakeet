using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Parakeet.Model
{
    // TO DO SERIALIZING

    public class Data
    {
        public static string DirectoryToSave = "\\Parakeet\\";

        public static string FullPathSaveDirectory =
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + DirectoryToSave;

        public string FileTitle;

        public SerializableList<ChangeRule> RenameRules;
        public SerializableList<RemoveRule> RemoveRules;
        public SerializableList<DirectoryModel> DirectoryModels;

        public Data(string fileTitle)
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
            FileTitle = fileTitle;
        }

        public Data(string fileTitle, SerializableList<ChangeRule> Changes, SerializableList<RemoveRule> Removes,
            SerializableList<DirectoryModel> Directory) : this(fileTitle)
        {
            RenameRules = Changes;
            RemoveRules = Removes;
            DirectoryModels = Directory;
        }

        public void WriteData()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter wr = XmlWriter.Create(FileTitle, settings))
            {
                DirectoryModels.WriteXml(wr);
                RenameRules.WriteXml(wr);
                RemoveRules.WriteXml(wr);
                wr.Close();
            }
        }
    }
}