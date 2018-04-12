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
    public partial class UserApi : BaseApi
    {
        public override AjaxResultModel GetPagerList()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            string sql = @"SELECT
					            u.*,
					            d1.DicName UserStatusName,
					            d2.DicName UserTypeName,
		                        (SELECT
			                        GROUP_CONCAT(r.RoleName)
		                        FROM
			                        TB_Role r
		                        JOIN TB_UserRole ur ON r.ID = ur.RoleID
		                        WHERE
			                        ur.UserID = u.ID
		                        GROUP BY
			                        ur.UserID) RoleNames
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

        public override AjaxResultModel DeleteByIds()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            new BasicUserBal().DeleteByIds(resultModel, searchModel);
            return resultModel;
        }

        public override AjaxResultModel Download()
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
            DataTable dt = new BasicUserBal().GetDataTable(searchModel, sql);
            string filePath = Server.MapPath("~/Temp/" + DateTime.Now.ToString("yyyyMMdd"));
            string fileName = "UserList_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            ExcelUtil.WriteExcel(dt, filePath, fileName, new string[] { "ID", "UserType", "UserStatus" });
            resultModel.data = "/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/" + fileName;
            return resultModel;
            //FileDownHelper.DownLoadFile(filePath + "/" + fileName, fileName);
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

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <returns></returns>
        public AjaxResultModel UpdatePassword()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            string id = GetRequestParam("Id");
            string newPwd = GetRequestParam("newPwd");
            string confirmPwd = GetRequestParam("confirmPwd");
            if (id.IsEmpty() || newPwd.IsEmpty() || confirmPwd.IsEmpty())
            {
                resultModel.result = 2;
                resultModel.message = "缺少必要参数";
            }
            else
            {
                if (newPwd != confirmPwd)
                {
                    resultModel.result = 2;
                    resultModel.message = "密码输入不一致，请重新输入！";
                }
                else
                {
                    int i_Id = id.ToInt();
                    TB_BasicUser user = new BasicUserBal().SelectSingleById(u => u.ID == i_Id && u.IsDeleted == Constants.IS_NO);
                    user.PasswordSalt = SecurityUtil.RandomCode(Constants.RANDOM_MODEL_MIXED, 10);
                    user.UserPassword = SecurityUtil.Md5Encrypt64(newPwd + user.PasswordSalt);
                    int i = new BasicUserBal().UpdateSingle(user);
                    if (i == 0)
                    {
                        resultModel.result = 2;
                        resultModel.message = "修改密码失败！";
                    }
                }
            }
            return resultModel;
        }

        /// <summary>
        /// 修改自己的密码
        /// </summary>
        /// <returns></returns>
        public AjaxResultModel UpdateSelfPassword()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            string oldPwd = GetRequestParam("oldPwd");
            string newPwd = GetRequestParam("newPwd");
            string confirmPwd = GetRequestParam("confirmPwd");
            if (newPwd.IsEmpty() || confirmPwd.IsEmpty())
            {
                resultModel.result = 2;
                resultModel.message = "缺少必要参数";
            }
            else
            {
                if (newPwd != confirmPwd)
                {
                    resultModel.result = 2;
                    resultModel.message = "密码输入不一致，请重新输入！";
                }
                else if (LoginUserInfo.UserPassword != SecurityUtil.Md5Encrypt64(oldPwd + LoginUserInfo.PasswordSalt))
                {

                    resultModel.result = 2;
                    resultModel.message = "原密码输入不正确，请重新输入！";
                }
                else
                {
                    TB_BasicUser user = LoginUserInfo;
                    user.PasswordSalt = SecurityUtil.RandomCode(Constants.RANDOM_MODEL_MIXED, 10);
                    user.UserPassword = SecurityUtil.Md5Encrypt64(newPwd + user.PasswordSalt);
                    int i = new BasicUserBal().UpdateSingle(user);
                    if (i == 0)
                    {
                        resultModel.result = 2;
                        resultModel.message = "修改密码失败！";
                    }
                }
            }
            return resultModel;
        }
    }
}