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
            TB_Authority entity = new AuthorityBal().SelectSingleById(u => u.ID.Equals(Id));
            ddlAuthorType.SelectedValue = entity.AuthorType.ToString();
            txtAuthorName.Text = entity.AuthorName;
            txtAuthorFlag.Text = entity.AuthorFlag;
            txtPageUrl.Text = entity.PageUrl;
            if (entity.ParentID != null)
            {
                hidParentID.Value = entity.ParentID.ToString();
                TB_Authority parentEntity = new AuthorityBal().SelectSingleById(u => u.ID == entity.ParentID);
                if (parentEntity != null)
                {
                    txtParent.Text = parentEntity.AuthorName;
                }
            }
            cbxIsMenu.Checked = entity.IsMenu == Constants.IS_YES;
        }

        /// <summary>
        /// 绑定下拉框数据源
        /// </summary>
        private void BindData()
        {
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            TB_Authority entity;
            if (Id != 0)
            {
                entity = new AuthorityBal().SelectSingleById(u => u.ID.Equals(Id));
                entity.AuthorType = Convert.ToInt32(ddlAuthorType.SelectedValue);
                entity.AuthorName = txtAuthorName.Text;
                entity.AuthorFlag = txtAuthorFlag.Text;
                entity.PageUrl = txtPageUrl.Text;
                if (hidParentID.Value != string.Empty && hidParentID.Value != "0")
                {
                    entity.ParentID = Convert.ToInt32(hidParentID.Value);
                    TB_Authority parentEntity = new AuthorityBal().SelectSingleById(u => u.ID == entity.ParentID);
                    entity.FullID = parentEntity.FullID;
                }
                else
                {
                    entity.ParentID = 0;
                }
                entity.IsMenu = cbxIsMenu.Checked ? Constants.IS_YES : Constants.IS_NO;
                new AuthorityBal().UpdateSingle(entity);
                if (entity.FullID.IsEmpty())
                {
                    entity.FullID = entity.ID.ToString();
                }
                else
                {
                    entity.FullID += "," + entity.ID;
                }

                new AuthorityBal().UpdateSingle(entity);
            }
            else
            {
                entity = new TB_Authority();
                entity.AuthorType = Convert.ToInt32(ddlAuthorType.SelectedValue);
                entity.AuthorName = txtAuthorName.Text;
                entity.AuthorFlag = txtAuthorFlag.Text;
                entity.PageUrl = txtPageUrl.Text;
                if (hidParentID.Value != string.Empty && hidParentID.Value != "0")
                {
                    entity.ParentID = Convert.ToInt32(hidParentID.Value);
                    TB_Authority parentEntity = new AuthorityBal().SelectSingleById(u => u.ID == entity.ParentID);
                    entity.FullID = parentEntity.FullID;
                }
                else
                {
                    entity.ParentID = 0;
                }
                entity.IsMenu = cbxIsMenu.Checked ? Constants.IS_YES : Constants.IS_NO;
                new AuthorityBal().InsertSingle(entity);
                if (entity.FullID.IsEmpty())
                {
                    entity.FullID = entity.ID.ToString();
                }
                else
                {
                    entity.FullID += "," + entity.ID;
                }

                new AuthorityBal().UpdateSingle(entity);
            }

            Response.Redirect("~/SysManage/AuthorityList.aspx");
        }

    }
}
