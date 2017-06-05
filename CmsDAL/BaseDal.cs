using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entity;
using MySql.Data.MySqlClient;

namespace CmsDAL
{
    public class BaseDal<T> where T : class
    {
        private CmsEntities _ctx = null;
        private DbSet _dbSet = null;

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

        public int DeleteSingle(T entity)
        {
            _dbSet.Remove(entity);
            return _ctx.SaveChanges();
        }

        public int DeleteList(List<T> list)
        {
            _dbSet.RemoveRange(list);
            return _ctx.SaveChanges();
        }

        public List<T> SelectList(Expression<Func<T, bool>> where)
        {
            return _ctx.Set<T>().Where(where).ToList();
        }

        public List<T> SelectList()
        {
            return _ctx.Set<T>().ToList();
        }

        public T SelectSingleById(Expression<Func<T, bool>> where)
        {
            return _ctx.Set<T>().FirstOrDefault(where);
        }

        public int UpdateSingle(T entity)
        {
            return _ctx.SaveChanges();
        }

        public int UpdateList(List<T> list)
        {
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
                foreach (string paramValue in searchModel.ParamsDic.Keys)
                {
                    sqlWhere = " " + paramValue + " =  @" + paramValue + " ";
                    MySqlParameter mySqlParam = new MySqlParameter();
                    mySqlParam.ParameterName = "@" + paramValue;
                    mySqlParam.Value = searchModel.ParamsDic[paramValue];
                    paramsList.Add(mySqlParam);
                }
            }
            baseSql += sql + sqlWhere;

            string countSql = "select count(*) from (" + baseSql + ") t ";
            DbParameter[] parameters = paramsList.ToArray();
            resultModel.total = _ctx.Database.SqlQuery<long>(countSql, parameters).FirstOrDefault();
            string pageSql = " select * from ( " + baseSql + "  order by " + searchModel.OrderColunm + " " + (searchModel.OrderDir == "desc" ? "asc" : "desc") + " limit " + limit + ") t order BY " + searchModel.OrderColunm + " " + searchModel.OrderDir + " LIMIT " + (resultModel.total - limit > 0 ? searchModel.Limit : resultModel.total - (searchModel.Limit * (searchModel.Page - 1))) + " ";
            
            resultModel.data = _ctx.Database.SqlQuery<T>(pageSql, parameters).ToList();
        }
    }
}
