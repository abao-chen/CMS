using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsBAL;
using CmsCommon;
using CmsEntity;
using CmsUtils;

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
            if (Constants.IS_NO.ToString().Equals(GlobalConfig.IsValidateCode) ||
                (SessionUtil.GetSession(VerifyCodeUtil.ValidateCodeKey) != null && 
                SecurityUtil.Md5Encrypt64(txtValidateCode.Text).Equals(SessionUtil.GetSession(VerifyCodeUtil.ValidateCodeKey).ToString())))
            {//验证码校验通过
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
                else
                {
                    txtPassword.Text = string.Empty;
                    txtValidateCode.Text = string.Empty;
                    WebHelper.ClientToast(this, "用户名或密码错误，请重新输入！");
                }
            }
            else
            {
                txtValidateCode.Text = string.Empty;
                WebHelper.ClientToast(this, "验证码输入错误，请重新输入！");
            }
        }
    }
}