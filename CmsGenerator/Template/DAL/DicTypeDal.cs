//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/24
// 文件说明：字典类型数据访问类
// 
// 
//------------------------------------------------------------------------------

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
