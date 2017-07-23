//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/07/22
// 文件说明：用户组织关联数据访问类
// 
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class UserOrganizationDal : BaseDal<TB_UserOrganization>
    {
        public UserOrganizationDal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}
