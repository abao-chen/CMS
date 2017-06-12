using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CmsUtils
{
    public class XmlUtil
    {
        /// <summary>
        /// 根据节点名称获取内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public static string GetContentByNode(string filePath, string nodeName)
        {
            string content = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName(nodeName);
            foreach (XmlNode xmlNode in nodeList)
            {
                content = xmlNode.InnerText;
                break;
            }
            return content;
        }
    }
}
