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
    public partial class UserApi : BaseApi
    {
        public AjaxResultModel GetPagerList()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            string sql = @"SELECT
					            u.*,
					            d1.DicName UserStatusName,
					            d2.DicName UserTypeName
				            FROM
					            tb_basicuser u
				            LEFT JOIN tb_dictionary d1 ON d1.isdeleted = 0
				            AND d1.DicTypeCode = '{0}'
				            AND d1.DicCode = u.UserStatus
				            LEFT JOIN tb_dictionary d2 ON d2.isdeleted = 0
				            AND d2.DicTypeCode = '{1}'
				            AND d2.DicCode = u.UserType
				            WHERE
					            u.isdeleted = 0 ";
            
            sql = string.Format(sql, Constants.DIC_TYPE_USERSTATUS, Constants.DIC_TYPE_USERTYPE);
            new BasicUserBal().GetPagerList(resultModel, searchModel, sql);
            return resultModel;
        }

        public AjaxResultModel DeleteByIds()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            new BasicUserBal().DeleteByIds(resultModel, searchModel);
            return resultModel;
        }

        /// <summary>
        /// 验证用户名称是否重复
        /// </summary>
        public Dictionary<string, bool> ValidateUserAccount()
        {
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            AjaxModel searchModel = GetPostParams();
            TB_BasicUser model = new TB_BasicUser();
            Dictionary<string, string> postParams = new Dictionary<string, string>();
            GetValidateParams(searchModel, postParams);
            postParams.ConvertToEntity(model);
            TB_BasicUser validModel;
            if (postParams.ContainsKey("ID"))
            {//编辑
                int id;
                int.TryParse(postParams["ID"], out id);
                validModel = new BasicUserBal().SelectSingleById(s => !s.ID.Equals(id) && s.UserAccount.Equals(model.UserAccount) && s.IsDeleted == Constants.IS_NO);
            }
            else
            {
                validModel = new BasicUserBal().SelectSingleById(s => s.UserAccount.Equals(model.UserAccount) && s.IsDeleted == Constants.IS_NO);
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