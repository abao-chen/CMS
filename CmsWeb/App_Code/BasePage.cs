using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsCommon;
using CmsEntity;
using CmsUtils;

namespace Cms.Web.Admin
{
    public class BasePage : Page, IRequiresSessionState
    {
        protected Log log = LogFactory.GetLogger(HttpContext.Current.GetType());

        /// <summary>
        /// 登录用户信息
        /// </summary>
        protected TB_BasicUser LoginUserInfo
        {
            get
            {
                if (SessionUtil.GetSession(Constants.SESSION_LOGIN_USERINFO) != null)
                {
                    return (TB_BasicUser)SessionUtil.GetSession(Constants.SESSION_LOGIN_USERINFO);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 登录页面URL 
        /// </summary>
        private string LoginUrl = "~/login.aspx";

        protected override void OnInit(EventArgs e)
        {
            if (LoginUserInfo == null)
            {//判断用户是否登录
                string loginUrl = string.IsNullOrEmpty(HttpContext.Current.Request.RawUrl) ? LoginUrl : LoginUrl + "?" + Constants.REQUEST_PREURL + "=" + HttpUtility.UrlDecode(HttpContext.Current.Request.RawUrl);
                HttpContext.Current.Response.Redirect(loginUrl);
                HttpContext.Current.Response.End();
            }
            if (GlobalConfig.IsValidAuthor == Constants.IS_YES.ToString())
            {
                ValidateAuthor();
            }
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        private void ValidateAuthor()
        {
            if (ValidatePageAuthor())
            {
                ValidateControlsAuthor();
            }
            else
            {
                string preUrl = HttpContext.Current.Request.UrlReferrer == null
                    ? LoginUrl
                    : HttpContext.Current.Request.UrlReferrer.LocalPath;
                HttpContext.Current.Response.Redirect(preUrl);
                HttpContext.Current.Response.End();
            }

        }

        /// <summary>
        /// 页面权限验证
        /// </summary>
        /// <returns></returns>
        private bool ValidatePageAuthor()
        {
            bool result = false;
            string currentUrl = HttpContext.Current.Request.Url.LocalPath;
            if (LoginUserInfo.AuthorityList.Any(a => a.IsDeleted == Constants.IS_NO && a.AuthorType == 2 && a.PageUrl.Equals(currentUrl)))
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 控件权限验证
        /// </summary>
        /// <returns></returns>
        private void ValidateControlsAuthor()
        {
            string currentUrl = HttpContext.Current.Request.Url.LocalPath;
            var pageAuthor = LoginUserInfo.AuthorityList.FirstOrDefault(a => a.IsDeleted == Constants.IS_NO && a.AuthorType == 2 && a.PageUrl.Equals(currentUrl));
            if (pageAuthor != null)
            {
                FindControls(Page, pageAuthor.ID);
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 有效遍历页面所有控件的方法
        /// </summary>
        /// <param name="parent">父控件</param>
        /// <param name="pageId">页面Id</param>
        private void FindControls(Control parent, int pageId)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Button && !LoginUserInfo.AuthorityList.Any(a => a.ParentID == pageId && a.AuthorType == 3 && a.AuthorFlag.Equals(c.ID)))
                {//判断是否有按钮的操作权限
                    c.Visible = false;
                }
                if (c.HasControls())
                {//判断该控件是否有下属控件。
                    //递归，访问该控件的下属控件集。
                    FindControls(c, pageId);
                }
            }
        }
    }
}
