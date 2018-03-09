using System;
using System.Web;

namespace CmsUtils
{
    public class CookieUtil
    {
        /// <summary>
        ///     获取Cookie值
        /// </summary>
        /// <param name="cookieKey"></param>
        /// <returns></returns>
        public static string GetCookie(string cookieKey)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookieKey];
            var str = string.Empty;
            if (cookie != null)
                str = cookie.Value;
            return str;
        }

        /// <summary>
        ///     设置Cookie值
        /// </summary>
        /// <param name="cookieKey"></param>
        /// <param name="cookieValue"></param>
        /// <param name="expires">过期时间，默认为永久</param>
        public static void SetCookie(string cookieKey, string cookieValue, DateTime? expires = null)
        {
            SetCookie(cookieKey, cookieValue, expires ?? DateTime.MaxValue);
        }

        /// <summary>
        ///     设置Cookie值
        /// </summary>
        /// <param name="cookieKey"></param>
        /// <param name="cookieValue"></param>
        /// <param name="expires"></param>
        public static void SetCookie(string cookieKey, string cookieValue, DateTime expires)
        {
            var cookie = new HttpCookie(cookieKey)
            {
                Value = cookieValue,
                Path = "/",
                Expires = expires
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        ///     清除所有Cookie
        /// </summary>
        public void RemoveCookie()
        {
            HttpContext.Current.Request.Cookies.Clear();
        }

        /// <summary>
        ///     清除指定Cookie
        /// </summary>
        /// <param name="cookieKey"></param>
        public static void RemoveCookie(string cookieKey)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookieKey];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}