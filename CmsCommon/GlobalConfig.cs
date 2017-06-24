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
    }
}
