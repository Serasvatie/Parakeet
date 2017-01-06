using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Parakeet.Model
{
    public class SerializableList<TValue> : ObservableCollection<TValue>, IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            bool wasEmpty = reader.IsEmptyElement;
            if (wasEmpty)
                return;

            reader.ReadStartElement(typeof(TValue).ToString());
            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                TValue value = (TValue)valueSerializer.Deserialize(reader);

                this.Add(value);

                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializerNamespaces emptyNS = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            writer.WriteStartElement(typeof(TValue).ToString());

            foreach (TValue value in this)
                valueSerializer.Serialize(writer, value, emptyNS);
            writer.WriteFullEndElement();
        }
    }
}