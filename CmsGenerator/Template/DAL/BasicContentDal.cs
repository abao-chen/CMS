//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/28
// 文件说明：基础内容数据访问类
// 
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class BasicContentDal : BaseDal<TB_BasicContent>
    {
        public BasicContentDal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}
