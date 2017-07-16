using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using CmsUtils;

namespace CmsCommon
{
    public static class GlobalConfig

    {
        /// <summary>
        /// API 程序集名称
        /// </summary>
        public static readonly string Assembly = Configs.GetValue("Assembly");

        /// <summary>
        /// API 命名空间
        /// </summary>
        public static readonly string ApiNamespace = Configs.GetValue("ApiNamespace");

        /// <summary>
        /// 登录默认跳转页面URL
        /// </summary>
        public static readonly string DefaultUrl = Configs.GetValue("DefaultUrl");

        /// <summary>
        /// 登录页面URL
        /// </summary>
        public static readonly string LoginUrl = Configs.GetValue("LoginUrl");

        /// <summary>
        /// 是否验证权限
        /// </summary>
        public static readonly string IsValidAuthor = Configs.GetValue("IsValidAuthor");

        /// <summary>
        /// 是否验证登录验证码
        /// </summary>
        public static readonly string IsValidateCode = Configs.GetValue("IsValidateCode");

        /// <summary>
        /// 邮件服务器
        /// </summary>
        public static readonly string MailServer = Configs.GetValue("MailServer");

        /// <summary>
        /// 邮件账号用户名
        /// </summary>
        public static readonly string MailUserName = Configs.GetValue("MailUserName");

        /// <summary>
        /// 邮件账号密码
        /// </summary>
        public static readonly string MailPassword = Configs.GetValue("MailPassword");

        /// <summary>
        /// 邮件服务器端口
        /// </summary>
        public static readonly int MailPort = Convert.ToInt32(Configs.GetValue("MailPort"));

        /// <summary>
        /// 邮件发送人显示名称
        /// </summary>
        public static readonly string MailDisplayName = Configs.GetValue("MailDisplayName");


    }
}
