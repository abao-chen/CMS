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
            txtTypeName.Text = entity.TypeName.ToString();
            txtTypeAlias.Text = entity.TypeAlias.ToString();
            cbxIsUse.Checked = entity.IsUse == Constants.IS_YES;

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
            TB_ContentType entity;
            if (Id != 0)
            {
                entity = new ContentTypeBal().SelectSingleById(u => u.ID.Equals(Id));
                entity.TypeName = txtTypeName.Text;
                entity.TypeAlias = txtTypeAlias.Text;
                entity.IsUse = cbxIsUse.Checked ? Constants.IS_YES : Constants.IS_NO;

                new ContentTypeBal().UpdateSingle(entity);
            }
            else
            {
                entity = new TB_ContentType();
                entity.TypeName = txtTypeName.Text;
                entity.TypeAlias = txtTypeAlias.Text;
                entity.IsUse = cbxIsUse.Checked ? Constants.IS_YES : Constants.IS_NO;

                new ContentTypeBal().InsertSingle(entity);
            }

            Response.Redirect("~/ContentManage/ContentTypeList.aspx");
        }

    }
}
