//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/07/22
// 文件说明：权限数据访问类
// 
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class AuthorityDal : BaseDal<TB_Authority>
    {
        public AuthorityDal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}
