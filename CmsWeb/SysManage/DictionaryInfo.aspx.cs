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
    public partial class DictionaryInfo : BasePage
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
            TB_Dictionary entity = new DictionaryBal().SelectSingleById(u => u.ID.Equals(Id));
            if (entity.DicTypeCode != null)
            {
                ddlDicTypeCode.SelectedValue = entity.DicTypeCode.ToString();
            }
            if (entity.DicName != null)
            {
                txtDicName.Text = entity.DicName.ToString();
            }
            if (entity.DicCode != null)
            {
                txtDicCode.Text = entity.DicCode.ToString();
            }
            if (entity.IsUsing != null)
            {
                cbIsUsing.Checked = entity.IsUsing == Constants.IS_YES;
            }

        }

        /// <summary>
        /// 绑定下拉框数据源
        /// </summary>
        private void BindData()
        {
            ControlUtil.BindListControl(this.ddlDicTypeCode, new DicTypeBal().SelectList(a => a.IsDeleted != Constants.IS_YES && a.IsUsing == Constants.IS_YES).ToList(), "DicTypeName", "DicTypeCode", true);
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            TB_Dictionary entity;
            if (Id != 0)
            {
                entity = new DictionaryBal().SelectSingleById(u => u.ID.Equals(Id));
            }
            else
            {
                entity = new TB_Dictionary();
            }
            entity.DicTypeCode = ddlDicTypeCode.SelectedValue;
            entity.DicName = txtDicName.Text.Trim();
            entity.DicCode = txtDicCode.Text.Trim();
            entity.IsUsing = cbIsUsing.Checked ? Constants.IS_YES : Constants.IS_NO;

            if (Id != 0)
            {
                new DictionaryBal().UpdateSingle(entity);
            }
            else
            {
                new DictionaryBal().InsertSingle(entity);
            }

            Response.Redirect("~/SysManage/DictionaryList.aspx");
        }

    }
}
