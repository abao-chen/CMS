﻿using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;

namespace CmsUtils
{
    public class FileDownHelper
    {
        private static string MapPathFile(string FileName)
        {
            return HttpContext.Current.Server.MapPath(FileName);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="destFileName">文件全路径，包含文件名</param>
        /// <param name="name">下载文件默认名</param>
        public static void DownLoadFile(string destFileName, string name = null)
        {
            if (File.Exists(destFileName))
            {
                var fi = new FileInfo(destFileName);
                string fileName;
                if (name == null)
                {
                    fileName = destFileName.Substring(destFileName.LastIndexOf("\\") + 1);
                }
                else
                {
                    fileName = name;
                }
                string userAgent = HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower();
                if (userAgent.IndexOf("firefox") == -1)//FF浏览器
                {
                    fileName = HttpUtility.UrlEncode(fileName, Encoding.UTF8);
                }
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
                HttpContext.Current.Response.AppendHeader("Content-Length", fi.Length.ToString());
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.WriteFile(destFileName);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 以文件流的方式下载文件
        /// </summary>
        /// <param name="fileName">文件全路径，包含文件名</param>
        public static void DownLoadFileStream(string fileName)
        {
            var filePath = MapPathFile(fileName);
            long chunkSize = 204800; //指定块大小 
            var buffer = new byte[chunkSize]; //建立一个200K的缓冲区 
            long dataToRead = 0; //已读的字节数   
            FileStream stream = null;
            try
            {
                //打开文件   
                stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                dataToRead = stream.Length;

                //添加Http头   
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition",
                    "attachement;filename=" + HttpUtility.UrlEncode(Path.GetFileName(filePath)));
                HttpContext.Current.Response.AddHeader("Content-Length", dataToRead.ToString());

                while (dataToRead > 0)
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        var length = stream.Read(buffer, 0, Convert.ToInt32(chunkSize));
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.Clear();
                        dataToRead -= length;
                    }
                    else
                    {
                        dataToRead = -1; //防止client失去连接 
                    }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Error:" + ex.Message);
            }
            finally
            {
                if (stream != null) stream.Close();
                HttpContext.Current.Response.Close();
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="_Request"></param>
        /// <param name="_Response"></param>
        /// <param name="_fileName"></param>
        /// <param name="_fullPath"></param>
        /// <param name="_speed"></param>
        /// <returns></returns>
        public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName,
            string _fullPath, long _speed)
        {
            try
            {
                var myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var br = new BinaryReader(myFile);
                try
                {
                    _Response.AddHeader("Accept-Ranges", "bytes");
                    _Response.Buffer = false;

                    var fileLength = myFile.Length;
                    long startBytes = 0;
                    var pack = 10240; //10K bytes
                    var sleep = (int)Math.Floor((double)(1000 * pack / _speed)) + 1;

                    if (_Request.Headers["Range"] != null)
                    {
                        _Response.StatusCode = 206;
                        var range = _Request.Headers["Range"].Split('=', '-');
                        startBytes = Convert.ToInt64(range[1]);
                    }
                    _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    if (startBytes != 0)
                        _Response.AddHeader("Content-Range",
                            string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));

                    _Response.AddHeader("Connection", "Keep-Alive");
                    _Response.ContentType = "application/octet-stream";
                    _Response.AddHeader("Content-Disposition",
                        "attachment;filename=" + HttpUtility.UrlEncode(_fileName, Encoding.UTF8));

                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    var maxCount = (int)Math.Floor((double)((fileLength - startBytes) / pack)) + 1;

                    for (var i = 0; i < maxCount; i++)
                        if (_Response.IsClientConnected)
                        {
                            _Response.BinaryWrite(br.ReadBytes(pack));
                            Thread.Sleep(sleep);
                        }
                        else
                        {
                            i = maxCount;
                        }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    br.Close();
                    myFile.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}