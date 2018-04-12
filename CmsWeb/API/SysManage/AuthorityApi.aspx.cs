//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/25
// 文件说明：权限Ajax请求页
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
    public partial class AuthorityApi : BaseApi
    {
        public override AjaxResultModel GetPagerList()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            string sql = @"SELECT
					          ID,AuthorType,CASE AuthorType WHEN 1 THEN '模块' WHEN 2 THEN '页面' WHEN 3 THEN '操作' END AuthorTypeName,
							  AuthorName,AuthorFlag,ParentID,FullID,CASE IsMenu WHEN 1 THEN '是' ELSE '否' END IsMenu, PageUrl, UpdateTime
				            FROM
					            TB_Authority 
				            WHERE
					            isdeleted = 0 ";
            new AuthorityBal().GetPagerList(resultModel, searchModel, sql);
            return resultModel;
        }

        public override AjaxResultModel DeleteByIds()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            new AuthorityBal().DeleteByIds(resultModel, searchModel);
            return resultModel;
        }

        public override AjaxResultModel Download()
        {
            throw new NotImplementedException();
        }
    }
}
