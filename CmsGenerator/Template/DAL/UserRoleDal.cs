//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/07/19
// 文件说明：用户角色关联数据访问类
// 
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class UserRoleDal : BaseDal<TB_UserRole>
    {
        public UserRoleDal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}
