//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2018/03/09
// 文件说明：基础内容Ajax请求页
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

namespace Cms.Web.Admin.API
{
    public partial class BasicContentApi : BaseApi
    {
        public override AjaxResultModel GetPagerList()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            string sql = @"SELECT
					            *
				            FROM
					            TB_BasicContent 
				            WHERE
					            isdeleted = 0 ";
            new BasicContentBal().GetPagerList(resultModel, searchModel, sql);
            return resultModel;
        }

        public override AjaxResultModel DeleteByIds()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            new BasicContentBal().DeleteByIds(resultModel, searchModel);
            return resultModel;
        }

        public override AjaxResultModel Download()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            string sql = @"SELECT
					            *
				            FROM
					            TB_BasicContent 
				            WHERE
					            isdeleted = 0 ";
            DataTable dt = new BasicContentBal().GetDataTable(searchModel, sql);
            string filePath = Server.MapPath("~/Temp/" + DateTime.Now.ToString("yyyyMMdd"));
            string fileName = " BasicContent_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            ExcelUtil.WriteExcel(dt, filePath, fileName);
            resultModel.data = "/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/" + fileName;
            return resultModel;
        }
    }
}
