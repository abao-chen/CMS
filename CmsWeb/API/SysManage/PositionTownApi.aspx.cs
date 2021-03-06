﻿//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/28
// 文件说明：镇数据库Ajax请求页
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
    public partial class PositionTownApi : BaseApi
    {
        public override AjaxResultModel GetPagerList()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            string sql = @"SELECT
					            *
				            FROM
					            TB_PositionTown 
				            WHERE
					            1 = 1 ";
            new PositionTownBal().GetPagerList(resultModel, searchModel, sql);
            return resultModel;
        }

        public override AjaxResultModel DeleteByIds()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            new PositionTownBal().DeleteByIds(resultModel, searchModel);
            return resultModel;
        }

        public override AjaxResultModel Download()
        {
            throw new NotImplementedException();
        }
    }
}
