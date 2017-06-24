using System.Xml;

namespace CmsUtils
{
    public class XmlUtil
    {
        /// <summary>
        ///     根据节点名称获取内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public static string GetContentByNode(string filePath, string nodeName)
        {
            var content = string.Empty;
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var nodeList = xmlDoc.GetElementsByTagName(nodeName);
            foreach (XmlNode xmlNode in nodeList)
            {
                content = xmlNode.InnerText;
                break;
            }
            return content;
        }
    }
}