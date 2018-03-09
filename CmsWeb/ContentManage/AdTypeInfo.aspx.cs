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
    public partial class AdTypeInfo : BasePage
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
            TB_AdType  entity = new AdTypeBal().SelectSingleById(u => u.ID.Equals(Id));
                        if(entity.AdTypeName != null )
            {
                txtAdTypeName.Text = entity.AdTypeName.ToString();
            }
            if(entity.AdTypeDescription != null )
            {
                txtAdTypeDescription.Text = entity.AdTypeDescription.ToString();
            }
            if(entity.AdTypeComment != null )
            {
                txtAdTypeComment.Text = entity.AdTypeComment.ToString();
            }
            if(entity.IsUsing != null )
            {
                cbIsUsing.Checked = entity.IsUsing  == Constants.IS_YES;
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
            TB_AdType  entity;
            if (Id != 0)
            {
                entity = new AdTypeBal().SelectSingleById(u => u.ID.Equals(Id));
            }
            else
            {
                entity = new TB_AdType();
            }
                        entity.AdTypeName = txtAdTypeName.Text.Trim();
            entity.AdTypeDescription = txtAdTypeDescription.Text.Trim();
            entity.AdTypeComment = txtAdTypeComment.Text.Trim();
            entity.IsUsing = cbIsUsing.Checked ? Constants.IS_YES : Constants.IS_NO;

            if (Id != 0)
            {
                new AdTypeBal().UpdateSingle(entity);
            }
            else
            {
                new AdTypeBal().InsertSingle(entity);
            }

            Response.Redirect("~/ContentManage/AdTypeList.aspx");
        }
        
    }
}
