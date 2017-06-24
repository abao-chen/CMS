//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：#Date#
// 文件说明：#CnFileName#Ajax请求页
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
    public partial class #ClassName#Api : APIBase
    {
        public AjaxResultModel GetPagerList()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            SearchModel searchModel = GetPostParams();
            string sql = @"SELECT
					            *
				            FROM
					            #TableName# 
				            WHERE
					            isdeleted = 0 ";
            new #ClassName#Bal().GetPagerList(resultModel, searchModel, sql);
            return resultModel;
        }

        public AjaxResultModel DeleteByIds()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            SearchModel searchModel = GetPostParams();
            new #ClassName#Bal().DeleteByIds(resultModel, searchModel);
            return resultModel;
        }
    }
}