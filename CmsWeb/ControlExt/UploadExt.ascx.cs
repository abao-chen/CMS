using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsUtils;

namespace CmsWeb.ControlExt
{
    public partial class UploadExt : System.Web.UI.UserControl
    {
        public string Value
        {
            get { return this.hidFilePath.Value; }
            set { this.hidFilePath.Value = value; }
        }

        /// <summary>
        /// 上传类型：1：图片，2：文件
        /// </summary>
        public string UploadType = "1";

        /// <summary>
        /// 上传路径
        /// </summary>
        public string FolderPath = "/Upload/";

        /// <summary>
        /// 上传文件限制
        /// </summary>
        protected string Extensions
        {
            get
            {
                if (this.UploadType == "1")
                {
                    return Configs.GetValue("ImageExt"); 
                }
                else {
                    return Configs.GetValue("FileExt");
                }
            }
        }

        /// <summary>
        /// 1：单文件,0：多文件
        /// </summary>
        public string Single = "1";

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}