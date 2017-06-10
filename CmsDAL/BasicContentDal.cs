using System;
using System.Collections.Generic;
using System.Linq;
using Entity;

namespace CmsDAL
{
    public class BasicUserDal : BaseDal<tb_basicuser>
    {
        public BasicUserDal(CmsEntities ctx) : base(ctx)
        {
        }

        public void GetContentPageList(DataTablesResultModel<tb_basicuser> resultModel, SearchModel searchModel)
        {
            string sql = "select * from tb_basicuser";
            base.GetPagerList(resultModel, searchModel, sql);
        }
    }
}