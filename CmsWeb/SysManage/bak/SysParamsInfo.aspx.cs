using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsBAL;
using CmsCommon;
using CmsEntity;

namespace CmsWeb.SysManage
{
    public partial class SysParamsInfo : BasePage
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
        protected TB_SysParams entity;

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
            entity = new SysParamsBal().SelectSingleById(s => s.ID.Equals(Id));
            if (entity != null)
            {
                txtParamValue.Text = entity.ParamValue;
                txtParamName.Text = entity.ParamName;
                txtParamDesc.Text = entity.ParamDesc;
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            int result;
            if (Id != 0)
            {
                entity = new SysParamsBal().SelectSingleById(s => s.ID.Equals(Id));
                entity.ParamName = txtParamName.Text.Trim();
                entity.ParamValue = txtParamValue.Text.Trim();
                entity.ParamDesc = txtParamDesc.Text.Trim();
                result = new SysParamsBal().UpdateSingle(entity);

            }
            else
            {
                entity = new TB_SysParams();
                entity.ParamName = txtParamName.Text.Trim();
                entity.ParamValue = txtParamValue.Text.Trim();
                entity.ParamDesc = txtParamDesc.Text.Trim();
                result = new SysParamsBal().InsertSingle(entity);
            }

            if (result > 0)
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(),"myScript", "<script>saveCallback(1);</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myScript", "<script>saveCallback(2);</script>");
            }

        }
    }
}