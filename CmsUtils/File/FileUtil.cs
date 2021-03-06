﻿using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;

namespace CmsUtils
{
    public class FileUtil
    {
        #region 检测指定目录是否存在

        /// <summary>
        ///     检测指定目录是否存在
        /// </summary>
        /// <param name="directoryPath">目录的绝对路径</param>
        /// <returns></returns>
        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        #endregion

        #region 检测指定文件是否存在,如果存在返回true

        /// <summary>
        ///     检测指定文件是否存在,如果存在则返回true。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }

        #endregion

        #region 获取指定目录中的文件列表

        /// <summary>
        ///     获取指定目录中所有文件列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        public static string[] GetFileNames(string directoryPath)
        {
            //如果目录不存在，则抛出异常
            if (!IsExistDirectory(directoryPath))
                throw new FileNotFoundException();

            //获取文件列表
            return Directory.GetFiles(directoryPath);
        }

        #endregion

        #region 获取指定目录中所有子目录列表,若要搜索嵌套的子目录列表,请使用重载方法.

        /// <summary>
        ///     获取指定目录中所有子目录列表,若要搜索嵌套的子目录列表,请使用重载方法.
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        public static string[] GetDirectories(string directoryPath)
        {
            try
            {
                return Directory.GetDirectories(directoryPath);
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取指定目录及子目录中所有文件列表

        /// <summary>
        ///     获取指定目录及子目录中所有文件列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">
        ///     模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        ///     范例："Log*.xml"表示搜索所有以Log开头的Xml文件。
        /// </param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static string[] GetFileNames(string directoryPath, string searchPattern, bool isSearchChild)
        {
            //如果目录不存在，则抛出异常
            if (!IsExistDirectory(directoryPath))
                throw new FileNotFoundException();

            try
            {
                if (isSearchChild)
                    return Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);
                return Directory.GetFiles(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 检测指定目录是否为空

        /// <summary>
        ///     检测指定目录是否为空
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        public static bool IsEmptyDirectory(string directoryPath)
        {
            try
            {
                //判断是否存在文件
                var fileNames = GetFileNames(directoryPath);
                if (fileNames.Length > 0)
                    return false;

                //判断是否存在文件夹
                var directoryNames = GetDirectories(directoryPath);
                if (directoryNames.Length > 0)
                    return false;

                return true;
            }
            catch
            {
                //这里记录日志
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                return true;
            }
        }

        #endregion

        #region 创建一个目录

        /// <summary>
        ///     创建一个目录
        /// </summary>
        /// <param name="directoryPath">目录的绝对路径</param>
        public static void CreateDirectory(string directoryPath)
        {
            //如果目录不存在则创建该目录
            if (!IsExistDirectory(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        #endregion

        #region 删除目录

        /// <summary>
        ///     删除目录
        /// </summary>
        /// <param name="dir">要删除的目录路径和名称</param>
        public static void DeleteDir(string dir)
        {
            if (dir.Length == 0) return;
            if (Directory.Exists(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir))
                Directory.Delete(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir);
        }

        #endregion

        #region 删除文件

        /// <summary>
        ///     删除文件
        /// </summary>
        /// <param name="file">要删除的文件路径和名称</param>
        public static void DeleteFile(string file)
        {
            if (File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + file))
                File.Delete(HttpContext.Current.Request.PhysicalApplicationPath + file);
        }

        #endregion

        #region 移动文件(剪贴--粘贴)

        /// <summary>
        ///     移动文件(剪贴--粘贴)
        /// </summary>
        /// <param name="dir1">要移动的文件的路径及全名(包括后缀)</param>
        /// <param name="dir2">文件移动到新的位置,并指定新的文件名</param>
        public static void MoveFile(string dir1, string dir2)
        {
            dir1 = dir1.Replace("/", "\\");
            dir2 = dir2.Replace("/", "\\");
            if (File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir1))
                File.Move(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir1,
                    HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir2);
        }

        #endregion

        #region 复制文件

        /// <summary>
        ///     复制文件
        /// </summary>
        /// <param name="dir1">要复制的文件的路径已经全名(包括后缀)</param>
        /// <param name="dir2">目标位置,并指定新的文件名</param>
        public static void CopyFile(string dir1, string dir2)
        {
            dir1 = dir1.Replace("/", "\\");
            dir2 = dir2.Replace("/", "\\");
            if (File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir1))
                File.Copy(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir1,
                    HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir2, true);
        }

        #endregion

        #region 根据时间获取指定路径的 后缀名的 的所有文件

        /// <summary>
        ///     根据时间获取指定路径的 后缀名的 的所有文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="Extension">后缀名 比如.txt</param>
        /// <returns></returns>
        public static DataRow[] GetFilesByTime(string path, string Extension)
        {
            if (Directory.Exists(path))
            {
                var fielExts = string.Format("*{0}", Extension);
                var files = Directory.GetFiles(path, fielExts);
                if (files.Length > 0)
                {
                    var table = new DataTable();
                    table.Columns.Add(new DataColumn("filename", Type.GetType("System.String")));
                    table.Columns.Add(new DataColumn("createtime", Type.GetType("System.DateTime")));
                    for (var i = 0; i < files.Length; i++)
                    {
                        var row = table.NewRow();
                        var creationTime = File.GetCreationTime(files[i]);
                        var fileName = Path.GetFileName(files[i]);
                        row["filename"] = fileName;
                        row["createtime"] = creationTime;
                        table.Rows.Add(row);
                    }
                    return table.Select(string.Empty, "createtime desc");
                }
            }
            return new DataRow[0];
        }

        #endregion

        #region 复制文件夹

        /// <summary>
        ///     复制文件夹(递归)
        /// </summary>
        /// <param name="varFromDirectory">源文件夹路径</param>
        /// <param name="varToDirectory">目标文件夹路径</param>
        public static void CopyFolder(string varFromDirectory, string varToDirectory)
        {
            Directory.CreateDirectory(varToDirectory);

            if (!Directory.Exists(varFromDirectory)) return;

            var directories = Directory.GetDirectories(varFromDirectory);

            if (directories.Length > 0)
                foreach (var d in directories)
                    CopyFolder(d, varToDirectory + d.Substring(d.LastIndexOf("\\")));
            var files = Directory.GetFiles(varFromDirectory);
            if (files.Length > 0)
                foreach (var s in files)
                    File.Copy(s, varToDirectory + s.Substring(s.LastIndexOf("\\")), true);
        }

        #endregion

        #region 读文件

        /****************************************
         * 函数名称：ReadFile
         * 功能说明：读取文本内容
         * 参    数：Path:文件路径
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

        #region 检查文件,如果文件不存在则创建

        /// <summary>
        ///     检查文件,如果文件不存在则创建
        /// </summary>
        /// <param name="filePath">路径,包括文件名</param>
        public static void ExistsFile(string filePath)
        {
            //if(!File.Exists(FilePath))    
            //File.Create(FilePath);    
            //以上写法会报错,详细解释请看下文.........   
            if (!File.Exists(filePath))
            {
                var fs = File.Create(filePath);
                fs.Close();
            }
        }

        #endregion

        #region 删除指定文件夹下的所有文件

        /// <summary>
        ///     删除指定文件夹下的所有文件
        /// </summary>
        /// <param name="varFromDirectory">指定文件夹路径</param>
        public static void DeleteFolderFiles(string varFromDirectory, bool isDeleteSubDic)
        {
            DirectoryInfo di = new DirectoryInfo(varFromDirectory);
            if (!di.Exists)
            {
                return;
            }
            if (isDeleteSubDic)
            {//是否删除子目录
                DirectoryInfo[] subDic = di.GetDirectories();
                if (subDic.Length > 0)
                {
                    foreach (var d in subDic)
                    {
                        DeleteFolderFiles(d.FullName, true);
                    }
                }
            }

            //删除当前目录的所有文件
            var files = di.GetFiles();
            if (files.Length > 0)
            {
                foreach (var s in files)
                {
                    File.Delete(s.FullName);
                }
            }
        }

        #endregion

        #region 从文件的绝对路径中获取文件名( 包含扩展名 )

        /// <summary>
        ///     从文件的绝对路径中获取文件名( 包含扩展名 )
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static string GetFileName(string filePath)
        {
            //获取文件的名称
            var fi = new FileInfo(filePath);
            return fi.Name;
        }

        #endregion

        #region 复制文件参考方法,页面中引用

        /// <summary>
        ///     复制文件参考方法,页面中引用
        /// </summary>
        /// <param name="cDir">新路径</param>
        /// <param name="TempId">模板引擎替换编号</param>
        public static void CopyFiles(string cDir, string TempId)
        {
            //if (Directory.Exists(Request.PhysicalApplicationPath + "\\Controls"))
            //{
            //    string TempStr = string.Empty;
            //    StreamWriter sw;
            //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\Default.aspx"))
            //    {
            //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\Default.aspx");
            //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

            //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\Default.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
            //        sw.Write(TempStr);
            //        sw.Close();
            //    }
            //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\Column.aspx"))
            //    {
            //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\Column.aspx");
            //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

            //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\List.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
            //        sw.Write(TempStr);
            //        sw.Close();
            //    }
            //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\Content.aspx"))
            //    {
            //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\Content.aspx");
            //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

            //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\View.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
            //        sw.Write(TempStr);
            //        sw.Close();
            //    }
            //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\MoreDiss.aspx"))
            //    {
            //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\MoreDiss.aspx");
            //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

            //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\DissList.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
            //        sw.Write(TempStr);
            //        sw.Close();
            //    }
            //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\ShowDiss.aspx"))
            //    {
            //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\ShowDiss.aspx");
            //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

            //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\Diss.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
            //        sw.Write(TempStr);
            //        sw.Close();
            //    }
            //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\Review.aspx"))
            //    {
            //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\Review.aspx");
            //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

            //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\Review.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
            //        sw.Write(TempStr);
            //        sw.Close();
            //    }
            //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\Search.aspx"))
            //    {
            //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\Search.aspx");
            //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

            //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\Search.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
            //        sw.Write(TempStr);
            //        sw.Close();
            //    }
            //}
        }

        #endregion

        #region 获取文本文件的行数

        /// <summary>
        ///     获取文本文件的行数
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static int GetLineCount(string filePath)
        {
            //将文本文件的各行读到一个字符串数组中
            var rows = File.ReadAllLines(filePath);

            //返回行数
            return rows.Length;
        }

        #endregion

        #region 获取一个文件的长度

        /// <summary>
        ///     获取一个文件的长度,单位为Byte
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static long GetFileSize(string filePath)
        {
            //创建一个文件对象
            var fi = new FileInfo(filePath);

            //获取文件的大小
            return fi.Length;
        }

        #endregion

        #region 获取文件大小并以B，KB，GB，TB

        /// <summary>
        ///     计算文件大小函数(保留两位小数),Size为字节大小
        /// </summary>
        /// <param name="size">初始文件大小</param>
        /// <returns></returns>
        public static string ToFileSize(long size)
        {
            var m_strSize = "";
            long FactSize = 0;
            FactSize = size;
            if (FactSize < 1024.00)
                m_strSize = FactSize.ToString("F2") + " 字节";
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                m_strSize = (FactSize / 1024.00).ToString("F2") + " KB";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00).ToString("F2") + " MB";
            else if (FactSize >= 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00 / 1024.00).ToString("F2") + " GB";
            return m_strSize;
        }

        #endregion

        #region 获取指定目录中的子目录列表

        /// <summary>
        ///     获取指定目录及子目录中所有子目录列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">
        ///     模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        ///     范例："Log*.xml"表示搜索所有以Log开头的Xml文件。
        /// </param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static string[] GetDirectories(string directoryPath, string searchPattern, bool isSearchChild)
        {
            try
            {
                if (isSearchChild)
                    return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.AllDirectories);
                return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 向文本文件写入内容

        /// <summary>
        ///     写文件
        /// </summary>
        /// <param name="outPath">文件路径</param>
        /// <param name="content">文件内容</param>
        public static void WriteFile(string outPath, string content, Encoding encoding = null)
        {
            string dicPath = outPath.Replace(outPath.Substring(outPath.LastIndexOf("\\", StringComparison.Ordinal)), "");
            if (!Directory.Exists(dicPath))
            {
                Directory.CreateDirectory(dicPath);
            }
            if (File.Exists(outPath))
                File.Delete(outPath);
            var f = File.Create(outPath);
            f.Close();
            f.Dispose();
            var f2 = new StreamWriter(outPath, true, encoding ?? Encoding.UTF8);
            f2.WriteLine(content);
            f2.Close();
            f2.Dispose();
        }

        #endregion

        #region 向文本文件的尾部追加内容

        /// <summary>
        ///     向文本文件的尾部追加内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="content">写入的内容</param>
        public static void AppendText(string filePath, string content)
        {
            File.AppendAllText(filePath, content);
        }

        #endregion

        #region 将现有文件的内容复制到新文件中

        /// <summary>
        ///     将源文件的内容复制到目标文件中
        /// </summary>
        /// <param name="sourceFilePath">源文件的绝对路径</param>
        /// <param name="destFilePath">目标文件的绝对路径</param>
        public static void Copy(string sourceFilePath, string destFilePath)
        {
            File.Copy(sourceFilePath, destFilePath, true);
        }

        #endregion

        #region 将文件移动到指定目录

        /// <summary>
        ///     将文件移动到指定目录
        /// </summary>
        /// <param name="sourceFilePath">需要移动的源文件的绝对路径</param>
        /// <param name="descDirectoryPath">移动到的目录的绝对路径</param>
        public static void Move(string sourceFilePath, string descDirectoryPath)
        {
            //获取源文件的名称
            var sourceFileName = GetFileName(sourceFilePath);

            if (IsExistDirectory(descDirectoryPath))
            {
                //如果目标中存在同名文件,则删除
                if (IsExistFile(descDirectoryPath + "\\" + sourceFileName))
                    DeleteFile(descDirectoryPath + "\\" + sourceFileName);
                //将文件移动到指定目录
                File.Move(sourceFilePath, descDirectoryPath + "\\" + sourceFileName);
            }
        }

        #endregion

        #region 从文件的绝对路径中获取文件名( 不包含扩展名 )

        /// <summary>
        ///     从文件的绝对路径中获取文件名( 不包含扩展名 )
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static string GetFileNameNoExtension(string filePath)
        {
            //获取文件的名称
            var fi = new FileInfo(filePath);
            return fi.Name.Split('.')[0];
        }

        #endregion

        #region 从文件的绝对路径中获取扩展名

        /// <summary>
        ///     从文件路径中获取扩展名
        /// </summary>
        /// <param name="filePath">文件的路径</param>
        public static string GetExtension(string filePath)
        {
            string fileExt = string.Empty;
            //获取文件的名称
            if (!filePath.IsEmpty() && filePath.IndexOf(".", StringComparison.Ordinal) > 0)
            {
                fileExt = filePath.Substring(filePath.LastIndexOf(".", StringComparison.Ordinal));
            }

            return fileExt;
        }

        #endregion

        #region 清空文件内容

        /// <summary>
        ///     清空文件内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static void ClearFile(string filePath)
        {
            //删除文件
            File.Delete(filePath);

            //重新创建该文件
            CreateFile(filePath);
        }

        #endregion

        #region 检测指定目录中是否存在指定的文件

        /// <summary>
        ///     检测指定目录中是否存在指定的文件,若要搜索子目录请使用重载方法.
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">
        ///     模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        ///     范例："Log*.xml"表示搜索所有以Log开头的Xml文件。
        /// </param>
        public static bool Contains(string directoryPath, string searchPattern)
        {
            try
            {
                //获取指定的文件列表
                var fileNames = GetFileNames(directoryPath, searchPattern, false);

                //判断指定文件是否存在
                if (fileNames.Length == 0)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
            }
        }

        /// <summary>
        ///     检测指定目录中是否存在指定的文件
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">
        ///     模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        ///     范例："Log*.xml"表示搜索所有以Log开头的Xml文件。
        /// </param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static bool Contains(string directoryPath, string searchPattern, bool isSearchChild)
        {
            try
            {
                //获取指定的文件列表
                var fileNames = GetFileNames(directoryPath, searchPattern, true);

                //判断指定文件是否存在
                if (fileNames.Length == 0)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
            }
        }

        #endregion

        #region 创建一个文件

        /// <summary>
        ///     创建一个文件。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static void CreateFile(string filePath)
        {
            try
            {
                //如果文件不存在则创建该文件
                if (!IsExistFile(filePath))
                {
                    //创建一个FileInfo对象
                    var file = new FileInfo(filePath);

                    //创建文件
                    var fs = file.Create();

                    //关闭文件流
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                throw ex;
            }
        }

        /// <summary>
        ///     创建一个文件,并将字节流写入文件。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="buffer">二进制流数据</param>
        public static void CreateFile(string filePath, byte[] buffer)
        {
            try
            {
                //如果文件不存在则创建该文件
                if (!IsExistFile(filePath))
                {
                    //创建一个FileInfo对象
                    var file = new FileInfo(filePath);

                    //创建文件
                    var fs = file.Create();

                    //写入二进制流
                    fs.Write(buffer, 0, buffer.Length);

                    //关闭文件流
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                throw ex;
            }
        }

        #endregion
    }
}