using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmsDAL;
using CmsEntity;
using System.Linq.Expressions;
using System.Net.Http;
using CmsCommon;
using CmsUtils;
using System.Web;

namespace CmsBAL
{
    public class BaseBal<T> where T : class
    {
        public TB_BasicUser LoginUserInfo
        {
            get
            {
                if (SessionUtil.GetSession(Constants.SESSION_LOGIN_USERINFO) != null)
                {
                    return SessionUtil.GetSession(Constants.SESSION_LOGIN_USERINFO) as TB_BasicUser;
                }
                return null;
            }
        }

        protected Log Logger;

        public BaseBal()
        {
            Logger = LogFactory.GetLogger(this.GetType());
        }

        public int InsertSingle(T entity)
        {
            using (var ctx = new CmsEntities())
            {
                return new BaseDal<T>(ctx).InsertSingle(entity);
            }
        }

        public int InsertList(List<T> list)
        {
            using (var ctx = new CmsEntities())
            {
                return new BaseDal<T>(ctx).InsertList(list);
            }
        }

        public List<T> SelectList(Expression<Func<T, bool>> where)
        {
            using (var ctx = new CmsEntities())
            {
                return new BaseDal<T>(ctx).SelectList(where);
            }
        }

        public List<T> SelectAllList()
        {
            using (var ctx = new CmsEntities())
            {
                return new BaseDal<T>(ctx).SelectAllList();
            }
        }

        public T SelectSingleById(Expression<Func<T, bool>> where)
        {
            using (var ctx = new CmsEntities())
            {
                return new BaseDal<T>(ctx).SelectSingle(where);
            }
        }

        public int UpdateSingle(T entity)
        {
            using (var ctx = new CmsEntities())
            {
                SetEntityCommProp(entity);
                return new BaseDal<T>(ctx).UpdateSingle(entity);
            }
        }
        

        public int UpdateList(List<T> list)
        {
            using (var ctx = new CmsEntities())
            {
                return new BaseDal<T>(ctx).UpdateList(list);
            }
        }

        public int DeleteSingle(T entity)
        {
            using (var ctx = new CmsEntities())
            {
                return new BaseDal<T>(ctx).DeleteSingle(entity);
            }
        }

        public int DeleteList(List<T> list)
        {
            using (var ctx = new CmsEntities())
            {
                return new BaseDal<T>(ctx).DeleteList(list);
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
            using (var ctx = new CmsEntities())
            {
                new BaseDal<T>(ctx).GetPagerList(resultModel, searchModel, sql);
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="sql"></param>
        /// <returns>返回结果DataTable</returns>
        public DataTable GetDataTable(AjaxModel searchModel, string sql)
        {
            using (var ctx = new CmsEntities())
            {
                return new BaseDal<T>(ctx).GetDataTable(searchModel, sql);
            }
        }

        /// <summary>
        /// 设置公共字段值
        /// </summary>
        /// <param name="entity"></param>
        private void SetEntityCommProp(T entity)
        {
            var fields = entity.GetType().GetProperties();
            foreach (var item in fields)
            {
                if (item.Name == "UpdateUser")
                {
                    item.SetValue(entity, LoginUserInfo.ID);
                }
                else if (item.Name == "UpdateTime")
                {
                    item.SetValue(entity, DateTime.Now);
                }
            }
        }
    }
}
