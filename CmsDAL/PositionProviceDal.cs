//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/09/18
// 文件说明：省份数据库数据访问类
// 
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class PositionProviceDal : BaseDal<TB_PositionProvice>
    {
        public PositionProviceDal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}
