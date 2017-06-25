//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/25
// 文件说明：县级市数据库数据访问类
// 
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class PositionCityDal : BaseDal<TB_PositionCity>
    {
        public PositionCityDal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}
