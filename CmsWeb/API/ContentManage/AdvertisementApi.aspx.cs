//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/07/22
// 文件说明：广告Ajax请求页
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

namespace Cms.Web.Admin.API
{
    public partial class AdvertisementApi : BaseApi
    {
        public override AjaxResultModel GetPagerList()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            string sql = @"SELECT
					          a.ID,a.AdTypeID,a.AdName,a.AdImage,a.AdUrl,a.ValidStartTime,a.ValidEndTime,Case a.IsUsing WHEN 1 THEN '启用' ELSE '不启用' END IsUsing,`at`.AdTypeName
				            FROM
					            TB_Advertisement a
										left JOIN TB_AdType at 
										on `at`.IsDeleted=0 AND `at`.ID=a.AdTypeID
				            WHERE
					            a.isdeleted = 0  ";
            new AdvertisementBal().GetPagerList(resultModel, searchModel, sql);
            return resultModel;
        }

        public override AjaxResultModel DeleteByIds()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            new AdvertisementBal().DeleteByIds(resultModel, searchModel);
            return resultModel;
        }

        public override AjaxResultModel Download()
        {
            throw new NotImplementedException();
        }
    }
}
