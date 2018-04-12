//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2018/03/08
// 文件说明：广告类型Ajax请求页
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
    public partial class AdTypeApi : BaseApi
    {
        public override AjaxResultModel GetPagerList()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            string sql = @"SELECT
					          ID,AdTypeName,AdTypeDescription,AdTypeComment,Case IsUsing WHEN 1 THEN '启用' ELSE '不启用' END IsUsing,UpdateTime
				            FROM
					            TB_AdType 
				            WHERE
					            isdeleted = 0 ";
            new AdTypeBal().GetPagerList(resultModel, searchModel, sql);
            return resultModel;
        }

        public override AjaxResultModel DeleteByIds()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            new AdTypeBal().DeleteByIds(resultModel, searchModel);
            return resultModel;
        }

        public override AjaxResultModel Download()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            string sql = @"SELECT
					            *
				            FROM
					            TB_AdType 
				            WHERE
					            isdeleted = 0 ";
            DataTable dt = new AdTypeBal().GetDataTable(searchModel, sql);
            string filePath = Server.MapPath("~/Temp/" + DateTime.Now.ToString("yyyyMMdd"));
            string fileName = " AdType_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            ExcelUtil.WriteExcel(dt, filePath, fileName);
            resultModel.data = "/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/" + fileName;
            return resultModel;
        }
    }
}
