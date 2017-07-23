using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

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
            var ignoreP = new List<string>();
            if (ignorePoperties != null && ignorePoperties.Length > 0)
                ignoreP = ignorePoperties.ToList();
            var tFields = target.GetType().GetProperties();
            var sFields = source.GetType().GetProperties();
            foreach (var item in tFields)
                if (!ignoreP.Contains(item.Name.ToLower()))
                    foreach (var si in sFields)
                        if (si.Name == item.Name)
                        {
                            var svalue = si.GetValue(source, null);
                            var tvalue = item.GetValue(target, null);
                            if (svalue != null && !svalue.Equals(tvalue))
                                item.SetValue(target, svalue, null);
                        }
        }

        public static List<T> ToList<T>(this DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {

                // 定义集合    
                var list = new List<T>();
                foreach (DataRow dr in dt.Rows)
                {
                    var entity = Activator.CreateInstance<T>();
                    ;
                    // 获得此模型的公共属性      
                    var propertys = entity.GetType().GetProperties();
                    foreach (var pi in propertys)
                        if (dt.Columns.Contains(pi.Name) && pi.CanWrite)
                        {
                            // 检查DataTable是否包含此列和属性是否有Setter
                            var value = dr[pi.Name];
                            if (value != DBNull.Value)
                                pi.SetValue(entity, value, null);
                        }
                    list.Add(entity);
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 有Dictionary转换成Entity
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="dic">Dictionary</param>
        /// <returns>Entity</returns>
        public static void ConvertToEntity<T>(this Dictionary<string, string> dic, T entity)
        {
            var fields = entity.GetType().GetProperties();
            foreach (PropertyInfo pi in fields)
            {
                if (dic.Keys.Contains(pi.Name) && pi.CanWrite)
                {
                    string value = dic[pi.Name];
                    if (value != null)
                    {
                        if (pi.PropertyType.FullName == typeof(string).ToString())
                        {
                            pi.SetValue(entity, value, null);
                        }
                        else if (pi.PropertyType.FullName == typeof(decimal).ToString())
                        {
                            pi.SetValue(entity, decimal.Parse(value), null);
                        }
                        else if (pi.PropertyType.FullName == typeof(decimal?).ToString())
                        {
                            pi.SetValue(entity, decimal.Parse(value), null);
                        }
                        else if (pi.PropertyType.FullName == typeof(int).ToString())
                        {
                            pi.SetValue(entity, int.Parse(value), null);
                        }
                        else if (pi.PropertyType.FullName == typeof(int?).ToString())
                        {
                            pi.SetValue(entity, int.Parse(value), null);
                        }
                    }
                }
            }
        }
    }
}