﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CmsUtils
{
    public static class SessionUtil
    {
        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        /// <summary>
        /// 获取Session
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetSession(string key)
        {
            return HttpContext.Current.Session[key];
        }
    }
}