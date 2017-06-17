using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace CmsCommon
{
    public static class GlobalConfig

    {
        /// <summary>
        /// API 程序集名称
        /// </summary>
        public static readonly string Assembly = WebConfigurationManager.AppSettings["Assembly"];

        /// <summary>
        /// API 命名空间
        /// </summary>
        public static readonly string ApiNamespace = WebConfigurationManager.AppSettings["ApiNamespace"];

        /// <summary>
        /// 登录默认页面
        /// </summary>
        public static readonly string DefaultUrl = WebConfigurationManager.AppSettings["DefaultUrl"];
    }
}
