using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class UserList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        /// <summary>
        /// 绑定下拉框数据源
        /// </summary>
        private void BindData()
        {
            ControlUtil.BindDropDownList(this.ddlUserStatus,
                new DictionaryBal().GetDictionaryList(Constants.DIC_TYPE_USERSTATUS), true);
            ControlUtil.BindDropDownList(this.ddlUserType,
                new DictionaryBal().GetDictionaryList(Constants.DIC_TYPE_USERTYPE), true);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_OnClick(object sender, EventArgs e)
        {
            AjaxModel searchModel = new AjaxModel();
            string sql = @"SELECT
					            u.*,
					            d1.DicName UserStatusName,
					            d2.DicName UserTypeName
				            FROM
					            tb_basicuser u
				            LEFT JOIN tb_dictionary d1 ON d1.isdeleted = 0
				            AND d1.DicTypeCode = '{0}'
				            AND d1.DicCode = u.UserStatus
				            LEFT JOIN tb_dictionary d2 ON d2.isdeleted = 0
				            AND d2.DicTypeCode = '{1}'
				            AND d2.DicCode = u.UserType
				            WHERE
					            u.isdeleted = 0 ";

            sql = string.Format(sql, Constants.DIC_TYPE_USERSTATUS, Constants.DIC_TYPE_USERTYPE);
            DataTable dt = new BasicUserBal().GetDataTable(searchModel, sql);
            string filePath = Server.MapPath("~/Temp/" + DateTime.Now.ToString("yyyyMMdd"));
            string fileName = "UserList_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            ExcelUtil.WriteExcel(dt, filePath, fileName,
                new string[] {"ID", "UserType", "UserStatus"});
            FileDownHelper.DownLoadFile(filePath + "/" + fileName, fileName);
        }
    }
}