//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/07/08
// 文件说明：用户基础数据访问类
// 
// 
//------------------------------------------------------------------------------

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
