//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/07/19
// 文件说明：系统参数数据访问类
// 
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class SysParamsDal : BaseDal<TB_SysParams>
    {
        public SysParamsDal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}
