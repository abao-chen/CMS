//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/07/22
// 文件说明：地区市数据库数据访问类
// 
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class PositionCountyDal : BaseDal<TB_PositionCounty>
    {
        public PositionCountyDal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}
