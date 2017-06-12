using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;

namespace CmsUtils
{
    /// <summary>
    /// FileObj 的摘要说明
    /// </summary>
    public class IoUtil
    {
        #region 构造函数

        private bool _alreadyDispose = false;

        public IoUtil()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

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
        /// 写文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="content">文件内容</param>
        public static void WriteFile(string Path, string content)
        {

            if (!System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(Path)))
            {
                //Directory.CreateDirectory(Path);
                System.IO.FileStream f = System.IO.File.Create(System.Web.HttpContext.Current.Server.MapPath(Path));
                f.Close();
                f.Dispose();
            }
            System.IO.StreamWriter f2 = new System.IO.StreamWriter(System.Web.HttpContext.Current.Server.MapPath(Path), true, System.Text.Encoding.UTF8);
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
        /// 读文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <returns></returns>
        public static string ReadFile(string Path)
        {
            string s = "";
            if (!System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(Path)))
                s = "此文件不存在，或路径不存在";
            else
            {
                StreamReader f2 = new StreamReader(System.Web.HttpContext.Current.Server.MapPath(Path), System.Text.Encoding.Default);
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
        /// 追加文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="content">内容</param>
        public static void AppendFile(string Path, string content)
        {
            StreamWriter sw = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(Path));
            sw.Write(content);
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }

        #endregion

    }
}


