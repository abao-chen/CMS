using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CmsUtils
{
    public class CookieUtil
    {
        /// <summary>
        /// 获取Cookie值
        /// </summary>
        /// <param name="cookieKey"></param>
        /// <returns></returns>
        public static string GetCookie(string cookieKey)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieKey];
            string str = string.Empty;
            if (cookie != null)
            {
                str = cookie.Value;
            }
            return str; 
        }
        /// <summary>
        /// 设置Cookie值(过期时间)
        /// </summary>
        /// <param name="cookieKey"></param>
        /// <param name="cookieValue"></param>
        public static void SetCookie(string cookieKey, string cookieValue)
        {
            SetCookie(cookieKey, cookieValue, DateTime.Now.AddDays(1.0)); 
        }
        /// <summary>
        /// 设置Cookie值
        /// </summary>
        /// <param name="cookieKey"></param>
        /// <param name="cookieValue"></param>
        /// <param name="expires"></param>
        public static void SetCookie(string cookieKey, string cookieValue, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(cookieKey)
            {
                Value = cookieKey,
                Expires = expires
            };
            HttpContext.Current.Response.Cookies.Add(cookie); 
        }
        /// <summary>
        /// 清除所有Cookie
        /// </summary>
        public void RemoveCookie()
        {
            System.Web.HttpContext.Current.Request.Cookies.Clear();
        }
        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookieKey"></param>
        public static void RemoveCookie(string cookieKey)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieKey];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            } 
        }

    }
}
