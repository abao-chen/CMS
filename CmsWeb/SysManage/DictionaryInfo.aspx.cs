using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsBAL;
using CmsEntity;
using CmsCommon;
using CmsUtils;

namespace CmsWeb.SysManage
{
    public partial class DictionaryInfo : BasePage
    {
        private int Id
        {
            get
            {
                int iId;
                Int32.TryParse(Request.QueryString["ID"], out iId);
                return iId;
            }
        }
        protected TB_Dictionary entity;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            List<TB_DicType> list = new DicTypeBal().SelectList(d => d.IsDeleted == Constants.IS_DELETED_N);
            ControlUtil.BindDropDownList<TB_DicType>(this.ddlDicTypeCode, list, "DicTypeName", "DicTypeCode", false);
            entity = new DictionaryBal().SelectSingleById(s => s.ID.Equals(Id));
            if (entity != null)
            {
                txtDicCode.Text = entity.DicCode;
                txtDicName.Text = entity.DicName;
                ddlDicTypeCode.SelectedValue = entity.DicTypeCode;
                cbxIsUsing.Checked = (entity.IsUsing == Constants.IS_USING_Y);
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            int result;
            if (Id != 0)
            {
                entity = new DictionaryBal().SelectSingleById(s => s.ID.Equals(Id));
                entity.DicTypeCode = ddlDicTypeCode.SelectedValue;
                entity.DicCode = txtDicCode.Text;
                entity.DicName = txtDicName.Text;
                entity.IsUsing = cbxIsUsing.Checked ? 1 : 0;
                result = new DictionaryBal().UpdateSingle(entity);

            }
            else
            {
                entity = new TB_Dictionary();
                entity.DicTypeCode = ddlDicTypeCode.SelectedValue;
                entity.DicCode = txtDicCode.Text;
                entity.DicName = txtDicName.Text;
                entity.IsUsing = cbxIsUsing.Checked ? 1 : 0;
                result = new DictionaryBal().InsertSingle(entity);
            }

            if (result > 0)
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myScript", "<script>saveCallback(1);</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myScript", "<script>saveCallback(2);</script>");
            }
        }
    }
}
