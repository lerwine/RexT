using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Erwine.Leonard.T.RexT.DataModel
{
    public class SerializableDataModel<TImplemented>
        where TImplemented : SerializableDataModel<TImplemented>, new()
    {
        public static TImplemented Load(Stream stream)
        {
            TImplemented result;

            XmlReaderSettings settings = new XmlReaderSettings { CloseInput = false };
            using (XmlReader reader = XmlReader.Create(stream, settings))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(TImplemented));
                result = serializer.ReadObject(reader) as TImplemented;
            }

            return result;
        }

        public void Save(Stream stream)
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                CloseOutput = false,
                Encoding = System.Text.Encoding.UTF8,
                Indent = true
            };

            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(TImplemented));
                serializer.WriteObject(writer, this);
                writer.Flush();
            }

            stream.Flush();
        }
    }
}
