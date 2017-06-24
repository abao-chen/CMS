using System.Web;

namespace CmsUtils
{
    public static class SessionUtil
    {
        /// <summary>
        ///     设置Session
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetSession(string key, object value)
        {
            if (key.IsEmpty())
                return;
            HttpContext.Current.Session.Add(key, value);
        }

        /// <summary>
        ///     获取Session
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetSession(string key)
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null && !key.IsEmpty())
                return HttpContext.Current.Session[key];
            return null;
        }

        /// <summary>
        ///     删除Session
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void RemoveSession(string key)
        {
            if (HttpContext.Current != null
                && HttpContext.Current.Session != null
                && HttpContext.Current.Session[key] != null)
                HttpContext.Current.Session.Remove(key);
        }

        /// <summary>
        ///     清除所有Session
        /// </summary>
        /// <returns></returns>
        public static void ClearAllSession()
        {
            if (HttpContext.Current != null
                && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Session.RemoveAll();
            }
        }
    }
}