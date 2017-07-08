using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsUtils;

namespace CmsWeb.API
{
    public partial class UploadApi : BaseApi
    {
        public string UploadFile()
        {
            string result = string.Empty;
            var httpPostedFile = HttpContext.Current.Request.Files["FileData"];
            //上传图片相对路径
            string folderPath = HttpContext.Current.Request["FolderPath"] + DateTime.Now.ToString("yyyy-MM-dd");
            //上传图片绝对路径
            string folderDic = Server.MapPath(folderPath);
            //检测上传文件路径
            FileUtil.CreateDirectory(folderDic);

            if (httpPostedFile == null)
            {
                /*失败发送0*/
                result = "0";
            }
            else
            {
                //文件名称
                string fileName = httpPostedFile.FileName;
                string fileExt = FileUtil.GetExtension(fileName);
                string serverFileName = Guid.NewGuid().ToString();
                //文件路径（用于发至前台）
                string filePath = folderPath + "/" + serverFileName+ fileExt;
                //存储在服务器的文件物理路径 
                string normalPhotoFilePath = folderDic + "/" + serverFileName + fileExt;
                httpPostedFile.SaveAs(normalPhotoFilePath);
                //文件地址
                result = string.Format(@"{0}|{1}|{2}", filePath, fileName, serverFileName + fileExt);
            }
            return result;
        }
    }
}