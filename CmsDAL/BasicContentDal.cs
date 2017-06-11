using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class BasicUserDal : BaseDal<TB_BasicUser>
    {
        public BasicUserDal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}