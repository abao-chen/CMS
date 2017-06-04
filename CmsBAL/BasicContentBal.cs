using Entity;
using System;
using CmsDAL;

namespace CmsBAL
{
    public class BasicContentBal : BaseBal
    {
        public int Insert(tb_basiccontent entity)
        {
            using (var ctx = new CmsEntities())
            {
                return new BasicContentDal(ctx).InsertSingle(entity);
            }

        }
    }
}