﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CmsCommon;
using CmsEntity;
using MySql.Data.MySqlClient;

namespace CmsDAL
{
    public class BaseDal<T> where T : class
    {
        protected CmsEntities _ctx;
        private DbSet _dbSet;
        private Log Logger;
        private DbContextTransaction _dbTransac;

        public BaseDal(CmsEntities ctx)
        {
            _ctx = ctx;
            //Logger = LogFactory.GetLogger(this.GetType());
            //_ctx.Database.Log = s =>
            //{
            //    Logger.Debug(s);
            //};
            _dbSet = _ctx.Set<T>();
        }

        public void BeginTran()
        {
            if (_dbTransac == null)
            {
                _dbTransac = _ctx.Database.BeginTransaction();
            }
        }

        public void CommitTran()
        {
            if (_dbTransac != null)
            {
                _dbTransac.Commit();
            }
        }



        public int InsertSingle(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                return _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                //Logger.Error(e.Message);
                if (_dbTransac != null)
                {
                    _dbTransac.Rollback();
                }
                throw e;
            }
        }

        public int InsertList(List<T> list)
        {
            try
            {
                _dbSet.AddRange(list);
                return _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                if (_dbTransac != null)
                {
                    _dbTransac.Rollback();
                }
                throw e;
            }
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
            try
            {
                _ctx.Set<T>().Attach(entity);
                _ctx.Entry(entity).State = EntityState.Modified;
                return _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                if (_dbTransac != null)
                {
                    _dbTransac.Rollback();
                }
                throw e;
            }
        }

        public int UpdateList(List<T> list)
        {
            try
            {
                foreach (T entity in list)
                {
                    _ctx.Set<T>().Attach(entity);
                    _ctx.Entry(entity).State = EntityState.Modified;
                }
                return _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                if (_dbTransac != null)
                {
                    _dbTransac.Rollback();
                }
                throw e;
            }
        }

        /// <summary>
        /// 删除列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public int DeleteList<T>(List<T> list) where T : class
        {
            try
            {
                _ctx.Set<T>().RemoveRange(list);
                return _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                if (_dbTransac != null)
                {
                    _dbTransac.Rollback();
                }
                throw e;
            }
        }

        /// <summary>
        /// 删除单个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int DeleteSingle<T>(T entity) where T : class
        {
            try
            {
                _ctx.Set<T>().Remove(entity);
                return _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                if (_dbTransac != null)
                {
                    _dbTransac.Rollback();
                }
                throw e;
            }
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="resultModel"></param>
        /// <param name="searchModel"></param>
        /// <param name="sql"></param>
        public void GetPagerList(AjaxResultModel resultModel, AjaxModel searchModel, string sql)
        {
            string baseSql = string.Empty;
            string sqlWhere = string.Empty;
            List<DbParameter> paramsList = new List<DbParameter>();
            long limit = searchModel.Page * searchModel.Limit;

            sqlWhere = BuildWhereBySearchModel(searchModel, paramsList);
            baseSql += "select * from (" + sql + ") t3 where 1=1 " + sqlWhere;

            string countSql = "select count(*) from (" + baseSql + ") t ";
            DbParameter[] parameters = paramsList.ToArray();
            resultModel.total = _ctx.Database.SqlQuery<long>(countSql, parameters).FirstOrDefault();
            string pageSql = " select * from ( " + baseSql + "  order by " + searchModel.OrderColunm + " " + (searchModel.OrderDir == "desc" ? "asc" : "desc") + " limit " + limit + ") t order BY " + searchModel.OrderColunm + " " + searchModel.OrderDir + " LIMIT " + (resultModel.total - limit > 0 ? searchModel.Limit : resultModel.total - (searchModel.Limit * (searchModel.Page - 1))) + " ";
            resultModel.data = _ctx.Database.SqlQueryForDataTatable(pageSql, parameters);
            //if (dt != null)
            //{
            //    resultModel.data = dt.ToList<T>();
            //}
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="sql"></param>
        public DataTable GetDataTable(AjaxModel searchModel, string sql)
        {
            string executeSql = string.Empty;
            List<DbParameter> paramsList = new List<DbParameter>();
            string sqlWhere = BuildWhereBySearchModel(searchModel, paramsList);
            executeSql += "select * from (" + sql + ") t3 where 1=1 " + sqlWhere;
            DbParameter[] parameters = paramsList.ToArray();
            DataTable dt = _ctx.Database.SqlQueryForDataTatable(executeSql, parameters);
            return dt;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        public DataTable GetDataTable(string sql, DbParameter[] param = null)
        {
            DataTable dt = _ctx.Database.SqlQueryForDataTatable(sql, param);
            return dt;
        }

        /// <summary>
        /// 构建检索SQL的条件
        /// </summary>
        /// <param name="searchModel">检索model</param>
        /// <param name="paramsList">SQL参数</param>
        /// <returns>返回拼接的SQL条件</returns>
        private static string BuildWhereBySearchModel(AjaxModel searchModel, List<DbParameter> paramsList)
        {
            string sqlWhere = string.Empty;
            List<string> valueList = new List<string>();
            if (searchModel.OrParamsDic != null)
            {
                string[] valueParams;
                foreach (string paramValue in searchModel.OrParamsDic.Keys)
                {
                    //0:字段名称（包含表别名），1：运算操作符，2：参数值
                    valueParams = paramValue.Split(new string[] { "|" }, StringSplitOptions.None);
                    if (valueParams.Length == 3)
                    {
                        if (valueParams[1] == "LIKE")
                        {
                            if (string.IsNullOrEmpty( sqlWhere))
                            {
                                sqlWhere += " AND (" + valueParams[0] + " " + valueParams[1] + " @" + valueParams[2] + " ";
                            }
                            else
                            {
                                sqlWhere += " OR " + valueParams[0] + " " + valueParams[1] + " @" + valueParams[2] + " ";
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(sqlWhere))
                            {
                                sqlWhere += " AND ( " + valueParams[0] + valueParams[1] + " @" + valueParams[2] + " ";
                            }
                            else
                            {
                                sqlWhere += " OR " + valueParams[0] + valueParams[1] + " @" + valueParams[2] + " ";
                            }
                        }
                        if (!valueList.Any(v => v.Equals(valueParams[2])))
                        {
                            //判断参数是否存在，如不存在添加，存在则不添加重复
                            MySqlParameter mySqlParam = new MySqlParameter();
                            mySqlParam.ParameterName = "@" + valueParams[2];
                            if (valueParams[1] == "LIKE")
                            {
                                mySqlParam.Value = "%" + searchModel.OrParamsDic[paramValue] + "%";
                            }
                            else
                            {
                                mySqlParam.Value = searchModel.OrParamsDic[paramValue];
                            }
                            paramsList.Add(mySqlParam);
                            valueList.Add(valueParams[2]);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(sqlWhere)) {
                    sqlWhere += ")";
                }
            }
            if (searchModel.AndParamsDic != null)
            {
                string[] valueParams;
                foreach (string paramValue in searchModel.AndParamsDic.Keys)
                {
                    //0:字段名称（包含表别名），1：运算操作符，2：参数值
                    valueParams = paramValue.Split(new string[] { "|" }, StringSplitOptions.None);
                    if (valueParams.Length == 3)
                    {
                        if (valueParams[1] == "LIKE")
                        {
                            sqlWhere += " AND " + valueParams[0] + " " + valueParams[1] + " @" + valueParams[2] + " ";
                        }
                        else
                        {
                            sqlWhere += " AND " + valueParams[0] + valueParams[1] + " @" + valueParams[2] + " ";
                        }
                        if (!valueList.Any(v => v.Equals(valueParams[2])))
                        {
                            //判断参数是否存在，如不存在添加，存在则不添加重复
                            MySqlParameter mySqlParam = new MySqlParameter();
                            mySqlParam.ParameterName = "@" + valueParams[2];
                            if (valueParams[1] == "LIKE")
                            {
                                mySqlParam.Value = "%" + searchModel.AndParamsDic[paramValue] + "%";
                            }
                            else
                            {
                                mySqlParam.Value = searchModel.AndParamsDic[paramValue];
                            }
                            paramsList.Add(mySqlParam);
                            valueList.Add(valueParams[2]);
                        }
                    }
                }
            }
            return sqlWhere;
        }
    }
}
