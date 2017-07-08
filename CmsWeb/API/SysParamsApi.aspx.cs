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
using CmsUtils;

namespace CmsWeb.API
{
    public partial class SysParamsApi : BaseApi
    {
        public AjaxResultModel GetPagerList()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
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
            AjaxModel searchModel = GetPostParams();
            new SysParamsBal().DeleteByIds(resultModel, searchModel);
            return resultModel;
        }

        /// <summary>
        /// 验证参数名称是否重复
        /// </summary>
        public Dictionary<string, bool> ValidateParamsName()
        {
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            AjaxModel searchModel = GetPostParams();
            TB_SysParams sysModel = new TB_SysParams();
            Dictionary<string, string> postParams = new Dictionary<string, string>();
            GetValidateParams(searchModel, postParams);
            postParams.ConvertToEntity(sysModel);
            TB_SysParams validModel;
            if (postParams.ContainsKey("ID"))
            {//编辑
                int id;
                int.TryParse(postParams["ID"], out id);
                validModel = new SysParamsBal().SelectSingleById(s => !s.ID.Equals(id) && s.ParamName.Equals(sysModel.ParamName));
            }
            else
            {
                validModel = new SysParamsBal().SelectSingleById(s => s.ParamName.Equals(sysModel.ParamName));
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
