using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace NCmdLiner
{
    public static class SerializerHelper<T>
    {
        /// <summary>  Serialize info object. </summary>
        ///
        /// <param name="fileName">      File name. </param>
        /// <param name="info">   Info object beeing serialized. </param>
        public static void Serialize(string fileName, T info)
        {
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (StreamWriter streamWriter = new StreamWriter(fs, Encoding.UTF8))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(streamWriter, info);
                }
            }
        }

        /// <summary>
        /// Deserialize info object
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T DeSerialize(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fs, Encoding.UTF8))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    return (T)xmlSerializer.Deserialize(streamReader);
                }
            }
        }

        /// <summary>
        /// Deserialize info object
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        internal static T DeSerialize(Stream stream)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            return (T)xmlSerializer.Deserialize(stream);
        }
    }
}
