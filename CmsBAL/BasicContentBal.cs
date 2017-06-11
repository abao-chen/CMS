using System;
using CmsDAL;
using System.Collections.Generic;
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
                SessionUtil.SetSession("LoginUserInfo", selectUser);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}