using System;
using System.IO;
using System.Text;

namespace CmsUtils
{
    /// <summary>
    ///     FileObj 的摘要说明
    /// </summary>
    public class IoUtil
    {
        #region IDisposable 成员

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region 写文件

        /****************************************
         * 函数名称：WriteFile
         * 功能说明：当文件不存时，则创建文件，并追加文件
         * 参    数：Path:文件路径,Strings:文本内容
         * 调用示列：
         *           string Path = Server.MapPath("Default2.aspx");      
         *           string Strings = "这是我写的内容啊";
         *           EC.FileObj.WriteFile(Path,Strings);
        *****************************************/

        /// <summary>
        ///     写文件
        /// </summary>
        /// <param name="outPath">文件路径</param>
        /// <param name="content">文件内容</param>
        public static void WriteFile(string outPath, string content)
        {
            if (File.Exists(outPath))
                File.Delete(outPath);
            var f = File.Create(outPath);
            f.Close();
            f.Dispose();
            var f2 = new StreamWriter(outPath, true, Encoding.UTF8);
            f2.WriteLine(content);
            f2.Close();
            f2.Dispose();
        }

        #endregion

        #region 读文件

        /****************************************
         * 函数名称：ReadFile
         * 功能说明：读取文本内容
         * 参    数：Path:文件路径
         * 调用示列：
         *           string Path = Server.MapPath("Default2.aspx");      
         *           string s = EC.FileObj.ReadFile(Path);
        *****************************************/

        /// <summary>
        ///     读文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static string ReadFile(string filePath)
        {
            var s = "";
            if (!File.Exists(filePath))
            {
                s = "此文件不存在，或路径不存在";
            }
            else
            {
                var f2 = new StreamReader(filePath, Encoding.UTF8);
                s = f2.ReadToEnd();
                f2.Close();
                f2.Dispose();
            }
            return s;
        }

        #endregion

        #region 追加文件

        /****************************************
         * 函数名称：FileAdd
         * 功能说明：追加文件内容
         * 参    数：Path:文件路径,strings:内容
         * 调用示列：
         *           string Path = Server.MapPath("Default2.aspx");    
         *           string Strings = "新追加内容";
         *           EC.FileObj.FileAdd(Path, Strings);
        *****************************************/

        /// <summary>
        ///     追加文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="content">内容</param>
        public static void AppendFile(string filePath, string content)
        {
            var sw = File.AppendText(filePath);
            sw.Write(content);
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }

        #endregion

        #region 构造函数

        private bool _alreadyDispose;

        ~IoUtil()
        {
            Dispose();
            ;
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (_alreadyDispose) return;
            //if (isDisposing)
            //{
            //    if (xml != null)
            //    {
            //        xml = null;
            //    }
            //}
            _alreadyDispose = true;
        }

        #endregion
    }
}