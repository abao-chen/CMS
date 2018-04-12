//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2018/03/09
// 文件说明：内容类型Ajax请求页
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
    public partial class ContentTypeApi : BaseApi
    {
        public override AjaxResultModel GetPagerList()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            string sql = @"SELECT
					          ID,TypeName,TypeAlias,Case IsUse WHEN 1 THEN '启用' ELSE '不启用' END IsUse,UpdateTime
				            FROM
					            TB_ContentType 
				            WHERE
					            isdeleted = 0 ";
            new ContentTypeBal().GetPagerList(resultModel, searchModel, sql);
            return resultModel;
        }

        public override AjaxResultModel DeleteByIds()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            new ContentTypeBal().DeleteByIds(resultModel, searchModel);
            return resultModel;
        }

        public override AjaxResultModel Download()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            string sql = @"SELECT
					            *
				            FROM
					            TB_ContentType 
				            WHERE
					            isdeleted = 0 ";
            DataTable dt = new ContentTypeBal().GetDataTable(searchModel, sql);
            string filePath = Server.MapPath("~/Temp/" + DateTime.Now.ToString("yyyyMMdd"));
            string fileName = " ContentType_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            ExcelUtil.WriteExcel(dt, filePath, fileName);
            resultModel.data = "/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/" + fileName;
            return resultModel;
        }
    }
}
