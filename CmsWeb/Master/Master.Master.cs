using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsUtils;

namespace CmsWeb.Master
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lkLogout_OnClick(object sender, EventArgs e)
        {
            SessionUtil.ClearAllSession();
            Response.Redirect("~/Login.aspx");
        }
    }
}