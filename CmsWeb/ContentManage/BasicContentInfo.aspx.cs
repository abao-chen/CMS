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
    public partial class BasicContentInfo : BasePage
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
            TB_BasicContent entity = new BasicContentBal().SelectSingleById(u => u.ID.Equals(Id));
            if (entity.ContentType != null)
            {
                ddlContentType.SelectedValue = entity.ContentType.ToString();
            }
            if (entity.Source != null)
            {
                txtSource.Text = entity.Source.ToString();
            }
            if (entity.ContentTitle != null)
            {
                txtContentTitle.Text = entity.ContentTitle.ToString();
            }
            if (entity.ContentSubTitle != null)
            {
                txtContentSubTitle.Text = entity.ContentSubTitle.ToString();
            }
            uplCoverPictureUrl.Value = entity.CoverPictureUrl;
            if (entity.ValidStartTime != null)
            {
                txtValidStartTime.DateValue = entity.ValidStartTime;
            }
            if (entity.ValidEndTime != null)
            {
                txtValidEndTime.DateValue = entity.ValidEndTime;
            }
            if (entity.OrderNO != null)
            {
                txtOrderNO.Text = entity.OrderNO.ToString();
            }
            txtPageViewQua.Text = entity.PageViewQua?.ToString();
            if (entity.ForwardQua != null)
            {
                txtForwardQua.Text = entity.ForwardQua.ToString();
            }
            if (entity.PointQua != null)
            {
                txtPointQua.Text = entity.PointQua.ToString();
            }
            if (entity.CommentQua != null)
            {
                txtCommentQua.Text = entity.CommentQua.ToString();
            }
            uplAttachmentUrl.Value = entity.AttachmentUrl;
            edtContent.Text = entity.Content;

        }

        /// <summary>
        /// 绑定下拉框数据源
        /// </summary>
        private void BindData()
        {
            ControlUtil.BindListControl(this.ddlContentType, new ContentTypeBal().SelectList(c => c.IsUse == Constants.IS_YES && c.IsDeleted == Constants.IS_NO), "TypeName", "ID", true);
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            TB_BasicContent entity;
            if (Id != 0)
            {
                entity = new BasicContentBal().SelectSingleById(u => u.ID.Equals(Id));
            }
            else
            {
                entity = new TB_BasicContent();
            }
            entity.ContentType = ddlContentType.SelectedValue?.ToInt();
            entity.Source = txtSource.Text.Trim();
            entity.ContentTitle = txtContentTitle.Text.Trim();
            entity.ContentSubTitle = txtContentSubTitle.Text.Trim();
            entity.CoverPictureUrl = uplCoverPictureUrl.Value;
            if (!txtValidStartTime.TextValue.IsEmpty())
            {
                entity.ValidStartTime = txtValidStartTime.DateValue;
            }

            if (!txtValidEndTime.TextValue.IsEmpty())
            {
                entity.ValidEndTime = txtValidEndTime.DateValue;
            }

            if (txtPageViewQua.Text.IsNotEmpty())
            {
                entity.OrderNO = txtOrderNO.Text.Trim().ToInt();
            }
            if (txtForwardQua.Text.IsNotEmpty())
            {
                entity.ForwardQua = txtForwardQua.Text.Trim().ToInt();
            }
            if (txtPointQua.Text.IsNotEmpty())
            {
                entity.PointQua = txtPointQua.Text.Trim().ToInt();
            }
            if (txtCommentQua.Text.IsNotEmpty())
            {
                entity.CommentQua = txtCommentQua.Text.Trim().ToInt();
            }
            entity.AttachmentUrl = uplAttachmentUrl.Value;
            entity.Content = edtContent.Text;

            if (Id != 0)
            {
                new BasicContentBal().UpdateSingle(entity);
            }
            else
            {
                new BasicContentBal().InsertSingle(entity);
            }

            Response.Redirect("~/ContentManage/BasicContentList.aspx");
        }

    }
}
