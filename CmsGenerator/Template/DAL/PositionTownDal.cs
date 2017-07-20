//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/07/19
// 文件说明：镇数据库数据访问类
// 
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class PositionTownDal : BaseDal<TB_PositionTown>
    {
        public PositionTownDal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}
