
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Parakeet.Model
{
    public class DirectoryModel
    {
        private string _path;
        private bool _activated;

        public DirectoryModel()
        {
            _path = "";
            _activated = true;
        }

        public DirectoryModel(string path, bool activated)
        {
            _path = path;
            _activated = activated;
        }

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public bool Activated
        {
            get { return _activated; }
            set { _activated = value; }
        }

        //public XmlSchema GetSchema()
        //{
        //    return null;
        //}

        //public void ReadXml(XmlReader reader)
        //{
        //    XmlSerializer stringSerializer = new XmlSerializer(typeof(string));
        //    XmlSerializer boolSerializer = new XmlSerializer(typeof(bool));

        //    reader.ReadStartElement("Path");
        //    Path = (string)stringSerializer.Deserialize(reader);
        //    reader.ReadEndElement();

        //    reader.ReadStartElement("Activated");
        //    Activated = (bool)boolSerializer.Deserialize(reader);
        //    reader.ReadEndElement();
        //}

        //public void WriteXml(XmlWriter writer)
        //{
        //    XmlSerializer stringSerializer = new XmlSerializer(typeof(string));
        //    XmlSerializer boolSerializer = new XmlSerializer(typeof(bool));

        //    writer.WriteStartElement("Path");
        //    stringSerializer.Serialize(writer, Path);
        //    writer.WriteEndElement();

        //    writer.WriteStartElement("Activated");
        //    boolSerializer.Serialize(writer, Activated);
        //    writer.WriteEndElement();
        //}
    }
}
