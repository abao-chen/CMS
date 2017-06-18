using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsBAL;
using CmsCommon;
using CmsEntity;

namespace CmsWeb
{
    public partial class Login : System.Web.UI.Page
    {
        private string DefaultUrl
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString[Constants.REQUEST_PREURL]) ? GlobalConfig.DefaultUrl : Request.QueryString["preUrl"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            TB_BasicUser userInfo = new TB_BasicUser()
            {
                UserAccount = txtAccount.Text.Trim(),
                UserPassword = txtPassword.Text
            };
            bool result = new BasicUserBal().ValidateAccount(userInfo);
            if (result)
            {
                //记录最后一次登录时间
                userInfo.LastLoginTime = DateTime.Now;
                new BasicUserBal().UpdateSingle(userInfo);

                Response.Redirect(DefaultUrl);
            }
        }
    }
}