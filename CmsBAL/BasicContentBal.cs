using System;
using CmsDAL;
using System.Collections.Generic;
using System.Linq.Expressions;
using CmsCommon;
using CmsEntity;
using CmsUtils;

namespace CmsBAL
{
    public class BasicUserBal : BaseBal<TB_BasicUser>
    {
        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool ValidateAccount(TB_BasicUser userInfo)
        {
            TB_BasicUser selectUser;
            using (var ctx = new CmsEntities())
            {
                selectUser = new BasicUserDal(ctx).SelectSingle(u => u.UserAccount.Equals(userInfo.UserAccount));
            }
            if (selectUser != null && selectUser.UserPassword.Equals(SecurityUtil.Md5Encrypt64(userInfo.UserPassword + selectUser.PasswordSalt)))
            {
                EntityUtil.Copy(selectUser, userInfo);
                SessionUtil.SetSession(Constants.SESSION_LOGIN_USERINFO, selectUser);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除用户数据
        /// </summary>
        /// <param name="resultModel"></param>
        /// <param name="searchModel"></param>
        public void DeleteUser(DataTablesResultModel<TB_BasicUser> resultModel, SearchModel searchModel)
        {

            List<TB_BasicUser> userList = new List<TB_BasicUser>();
            using (var ctx = new CmsEntities())
            {
                BasicUserDal dal = new BasicUserDal(ctx);
                string[] ids = searchModel.ParamsDic["Id"].Split(new string[] { "," }, StringSplitOptions.None);
                int userId = 0;
                foreach (string id in ids)
                {
                    if (int.TryParse(id, out userId))
                    {
                        TB_BasicUser user = dal.SelectSingle(u => u.ID.Equals(userId));
                        if (user != null)
                        {
                            user.IsDeleted = 1;
                            userList.Add(user);
                        }
                    }
                    
                }
                int result = dal.UpdateList(userList);
                if (result > 0)
                {
                    resultModel.result = 1;
                }
            }
        }
    }
}