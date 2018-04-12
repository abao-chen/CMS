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
    public partial class ContentTypeInfo : BasePage
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
            TB_ContentType entity = new ContentTypeBal().SelectSingleById(u => u.ID.Equals(Id));
            if (entity.TypeName != null)
            {
                txtTypeName.Text = entity.TypeName.ToString();
            }
            if (entity.TypeAlias != null)
            {
                txtTypeAlias.Text = entity.TypeAlias.ToString();
            }
            if (entity.IsUse != null)
            {
                cbxIsUse.Checked = (entity.IsUse == Constants.IS_YES);
            }

        }

        /// <summary>
        /// 绑定下拉框数据源
        /// </summary>
        private void BindData()
        {
            //ControlUtil.BindListControl(this.ddlAdTypeID, new AdTypeBal().SelectList(a => a.IsDeleted != Constants.IS_NO && a.IsUsing == Constants.IS_YES).ToList(), "AdTypeName", "ID", true);
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            TB_ContentType entity;
            if (Id != 0)
            {
                entity = new ContentTypeBal().SelectSingleById(u => u.ID.Equals(Id));
            }
            else
            {
                entity = new TB_ContentType();
            }
            entity.TypeName = txtTypeName.Text.Trim();
            entity.TypeAlias = txtTypeAlias.Text.Trim();
            entity.IsUse = cbxIsUse.Checked ? Constants.IS_YES : Constants.IS_NO;

            if (Id != 0)
            {
                new ContentTypeBal().UpdateSingle(entity);
            }
            else
            {
                new ContentTypeBal().InsertSingle(entity);
            }

            Response.Redirect("~/ContentManage/ContentTypeList.aspx");
        }

    }
}
