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

namespace Cms.Web.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        Log log = LogFactory.GetLogger(HttpContext.Current.GetType());

        /// <summary>
        /// 默认调整URL
        /// </summary>
        private string DefaultUrl
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString[Constants.REQUEST_PREURL]) ? GlobalConfig.DefaultUrl : Request.QueryString["preUrl"];
            }
        }

        /// <summary>
        /// 是否注销
        /// </summary>
        private string IsLogout
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["Logout"]) ? string.Empty : Constants.IS_YES.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CookieUtil.GetCookie("LoginName").IsNotEmpty() && CookieUtil.GetCookie("Password").IsNotEmpty() && IsLogout.IsEmpty())
                {
                    log.Info("登录IP:" + Net.Ip);
                    TB_BasicUser userInfo = new TB_BasicUser()
                    {
                        UserAccount = CookieUtil.GetCookie("LoginName"),
                        UserPassword = CookieUtil.GetCookie("Password")
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
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Constants.IS_NO.ToString().Equals(GlobalConfig.IsValidateCode) ||
                (SessionUtil.GetSession(VerifyCodeUtil.ValidateCodeKey) != null
                && SecurityUtil.Md5Encrypt64(txtValidateCode.Text).Equals(SessionUtil.GetSession(VerifyCodeUtil.ValidateCodeKey).ToString())
                ))
            {//验证码校验通过
                log.Info("登录IP:" + Net.Ip);
                TB_BasicUser userInfo = new TB_BasicUser()
                {
                    UserAccount = txtAccount.Text.Trim(),
                    UserPassword = txtPassword.Text
                };
                if (cbxRemeber.Checked)
                {//记住密码
                    CookieUtil.SetCookie("LoginName", txtAccount.Text.Trim());
                    CookieUtil.SetCookie("Password", txtPassword.Text);
                }
                else
                {
                    CookieUtil.RemoveCookie("LoginName");
                    CookieUtil.RemoveCookie("Password");
                }
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