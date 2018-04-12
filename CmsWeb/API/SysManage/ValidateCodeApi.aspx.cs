using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsUtils;

namespace Cms.Web.Admin.API
{
    public partial class ValidateCodeApi : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string validateCode = SecurityUtil.RandomCode(3, 4);
            byte[] ms = VerifyCodeUtil.GetVerifyCode(validateCode);
            Response.ClearContent();
            Response.ContentType = "image/Gif";
            Response.BinaryWrite(ms);
        }
    }
}