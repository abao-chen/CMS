using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
        public static void CopyTo<T>(this T source, T target, params string[] ignorePoperties)
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

        public static List<T> ToList<T>(this DataTable dt)
        {
            // 定义集合    
            List<T> list = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T entity = Activator.CreateInstance<T>(); ;
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = entity.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (dt.Columns.Contains(pi.Name) && pi.CanWrite)
                    {// 检查DataTable是否包含此列和属性是否有Setter
                        object value = dr[pi.Name];
                        if (value != DBNull.Value)
                        {
                            pi.SetValue(entity, value, null);
                        }
                    }
                }
                list.Add(entity);
            }
            return list;
        }
    }
}
