using System;
using System.Collections.Generic;
using System.Linq;
using Entity;

namespace CmsDAL
{
    public class BasicContentDal : BaseDal<tb_basiccontent>
    {
        public BasicContentDal(CmsEntities ctx) : base(ctx)
        {
        }

        public void GetContentPageList(DataTablesResultModel<tb_basiccontent> resultModel, SearchModel searchModel)
        {
            string sql = "select * from tb_basiccontent";
            base.GetPagerList(resultModel, searchModel, sql);
        }
    }
}