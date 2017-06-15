using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CmsEntity;
using MySql.Data.MySqlClient;

namespace CmsDAL
{
    public class BaseDal<T> where T : class
    {
        protected CmsEntities _ctx = null;
        protected DbSet _dbSet = null;

        public BaseDal(CmsEntities ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<T>();
        }

        public int InsertSingle(T entity)
        {
            _dbSet.Add(entity);
            return _ctx.SaveChanges();
        }

        public int InsertList(List<T> list)
        {
            _dbSet.AddRange(list);
            return _ctx.SaveChanges();
        }

        public List<T> SelectList(Expression<Func<T, bool>> where)
        {
            return _ctx.Set<T>().Where(where).ToList();
        }

        public List<T> SelectAllList()
        {
            return _ctx.Set<T>().ToList();
        }

        public T SelectSingle(Expression<Func<T, bool>> where)
        {
            return _ctx.Set<T>().FirstOrDefault(where);
        }

        public int UpdateSingle(T entity)
        {
            _ctx.Set<T>().Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;
            return _ctx.SaveChanges();
        }

        public int UpdateList(List<T> list)
        {
            foreach (T entity in list)
            {
                _ctx.Set<T>().Attach(entity);
                _ctx.Entry(entity).State = EntityState.Modified;
            }
            return _ctx.SaveChanges();
        }

        public void GetPagerList(DataTablesResultModel<T> resultModel, SearchModel searchModel, string sql)
        {
            string baseSql = string.Empty;
            string sqlWhere = string.Empty;
            List<DbParameter> paramsList = new List<DbParameter>();
            long limit = searchModel.Page * searchModel.Limit;

            if (searchModel.ParamsDic != null)
            {
                string[] valueParams;
                List<string> valueList = new List<string>();
                foreach (string paramValue in searchModel.ParamsDic.Keys)
                {
                    //0:字段名称（包含表别名），1：运算操作符，2：参数值
                    valueParams = paramValue.Split(new string[] { "|" }, StringSplitOptions.None);
                    if (valueParams.Length == 3)
                    {
                        if (valueParams[1] == "LIKE")
                        {
                            sqlWhere += " AND " + valueParams[0] + " " + valueParams[1] + " @" + valueParams[0] + " ";
                        }
                        else
                        {
                            sqlWhere += " AND " + valueParams[0] + valueParams[1] + " @" + valueParams[0] + " ";
                        }
                        if (!valueList.Any(v => v.Equals(valueParams[0])))
                        {//判断参数是否存在，如不存在添加，存在则不添加重复
                            MySqlParameter mySqlParam = new MySqlParameter();
                            mySqlParam.ParameterName = "@" + valueParams[0];
                            if (valueParams[1] == "LIKE")
                            {
                                mySqlParam.Value = "%" + searchModel.ParamsDic[paramValue] + "%";
                            }
                            else
                            {
                                mySqlParam.Value = searchModel.ParamsDic[paramValue];
                            }
                            paramsList.Add(mySqlParam);
                            valueList.Add(valueParams[0]);
                        }
                    }
                }
            }
            baseSql += "select * from (" + sql + ") t3 where 1=1 " + sqlWhere;

            string countSql = "select count(*) from (" + baseSql + ") t ";
            DbParameter[] parameters = paramsList.ToArray();
            resultModel.total = _ctx.Database.SqlQuery<long>(countSql, parameters).FirstOrDefault();
            string pageSql = " select * from ( " + baseSql + "  order by " + searchModel.OrderColunm + " " + (searchModel.OrderDir == "desc" ? "asc" : "desc") + " limit " + limit + ") t order BY " + searchModel.OrderColunm + " " + searchModel.OrderDir + " LIMIT " + (resultModel.total - limit > 0 ? searchModel.Limit : resultModel.total - (searchModel.Limit * (searchModel.Page - 1))) + " ";

            resultModel.data = _ctx.Database.SqlQuery<T>(pageSql, parameters).ToList();
        }
    }
}
