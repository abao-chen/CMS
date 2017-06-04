using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entity;

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
    }
}
