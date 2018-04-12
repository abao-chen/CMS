using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsEntity;
using CmsUtils;

namespace Cms.Web.Admin.API
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
                //存储在服务器的文件物理绝对路径 
                string saveFilePath = folderDic + "/" + serverFileName + fileExt;
                //文件相对路径(用于发送至前台下载)
                string downloadPath = folderPath + "/" + serverFileName + fileExt;
                httpPostedFile.SaveAs(saveFilePath);
                //文件地址
                result = string.Format(@"{0}|{1}", fileName, downloadPath);
            }
            return result;
        }

        public override AjaxResultModel Download()
        {
            throw new NotImplementedException();
        }

        public override AjaxResultModel GetPagerList()
        {
            throw new NotImplementedException();
        }

        public override AjaxResultModel DeleteByIds()
        {
            throw new NotImplementedException();
        }
    }
}