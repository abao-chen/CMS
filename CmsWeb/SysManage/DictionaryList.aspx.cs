﻿//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2018/03/09
// 文件说明：字典列表页面
// 
// 
//------------------------------------------------------------------------------

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

namespace Cms.Web.Admin
{
    public partial class DictionaryList : BasePage
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
            ControlUtil.BindListControl(this.ddlDicTypeCode, new DicTypeBal().SelectList(a => a.IsDeleted != Constants.IS_YES && a.IsUsing == Constants.IS_YES).ToList(), "DicTypeName", "DicTypeCode", true);
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
					            *
				            FROM
					            TB_Dictionary
				            WHERE
					            isdeleted = 0 ";
            DataTable dt = new DictionaryBal().GetDataTable(searchModel, sql);
            string filePath = Server.MapPath("~/Temp/" + DateTime.Now.ToString("yyyyMMdd"));
            string fileName = "DictionaryList_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            ExcelUtil.WriteExcel(dt, filePath, fileName);
            FileDownHelper.DownLoadFile(filePath + "/" + fileName, fileName);
        }
    }
}
