using System;
using System.Collections.Generic;
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
                if (SessionUtil.GetSession(Constants.SESSION_LOGIN_USERINFO) != null && LoginUserInfo == null)
                {
                    return SessionUtil.GetSession(Constants.SESSION_LOGIN_USERINFO) as TB_BasicUser;
                }
                return null;
            }
        }

        public BaseBal()
        {

            
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

        public void GetPagerList(DataTablesResultModel<T> resultModel, SearchModel searchModel, string sql)
        {
            using (var ctx = new CmsEntities())
            {
                new BaseDal<T>(ctx).GetPagerList(resultModel, searchModel, sql);
            }
        }
    }
}
