//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/24
// 文件说明：字典Ajax请求页
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
    public partial class DictionaryApi : BaseApi
    {
        /// <summary>
        /// 获取分页列表数据
        /// </summary>
        /// <returns></returns>
        public AjaxResultModel GetPagerList()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            string sql = @"SELECT
	                            d.*, dt.DicTypeName
                            FROM
	                            TB_Dictionary d
                            LEFT JOIN TB_DicType dt ON d.DicTypeCode = dt.DicTypeCode
                            WHERE
	                            d.isdeleted = 0";
            new DictionaryBal().GetPagerList(resultModel, searchModel, sql);
            return resultModel;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public AjaxResultModel DeleteByIds()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            new DictionaryBal().DeleteByIds(resultModel, searchModel);
            return resultModel;
        }

        /// <summary>
        /// 验证参数名称是否重复
        /// </summary>
        public Dictionary<string, bool> ValidateDicCode()
        {
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            AjaxModel searchModel = GetPostParams();
            TB_Dictionary entity = new TB_Dictionary();
            Dictionary<string, string> postParams = new Dictionary<string, string>();
            GetValidateParams(searchModel, postParams);
            postParams.ConvertToEntity(entity);
            TB_Dictionary validModel;
            if (postParams.ContainsKey("ID"))
            {//编辑
                int id;
                int.TryParse(postParams["ID"], out id);
                validModel = new DictionaryBal().SelectSingleById(s => !s.ID.Equals(id) && s.DicTypeCode.Equals(entity.DicTypeCode) && s.DicCode.Equals(entity.DicCode));
            }
            else
            {
                validModel = new DictionaryBal().SelectSingleById(s => s.DicTypeCode.Equals(entity.DicTypeCode) && s.DicCode.Equals(entity.DicCode));
            }

            if (validModel != null)
            {
                result.Add("valid", false);
            }
            else
            {
                result.Add("valid", true);
            }
            return result;
        }
    }
}
