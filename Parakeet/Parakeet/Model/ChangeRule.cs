using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Parakeet.ViewModel;

namespace Parakeet.Model
{
    public class ChangeRule : IXmlSerializable
    {
        private string _old;
        private string _new;
        private Target _target;
        private bool _activated;

        public ChangeRule()
        {
            _old = "";
            _new = "";
            _activated = false;
            _target = Target.All;
        }

        public ChangeRule(string old, string __new, bool isActivated, Target target)
        {
            _old = old;
            _new = __new;
            _activated = isActivated;
            _target = target;
        }

        public string Old
        {
            get { return _old; }
            set { _old = value; }
        }

        public string New
        {
            get { return _new; }
            set { _new = value; }
        }

        public Target Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public bool IsActivate
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
            XmlSerializer enumSerializer = new XmlSerializer(typeof(Target));

            reader.ReadStartElement("Old");
            Old = (string)stringSerializer.Deserialize(reader);
            reader.ReadEndElement();

            reader.ReadStartElement("New");
            New = (string)stringSerializer.Deserialize(reader);
            reader.ReadEndElement();

            reader.ReadStartElement("Target");
            Target = (Target)enumSerializer.Deserialize(reader);
            reader.ReadEndElement();

            reader.ReadStartElement("IsActivate");
            IsActivate = (bool)boolSerializer.Deserialize(reader);
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer stringSerializer = new XmlSerializer(typeof(string));
            XmlSerializer boolSerializer = new XmlSerializer(typeof(bool));
            XmlSerializer enumSerializer = new XmlSerializer(typeof(Target));

            writer.WriteStartElement("Old");
            stringSerializer.Serialize(writer, Old);
            writer.WriteEndElement();

            writer.WriteStartElement("New");
            stringSerializer.Serialize(writer, New);
            writer.WriteEndElement();

            writer.WriteStartElement("Target");
            enumSerializer.Serialize(writer, Target);
            writer.WriteEndElement();

            writer.WriteStartElement("IsActivate");
            boolSerializer.Serialize(writer, IsActivate);
            writer.WriteEndElement();
        }
    }
}