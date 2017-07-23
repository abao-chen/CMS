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
    public partial class AdvertisementInfo : BasePage
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
            TB_Advertisement  entity = new AdvertisementBal().SelectSingleById(u => u.ID.Equals(Id));
            if(entity.AdTypeID != null )
{
ddlAdTypeID.SelectedValue = entity.AdTypeID.ToString();
}
if(entity.AdName != null )
{
txtAdName.Text = entity.AdName.ToString();
}
if(entity.AdDescription != null )
{
txtAdDescription.Text = entity.AdDescription.ToString();
}
if(entity.AdUrl != null )
{
txtAdUrl.Text = entity.AdUrl.ToString();
}
if(entity.ValidStartTime != null )
{
    txtValidStartTime.Text = entity.ValidStartTime.ToString();
}
if(entity.ValidEndTime != null )
{
    txtValidEndTime.Text = entity.ValidEndTime.ToString();
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
            //ControlUtil.BindDropDownList(this.ddlStatus, new DictionaryBal().GetDictionaryList(Constants.DIC_TYPE_USERSTATUS), true);
            //ControlUtil.BindDropDownList(this.ddlType, new DictionaryBal().GetDictionaryList(Constants.DIC_TYPE_USERTYPE), true);
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            TB_Advertisement  entity;
            if (Id != 0)
            {
                entity = new AdvertisementBal().SelectSingleById(u => u.ID.Equals(Id));
            }
            else
            {
                entity = new TB_Advertisement();
            }
            entity.AdTypeID = ddlAdTypeID.SelectedValue;
entity.AdName = txtAdName.Text.Trim();
entity.AdDescription = txtAdDescription.Text.Trim();
entity.AdUrl = txtAdUrl.Text.Trim();
if(txtValidStartTime.Text != null )
{
entity.ValidStartTime = Convert.ToDateTime(txtValidStartTime.Text);
}

if(txtValidEndTime.Text != null )
{
entity.ValidEndTime = Convert.ToDateTime(txtValidEndTime.Text);
}

entity.AdTypeComment = txtAdTypeComment.Text.Trim();
entity.IsUsing = cbIsUsing.Checked ? Constants.IS_YES : Constants.IS_NO;

            if (Id != 0)
            {
                new AdvertisementBal().UpdateSingle(entity);
            }
            else
            {
                new AdvertisementBal().InsertSingle(entity);
            }

            Response.Redirect("~/ContentManage/AdvertisementList.aspx");
        }
        
    }
}
