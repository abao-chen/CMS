using Entity;
using System;
using CmsDAL;
using System.Collections.Generic;

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

        public List<tb_basiccontent> GetContentList()
        {
            using (var ctx = new CmsEntities())
            {
                return new BasicContentDal(ctx).SelectList();
            }
        }

        public void GetContentPageList(DataTablesResultModel<tb_basiccontent> resultModel, SearchModel searchModel)
        {
            using (var ctx = new CmsEntities())
            {
                new BasicContentDal(ctx).GetContentPageList(resultModel, searchModel);
            }
        }
    }
}