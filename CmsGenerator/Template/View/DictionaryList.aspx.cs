//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/24
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

namespace CmsWeb
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
            //ControlUtil.BindDropDownList(this.#ControlAlisa##ColName#, new DictionaryBal().GetDictionaryList(#DicType#), true);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_OnClick(object sender, EventArgs e)
        {

            SearchModel searchModel = new SearchModel();
            string sql = @"SELECT
					            *
				            FROM
					            TB_Dictionary
				            WHERE
					            isdeleted = 0 ";
            DataTable dt = new DictionaryBal().GetDataTable(searchModel, sql);
            string filePath = Server.MapPath("~/Temp/" + DateTime.Now.ToString("yyyyMMdd"));
            string fileName = "DictionaryList_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            long fileLength = ExcelUtil.WriteExcel(dt, filePath, fileName);
            if (fileLength > 0)
            {
                Response.WriteFile(filePath + "/" + fileName);
                Response.ContentType = "application/x-xls";
                Response.Charset = "UTF-8";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.End();
            }
        }
    }
}
