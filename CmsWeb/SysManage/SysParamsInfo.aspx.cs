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
    public partial class SysParamsInfo : BasePage
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
            TB_SysParams  entity = new SysParamsBal().SelectSingleById(u => u.ID.Equals(Id));
                        if(entity.ParamName != null )
            {
                txtParamName.Text = entity.ParamName.ToString();
            }
            if(entity.ParamValue != null )
            {
                txtParamValue.Text = entity.ParamValue.ToString();
            }
            if(entity.ParamDesc != null )
            {
                txtParamDesc.Text = entity.ParamDesc.ToString();
            }

        }

        /// <summary>
        /// 绑定下拉框数据源
        /// </summary>
        private void BindData()
        {
            //ControlUtil.BindListControl(this.ddlAdTypeID, new AdTypeBal().SelectList(a => a.IsDeleted != Constants.IS_YES && a.IsUsing == Constants.IS_YES).ToList(), "AdTypeName", "ID", true);
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            TB_SysParams  entity;
            if (Id != 0)
            {
                entity = new SysParamsBal().SelectSingleById(u => u.ID.Equals(Id));
            }
            else
            {
                entity = new TB_SysParams();
            }
                        entity.ParamName = txtParamName.Text.Trim();
            entity.ParamValue = txtParamValue.Text.Trim();
            entity.ParamDesc = txtParamDesc.Text.Trim();

            if (Id != 0)
            {
                new SysParamsBal().UpdateSingle(entity);
            }
            else
            {
                new SysParamsBal().InsertSingle(entity);
            }

            Response.Redirect("~/SysManage/SysParamsList.aspx");
        }
        
    }
}
