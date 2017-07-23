using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsCommon
{
    public static class Constants
    {
        #region 字典类型

        /// <summary>
        /// 用户类型
        /// </summary>
        public const string DIC_TYPE_USERTYPE = "U01000";

        /// <summary>
        /// 用户状态
        /// </summary>
        public const string DIC_TYPE_USERSTATUS = "U02000";

        #endregion

        #region 随机字符生成模式

        /// <summary>
        /// 纯数字
        /// </summary>
        public const int RANDOM_MODEL_NUM = 1;

        /// <summary>
        /// 纯字母
        /// </summary>
        public const int RANDOM_MODEL_LETTER = 2;

        /// <summary>
        /// 数字、字母混合
        /// </summary>
        public const int RANDOM_MODEL_MIXED = 3;

        #endregion

        #region Session Key

        /// <summary>
        /// 登录用户信息
        /// </summary>
        public const string SESSION_LOGIN_USERINFO = "LoginUserInfo";

        #endregion

        #region 固定参数名称

        /// <summary>
        /// 上一次session丢失的页面URL
        /// </summary>
        public const string REQUEST_PREURL = "PreUrl";

        #endregion

        #region 是否标识位

        /// <summary>
        /// 是
        /// </summary>
        public const int IS_YES = 1;

        /// <summary>
        /// 否
        /// </summary>
        public const int IS_NO = 0;

        #endregion

        #region 用户状态

        /// <summary>
        /// 用户状态：使用中
        /// </summary>
        public const string USER_STATUS_USING = "U00201";

        /// <summary>
        /// 用户状态：已冻结
        /// </summary>
        public const string USER_STATUS_FROZEN = "U00202";

        #endregion

        #region 权限标识

        /// <summary>
        /// 权限标识：模块
        /// </summary>
        public const int AUTHOR_FLAG_MODULE = 1;

        /// <summary>
        /// 权限标识：页面
        /// </summary>
        public const int AUTHOR_FLAG_PAGE = 2;

        /// <summary>
        /// 权限标识：按钮
        /// </summary>
        public const int AUTHOR_FLAG_BUTTON = 3;

        #endregion
    }
}
