using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using CmsUtils;

namespace CmsCommon
{
    public class BasePage : System.Web.UI.Page, IRequiresSessionState
    {
        protected override void OnInit(EventArgs e)
        {
            if (SessionUtil.GetSession(Constants.SESSION_LOGIN_USERINFO) == null)
            {
                string url = string.IsNullOrEmpty(HttpContext.Current.Request.RawUrl) ? "~/login.aspx" : "~/login.aspx?" + Constants.REQUEST_PREURL + "=" + HttpUtility.UrlDecode(HttpContext.Current.Request.RawUrl);
                HttpContext.Current.Response.Redirect(url);
            }
        }
    }
}
