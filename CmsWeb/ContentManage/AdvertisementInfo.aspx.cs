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
                txtAdTypeID.Text = entity.AdTypeID.ToString();
            }
            if(entity.AdName != null )
            {
                txtAdName.Text = entity.AdName.ToString();
            }
            uplAdDescription.Value = entity.AdImage;
            if(entity.AdUrl != null )
            {
                txtAdUrl.Text = entity.AdUrl.ToString();
            }
            if(entity.ValidStartTime != null )
            {
                txtValidStartTime.DateValue = entity.ValidStartTime;
            }
            if(entity.ValidEndTime != null )
            {
                txtValidEndTime.DateValue = entity.ValidEndTime;
            }
            if(entity.AdTypeComment != null )
            {
                txtAdTypeComment.Text = entity.AdTypeComment.ToString();
            }
            if(entity.IsUsing != null )
            {
                txtIsUsing.Text = entity.IsUsing.ToString();
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
            TB_Advertisement  entity;
            if (Id != 0)
            {
                entity = new AdvertisementBal().SelectSingleById(u => u.ID.Equals(Id));
            }
            else
            {
                entity = new TB_Advertisement();
            }
                        entity.AdTypeID = txtAdTypeID.Text.ToIntOrNull();
            entity.AdName = txtAdName.Text.Trim();
            entity.AdImage = uplAdDescription.Value;
            entity.AdUrl = txtAdUrl.Text.Trim();
            if(!txtValidStartTime.TextValue.IsEmpty())
            {
                entity.ValidStartTime = txtValidStartTime.DateValue;
            }

            if(!txtValidEndTime.TextValue.IsEmpty())
            {
                entity.ValidEndTime = txtValidEndTime.DateValue;
            }

            entity.AdTypeComment = txtAdTypeComment.Text.Trim();
            entity.IsUsing = txtIsUsing.Text.ToIntOrNull();

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
