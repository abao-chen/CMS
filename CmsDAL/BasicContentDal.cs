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
    }
}