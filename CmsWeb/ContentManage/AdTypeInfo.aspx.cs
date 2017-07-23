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
            TB_AdType entity = new AdTypeBal().SelectSingleById(u => u.ID.Equals(Id));
            txtAdTypeName.Text = entity.AdTypeName.ToString();
            txtAdTypeDescription.Text = entity.AdTypeDescription.ToString();
            txtAdTypeComment.Text = entity.AdTypeComment.ToString();
            cbIsUsing.Checked = entity.IsUsing == Constants.IS_YES;

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
            TB_AdType entity;
            if (Id != 0)
            {
                entity = new AdTypeBal().SelectSingleById(u => u.ID.Equals(Id));
            }
            else
            {
                entity = new TB_AdType();
            }
            entity.AdTypeName = txtAdTypeName.Text;
            entity.AdTypeDescription = txtAdTypeDescription.Text;
            entity.AdTypeComment = txtAdTypeComment.Text;
            entity.IsUsing = !cbIsUsing.Checked ? Constants.IS_NO : Constants.IS_YES;

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
