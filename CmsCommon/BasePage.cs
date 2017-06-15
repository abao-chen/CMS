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
        public BasePage()
        {

        }


        protected override void OnInit(EventArgs e)
        {
            if (SessionUtil.GetSession(Constants.SESSION_LOGIN_USERINFO) == null)
            {
                HttpContext.Current.Response.Redirect("~/login.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
