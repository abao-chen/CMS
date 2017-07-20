//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/07/19
// 文件说明：角色权限关联数据访问类
// 
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class RoleAuthorityDal : BaseDal<TB_RoleAuthority>
    {
        public RoleAuthorityDal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}
