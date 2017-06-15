using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsUtils
{
    public static class EntityUtil
    {
        /// <param name="source">源对象</param>
        /// <param name="target">目标对象</param>
        /// <param name="ignorePoperties"></param>
        /// <returns></returns>
        public static void Copy<T>(T source, T target, params string[] ignorePoperties)
        {
            List<string> ignoreP = new List<string>();
            if (ignorePoperties != null && ignorePoperties.Length > 0)
            {
                ignoreP = ignorePoperties.ToList();
            }
            var tFields = target.GetType().GetProperties();
            var sFields = source.GetType().GetProperties();
            foreach (var item in tFields)
            {
                if (!ignoreP.Contains(item.Name.ToLower()))
                {
                    foreach (var si in sFields)
                    {
                        if (si.Name == item.Name)
                        {
                            object svalue = si.GetValue(source, null);
                            object tvalue = item.GetValue(target, null);
                            if (svalue != null && !svalue.Equals(tvalue))
                            {
                                item.SetValue(target, svalue, null);
                            }
                        }
                    }
                }
            }
        }
    }
}
