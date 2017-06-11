using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class DicTypeDal : BaseDal<TB_DicType>
    {
        public DicTypeDal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}