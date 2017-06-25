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
    public partial class AuthorityInfo : BasePage
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        private int Id
        {
            get
            {
                int id = 0;
                int.TryParse(Request.QueryString["Id"], out id);
                return id;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                if (Id != 0)
                {
                    InitData();
                }
            }

        }

        /// <summary>
        /// 初始页面数据
        /// </summary>
        private void InitData()
        {
            TB_Authority  entity = new AuthorityBal().SelectSingleById(u => u.ID.Equals(Id));
        }

        /// <summary>
        /// 绑定下拉框数据源
        /// </summary>
        private void BindData()
        {
            //ControlUtil.BindDropDownList(this.ddlStatus, new DictionaryBal().GetDictionaryList(Constants.DIC_TYPE_USERSTATUS), true);
            //ControlUtil.BindDropDownList(this.ddlType, new DictionaryBal().GetDictionaryList(Constants.DIC_TYPE_USERTYPE), true);
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            TB_Authority  entity;
            if (Id != 0)
            {
                entity = new AuthorityBal().SelectSingleById(u => u.ID.Equals(Id));
               
                new AuthorityBal().UpdateSingle(entity);
            }
            else
            {
                entity = new TB_Authority();
               
                new AuthorityBal().InsertSingle(entity);
            }

            Response.Redirect("~/SysConfig/AuthorityList.aspx");
        }
        
    }
}
