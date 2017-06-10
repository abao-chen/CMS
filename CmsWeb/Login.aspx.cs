using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsBAL;
using Entity;

namespace CmsWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            BasicContentBal baseBasicContentBal = new BasicContentBal();
            tb_basiccontent entity = new tb_basiccontent
            {
                ContentType = 1,
                ContentTitle = "testTile",
                Source = "无极网",
                ContentSubTitle = "子标题"
            };
            int count = baseBasicContentBal.Insert(entity);
        }
    }
}