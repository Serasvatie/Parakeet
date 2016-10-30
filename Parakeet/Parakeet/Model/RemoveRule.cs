using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Parakeet.Model
{
    public class RemoveRule : IXmlSerializable
    {
        private string _string;
        private bool _extension;
        private bool _activated;

        public RemoveRule(string __string, bool extension, bool activated)
        {
            _string = __string;
            _extension = extension;
            _activated = activated;
        }

        public RemoveRule()
        {
            _string = "";
            _extension = false;
            _activated = false;
        }

        public string Strings
        {
            get { return _string; }
            set { _string = value; }
        }

        public bool IsExtension
        {
            get
            {
                return _extension;
            }
            set { _extension = value; }
        }

        public bool IsActivated
        {
            get { return _activated; }
            set { _activated = value; }
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            XmlSerializer stringSerializer = new XmlSerializer(typeof(string));
            XmlSerializer boolSerializer = new XmlSerializer(typeof(bool));

            reader.ReadStartElement("Strings");
            Strings = (string)stringSerializer.Deserialize(reader);
            reader.ReadEndElement();

            reader.ReadStartElement("IsExtension");
            IsExtension = (bool)boolSerializer.Deserialize(reader);
            reader.ReadEndElement();

            reader.ReadStartElement("IsActivated");
            IsActivated = (bool)boolSerializer.Deserialize(reader);
            reader.ReadEndElement();

        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer stringSerializer = new XmlSerializer(typeof(string));
            XmlSerializer boolSerializer = new XmlSerializer(typeof(bool));

            writer.WriteStartElement("Strings");
            stringSerializer.Serialize(writer, Strings);
            writer.WriteEndElement();

            writer.WriteStartElement("IsExtension");
            boolSerializer.Serialize(writer, IsExtension);
            writer.WriteEndElement();

            writer.WriteStartElement("IsActivated");
            boolSerializer.Serialize(writer, IsActivated);
            writer.WriteEndElement();
        }
    }
}