using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmsDAL;
using Entity;

namespace CmsBAL
{
    public class BaseBal<T> where T : class
    {
        public int Insert(T entity)
        {
            using (var ctx = new CmsEntities())
            {
                return new BaseDal<T>(ctx).InsertSingle(entity);
            }

        }

        public List<T> GetContentList()
        {
            using (var ctx = new CmsEntities())
            {
                return new BaseDal<T>(ctx).SelectList();
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
