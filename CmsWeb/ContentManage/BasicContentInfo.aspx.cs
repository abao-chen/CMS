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
            if (entity != null)
            {
                ddlContentType.SelectedValue = entity.ContentType != null ? entity.ContentType.ToString() : string.Empty;
                txtContentTitle.Text = entity.ContentTitle;
                txtContentSubTitle.Text = entity.ContentSubTitle;
                txtSource.Text = entity.Source;
                if (entity.ValidStartTime != null)
                {
                    txtValidStartTime.DateValue = entity.ValidStartTime;
                }
                if (entity.ValidEndTime != null)
                {
                    txtValidEndTime.DateValue = entity.ValidEndTime;
                }
                txtCoverPictureUrl.Text = entity.CoverPictureUrl;
                UploadExtAttachmentUrl.Value = entity.AttachmentUrl;
                txtOrderNO.Text = entity.OrderNO != null ? entity.OrderNO.ToString() : string.Empty;
                txtPageViewQua.Text = entity.PageViewQua != null ? entity.PageViewQua.ToString() : string.Empty;
                txtForwardQua.Text = entity.ForwardQua != null ? entity.ForwardQua.ToString() : string.Empty;
                txtPointQua.Text = entity.PointQua != null ? entity.PointQua.ToString() : string.Empty;
                txtCommentQua.Text = entity.CommentQua != null ? entity.CommentQua.ToString() : string.Empty;
                edtContent.Text = entity.Content;
            }
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
                entity.ContentType = int.Parse(ddlContentType.SelectedValue);
                entity.ContentTitle = txtContentTitle.Text;
                entity.ContentSubTitle = txtContentSubTitle.Text;
                entity.Source = txtSource.Text;
                if (!txtValidStartTime.TextValue.IsEmpty())
                {
                    entity.ValidStartTime = txtValidStartTime.DateValue;
                }
                else
                {
                    entity.ValidStartTime = null;
                }
                if (!txtValidEndTime.TextValue.IsEmpty())
                {
                    entity.ValidEndTime = txtValidEndTime.DateValue;
                }
                else
                {
                    entity.ValidEndTime = null;
                }
                entity.CoverPictureUrl = txtCoverPictureUrl.Text;
                entity.ContentSubTitle = txtContentSubTitle.Text;
                if (txtOrderNO.Text.IsNotEmpty())
                {
                    entity.OrderNO = int.Parse(txtOrderNO.Text);
                }
                if (txtPageViewQua.Text.IsNotEmpty())
                {
                    entity.PageViewQua = int.Parse(txtPageViewQua.Text);
                }
                if (txtForwardQua.Text.IsNotEmpty())
                {
                    entity.ForwardQua = int.Parse(txtForwardQua.Text);
                }
                if (txtPointQua.Text.IsNotEmpty())
                {
                    entity.PointQua = int.Parse(txtPointQua.Text);
                }
                if (txtCommentQua.Text.IsNotEmpty())
                {
                    entity.CommentQua = int.Parse(txtCommentQua.Text);
                }
                entity.AttachmentUrl = UploadExtAttachmentUrl.Value;
                entity.Content = edtContent.Text;
                
                new BasicContentBal().UpdateSingle(entity);
            }
            else
            {
                entity = new TB_BasicContent();
                entity.ContentType = int.Parse(ddlContentType.SelectedValue);
                entity.ContentTitle = txtContentTitle.Text;
                entity.ContentSubTitle = txtContentSubTitle.Text;
                entity.Source = txtSource.Text;
                if (!txtValidStartTime.TextValue.IsEmpty())
                {
                    entity.ValidStartTime = txtValidStartTime.DateValue;
                }
                else
                {
                    entity.ValidStartTime = null;
                }
                if (!txtValidEndTime.TextValue.IsEmpty())
                {
                    entity.ValidEndTime = txtValidEndTime.DateValue;
                }
                else
                {
                    entity.ValidEndTime = null;
                }
                entity.CoverPictureUrl = txtCoverPictureUrl.Text;
                entity.ContentSubTitle = txtContentSubTitle.Text;
                if (txtOrderNO.Text.IsNotEmpty())
                {
                    entity.OrderNO = int.Parse(txtOrderNO.Text);
                }
                if (txtPageViewQua.Text.IsNotEmpty())
                {
                    entity.PageViewQua = int.Parse(txtPageViewQua.Text);
                }
                if (txtForwardQua.Text.IsNotEmpty())
                {
                    entity.ForwardQua = int.Parse(txtForwardQua.Text);
                }
                if (txtPointQua.Text.IsNotEmpty())
                {
                    entity.PointQua = int.Parse(txtPointQua.Text);
                }
                if (txtCommentQua.Text.IsNotEmpty())
                {
                    entity.CommentQua = int.Parse(txtCommentQua.Text);
                }
                entity.AttachmentUrl = UploadExtAttachmentUrl.Value;
                entity.Content = edtContent.Text;

                new BasicContentBal().InsertSingle(entity);
            }

            Response.Redirect("~/ContentManage/BasicContentList.aspx");
        }

    }
}
