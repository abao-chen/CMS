//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/24
// 文件说明：字典类型Ajax请求页
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

namespace CmsWeb.API
{
    public partial class DicTypeApi : APIBase
    {
        public AjaxResultModel GetPagerList()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            string sql = @"SELECT
					            *
				            FROM
					            TB_DicType 
				            WHERE
					            isdeleted = 0 ";
            new DicTypeBal().GetPagerList(resultModel, searchModel, sql);
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
					            *,0 pId
				            FROM
					            TB_DicType 
				            WHERE
					            isdeleted = 0 ";
            TB_DicType rootNode = new TB_DicType();
            rootNode.ID = 0;
            rootNode.pId = -1;
            rootNode.DicTypeName = "字典类型";
            List<TB_DicType> list = new DicTypeBal().GetDataTable(searchModel, sql).ToList<TB_DicType>();
            list.Insert(0, rootNode);
            resultModel.data = list;
            return resultModel;
        }

        public AjaxResultModel DeleteByIds()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            new DicTypeBal().DeleteByIds(resultModel, searchModel);
            return resultModel;
        }
    }
}
