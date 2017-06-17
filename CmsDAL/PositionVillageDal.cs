//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/09/18
// 文件说明：省市县镇村数据数据访问类
// 
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class PositionVillageDal : BaseDal<TB_PositionVillage>
    {
        public PositionVillageDal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}
