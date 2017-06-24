//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/24
// 文件说明：系统参数Ajax请求页
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

namespace CmsWeb.API
{
    public partial class SysParamsApi : APIBase
    {
        public AjaxResultModel GetPagerList()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            SearchModel searchModel = GetPostParams();
            string sql = @"SELECT
					            *
				            FROM
					            TB_SysParams 
				            WHERE
					            isdeleted = 0 ";
            new SysParamsBal().GetPagerList(resultModel, searchModel, sql);
            return resultModel;
        }

        public AjaxResultModel DeleteByIds()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            SearchModel searchModel = GetPostParams();
            new SysParamsBal().DeleteByIds(resultModel, searchModel);
            return resultModel;
        }
    }
}
