﻿//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/09/18
// 文件说明：角色数据访问类
// 
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class RoleDal : BaseDal<TB_Role>
    {
        public RoleDal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}
