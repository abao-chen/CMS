//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/25
// 文件说明：组织架构数据访问类
// 
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class OrganizationDal : BaseDal<TB_Organization>
    {
        public OrganizationDal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}
