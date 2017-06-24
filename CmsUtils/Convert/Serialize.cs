using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CmsUtils
{
    public static class Serialize
    {
        /// <summary>
        ///     获取对象序列化的二进制版本
        /// </summary>
        /// <param name="pObj">对象实体</param>
        /// <returns>如果对象实体为Null，则返回结果为Null。</returns>
        public static byte[] GetBytes(object pObj)
        {
            if (pObj == null) return null;
            var serializationStream = new MemoryStream();
            new BinaryFormatter().Serialize(serializationStream, pObj);
            serializationStream.Position = 0L;
            var buffer = new byte[serializationStream.Length];
            serializationStream.Read(buffer, 0, buffer.Length);
            serializationStream.Close();
            return buffer;
        }

        /// <summary>
        ///     获取对象序列化的XmlDocument版本
        /// </summary>
        /// <param name="pObj">对象实体</param>
        /// <returns>如果对象实体为Null，则返回结果为Null。</returns>
        public static XmlDocument GetXmlDoc(object pObj)
        {
            if (pObj == null) return null;
            var serializer = new XmlSerializer(pObj.GetType());
            var sb = new StringBuilder();
            var writer = new StringWriter(sb);
            serializer.Serialize(writer, pObj);
            var document = new XmlDocument();
            document.LoadXml(sb.ToString());
            writer.Close();
            return document;
        }

        /// <summary>
        ///     从已序列化数据中(byte[])获取对象实体
        /// </summary>
        /// <typeparam name="T">要返回的数据类型</typeparam>
        /// <param name="binData">二进制数据</param>
        /// <returns>对象实体</returns>
        public static T GetObject<T>(byte[] binData)
        {
            if (binData == null) return default(T);
            var formatter = new BinaryFormatter();
            var serializationStream = new MemoryStream(binData);
            return (T) formatter.Deserialize(serializationStream);
        }

        /// <summary>
        ///     从已序列化数据(XmlDocument)中获取对象实体
        /// </summary>
        /// <typeparam name="T">要返回的数据类型</typeparam>
        /// <param name="xmlDoc">已序列化的文档对象</param>
        /// <returns>对象实体</returns>
        public static T GetObject<T>(XmlDocument xmlDoc)
        {
            if (xmlDoc == null) return default(T);
            var xmlReader = new XmlNodeReader(xmlDoc.DocumentElement);
            var serializer = new XmlSerializer(typeof(T));
            return (T) serializer.Deserialize(xmlReader);
        }
    }
}