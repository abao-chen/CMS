using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq.Dynamic;

namespace CmsUtils
{
    public static class BuildFilter
    {
        #region Linq

        public static Expression<Func<T, bool>> BuildFilterExpression<T>(this List<FilterModel> filterConditionList)
        {
            Expression<Func<T, bool>> condition = null;
            try
            {
                if (filterConditionList != null && filterConditionList.Count > 0)
                {
                    foreach (FilterModel filterCondition in filterConditionList)
                    {
                        if (filterCondition.Value.IsNotEmpty())
                        {
                            Expression<Func<T, bool>> tempCondition = BuildLambda<T>(filterCondition);
                            if (condition == null)
                            {
                                condition = tempCondition;
                            }
                            else
                            {
                                if (FilterLogic.AND.Equals(filterCondition.Logic))
                                {
                                    condition = condition.And(tempCondition);
                                }
                                else
                                {
                                    condition = condition.Or(tempCondition);
                                }
                            }
                        }
                    }
                }

                FilterModel delModel = new FilterModel();
                delModel.Action = FilterAction.Equal;
                delModel.Column = "IsDeleted";
                delModel.Logic = FilterLogic.AND;
                delModel.Value = "0";
                delModel.DataType = FilterDataType.Int;
                if (condition == null)
                {
                    condition = BuildLambda<T>(delModel);
                }
                else
                {
                    condition = condition.And(BuildLambda<T>(delModel));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return condition;
        }

        private static Expression<Func<T, bool>> BuildLambda<T>(FilterModel filterCondition)
        {
            var parameter = Expression.Parameter(typeof(T), "p");//创建参数i
            ConstantExpression constant = null;//创建常数
            switch (filterCondition.DataType)
            {
                case FilterDataType.Int:
                    constant = Expression.Constant(filterCondition.Value.ToIntOrNull(), typeof(int?));
                    break;
                case FilterDataType.Decimal:
                    constant = Expression.Constant(filterCondition.Value.ToDecimalOrNull(), typeof(decimal?));
                    break;
                case FilterDataType.DateTime:
                    constant = Expression.Constant(filterCondition.Value.ToDateOrNull(), typeof(DateTime?));
                    break;
                default:
                    constant = Expression.Constant(filterCondition.Value);
                    break;
            }
            MemberExpression member = Expression.PropertyOrField(parameter, filterCondition.Column);
            if (FilterAction.Equal.Equals(filterCondition.Action))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.Equal(member, constant), parameter);
            }
            else if (FilterAction.NotEqual.Equals(filterCondition.Action))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.NotEqual(member, constant), parameter);
            }
            else if (FilterAction.GreaterThan.Equals(filterCondition.Action))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.GreaterThan(member, constant), parameter);
            }
            else if (FilterAction.LessThan.Equals(filterCondition.Action))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.LessThan(member, constant), parameter);
            }
            else if (FilterAction.GreaterThanOrEqual.Equals(filterCondition.Action))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(member, constant), parameter);
            }
            else if (FilterAction.LessThanOrEqual.Equals(filterCondition.Action))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.LessThanOrEqual(member, constant), parameter);
            }
            else if (FilterAction.Like.Equals(filterCondition.Action))
            {
                return GetExpressionWithMethod<T>("Contains", filterCondition);
            }
            else if (FilterAction.NotLike.Equals(filterCondition.Action))
            {
                return GetExpressionWithoutMethod<T>("Contains", filterCondition);
            }
            else
            {
                return null;
            }
        }

        private static Expression<Func<T, bool>> GetExpressionWithMethod<T>(string methodName, FilterModel filterCondition)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "p");
            MethodCallExpression methodExpression = GetMethodExpression(methodName, filterCondition.Column, filterCondition.Value, parameterExpression);
            return Expression.Lambda<Func<T, bool>>(methodExpression, parameterExpression);
        }

        private static Expression<Func<T, bool>> GetExpressionWithoutMethod<T>(string methodName, FilterModel filterCondition)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "p");
            MethodCallExpression methodExpression = GetMethodExpression(methodName, filterCondition.Column, filterCondition.Value, parameterExpression);
            var notMethodExpression = Expression.Not(methodExpression);
            return Expression.Lambda<Func<T, bool>>(notMethodExpression, parameterExpression);
        }

        /// <summary>
        /// 生成类似于p=>p.values.Contains("xxx");的lambda表达式
        /// parameterExpression标识p，propertyName表示values，propertyValue表示"xxx",methodName表示Contains
        /// 仅处理p的属性类型为string这种情况
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <param name="parameterExpression"></param>
        /// <returns></returns>
        private static MethodCallExpression GetMethodExpression(string methodName, string propertyName, string propertyValue, ParameterExpression parameterExpression)
        {
            var propertyExpression = Expression.Property(parameterExpression, propertyName);
            MethodInfo method = typeof(string).GetMethod(methodName, new[] { typeof(string) });
            var someValue = Expression.Constant(propertyValue, typeof(string));
            return Expression.Call(propertyExpression, method, someValue);
        }

        public static IQueryable<T> DataSort<T>(this IQueryable<T> source, string sortExpression, string sortDirection)
        {
            Type t = typeof(T);
            var parameter = Expression.Parameter(t, "t");
            var property = t.GetProperty(sortExpression);
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var resultExp = Expression.Call(typeof(Queryable), sortDirection == "ASC" ? "OrderBy" : "OrderByDescending", new Type[] { t, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
            //return source.OrderBy(sortExpression + " " + sortDirection);
        }

        public static IQueryable<T> DataPaging<T>(this IQueryable<T> source, int pageNumber, int pageSize, out int totalCount, string sortExpression, string sortDirection)
        {
            totalCount = source.Count();
            var query = source.DataSort(sortExpression, sortDirection).Skip(pageNumber * pageSize).Take(pageSize);
            return query;
        }
    }

    public static class LinqBuilder
    {
        /// <summary>
        /// 默认True条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>() { return f => true; }

        /// <summary>
        /// 默认False条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        /// <summary>
        /// 拼接 OR 条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> condition)
        {
            var inv = Expression.Invoke(condition, exp.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.Or(exp.Body, inv), exp.Parameters);
        }

        /// <summary>
        /// 拼接And条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> condition)
        {
            var inv = Expression.Invoke(condition, exp.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.And(exp.Body, inv), exp.Parameters);
        }
    }

    #endregion

    #region SQL

    #endregion

    public class FilterModel
    {
        public FilterModel()
        {
            this.Action = FilterAction.Equal;
            this.Logic = FilterLogic.AND;
            this.DataType = FilterDataType.String;
        }

        public string Column { get; set; }//过滤条件中使用的数据列
        public FilterAction Action { get; set; }//过滤条件中的操作:==、!=等
        public FilterLogic Logic { get; set; }//过滤条件之间的逻辑关系：AND和OR
        public string Value { get; set; }//过滤条件中的操作的值
        public FilterDataType DataType { get; set; }//过滤条件中的操作的字段的类型
    }

    public enum FilterAction
    {
        Equal = 1,
        NotEqual = 2,
        GreaterThan = 3,
        LessThan = 4,
        GreaterThanOrEqual = 5,
        LessThanOrEqual = 6,
        Like = 7,
        NotLike = 8
    }
    public enum FilterLogic
    {
        AND = 1,
        OR = 2
    }
    public enum FilterDataType
    {
        String = 1,
        Int = 2,
        Decimal = 3,
        DateTime = 4
    }
}
