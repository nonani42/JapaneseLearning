using System;
using System.IO;
using System.Xml.Serialization;

namespace TestSpace
{
    public class SerializableXMLData<T> : IData<T>
    {
        private static XmlSerializer _serializer;

        public SerializableXMLData() => _serializer = new XmlSerializer(typeof(T));

        public void Save(T data, string path = null)
        {
            if (data == null && !String.IsNullOrEmpty(path))
                return;

            using (var fs = new FileStream(path, FileMode.Create))
            {
                _serializer.Serialize(fs, data);
            }
        }

        public T Load(string path)
        {
            T result;
            if (!File.Exists(path)) return default(T);

            using (var fs = new FileStream(path, FileMode.Open))
            {
                result = (T)_serializer.Deserialize(fs);
            }
            return result;
        }
    }
}
