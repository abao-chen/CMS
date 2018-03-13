//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/28
// 文件说明：地区市数据库Ajax请求页
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
    public partial class PositionCountyApi : BaseApi
    {
        public override AjaxResultModel GetPagerList()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            string sql = @"SELECT
					            *
				            FROM
					            TB_PositionCounty 
				            WHERE
					            1 = 1 ";
            new PositionCountyBal().GetPagerList(resultModel, searchModel, sql);
            return resultModel;
        }

        public override AjaxResultModel DeleteByIds()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            new PositionCountyBal().DeleteByIds(resultModel, searchModel);
            return resultModel;
        }

        public override AjaxResultModel Download()
        {
            throw new NotImplementedException();
        }
    }
}
