//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/24
// 文件说明：角色Ajax请求页
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
    public partial class RoleApi : BaseApi
    {
        public override AjaxResultModel GetPagerList()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            string sql = @"SELECT
	                            r.ID,r.RoleName,r.UpdateTime, CASE r.IsUsing WHEN 1 THEN '启用' ELSE '不启用' END AS IsUsing
                            FROM
	                            TB_Role r
                            WHERE
	                            r.isdeleted = 0 ";
            new RoleBal().GetPagerList(resultModel, searchModel, sql);
            return resultModel;
        }

        public override AjaxResultModel DeleteByIds()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            new RoleBal().DeleteByIds(resultModel, searchModel);
            return resultModel;
        }

        /// <summary>
        /// 获取树数据源
        /// </summary>
        /// <returns></returns>
        public AjaxResultModel GetTreeList()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            string sql = @"SELECT
					            *
				            FROM
					            TB_Authority 
				            WHERE
					            isdeleted = 0 ";
            TB_Authority rootNode = new TB_Authority();
            rootNode.ID = 0;
            rootNode.ParentID = -1;
            rootNode.AuthorName = "系统权限";
            List<TB_Authority> list = new AuthorityBal().GetDataTable(searchModel, sql).ToList<TB_Authority>();
            list.Insert(0, rootNode);
            resultModel.data = list;
            return resultModel;
        }

        public override AjaxResultModel Download()
        {
            throw new NotImplementedException();
        }
    }
}
