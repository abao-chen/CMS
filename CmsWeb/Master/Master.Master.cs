﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsBAL;
using CmsCommon;
using CmsEntity;
using CmsUtils;

namespace Cms.Web.Admin.Master
{
    public partial class Master : System.Web.UI.MasterPage
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定菜单
                List<TB_Authority> authorList = new AuthorityBal().GetMenuList();
                if (authorList != null)
                {
                    StringBuilder menuHtml = new StringBuilder();
                    List<TB_Authority> moduleList = authorList.Where(m => m.AuthorType == Constants.AUTHOR_FLAG_MODULE).ToList();
                    foreach (TB_Authority module in moduleList)
                    {
                        menuHtml.AppendLine("<li>");
                        menuHtml.AppendLine("<a href=\"#\">" + module.AuthorName + "<span class=\"fa arrow\"></span></a>");
                        menuHtml.AppendLine("<ul class=\"nav nav-second-level\">");
                        List<TB_Authority> pageList = authorList.Where(m => m.AuthorType == Constants.AUTHOR_FLAG_PAGE && m.IsMenu == Constants.IS_YES && m.ParentID == module.ID).ToList();
                        foreach (TB_Authority page in pageList)
                        {
                            //menuHtml.AppendLine("<li mid=\"" + page.ID + "\" funurl=\"" + page.PageUrl + "\"><a tabindex=\"-1\" href=\"javascript:void(0);\">" + page.AuthorName + "</a></li>");
                            menuHtml.AppendLine("<li>");
                            menuHtml.AppendLine("<a href=\"" + page.PageUrl + "\">" + page.AuthorName + "</a>");
                            menuHtml.AppendLine("</li>");
                        }
                        menuHtml.AppendLine("</ul>");
                        menuHtml.AppendLine("</li>");
                        sideMenu.InnerHtml = menuHtml.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lkLogout_OnClick(object sender, EventArgs e)
        {
            SessionUtil.ClearAllSession();
            Response.Redirect("~/Login.aspx?Logout=" + Guid.NewGuid());
        }
    }
}