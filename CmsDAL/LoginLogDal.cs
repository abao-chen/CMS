//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/12/22
// 文件说明：用户登录日志数据访问类
// 
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class LoginLogDal : BaseDal<TB_LoginLog>
    {
        public LoginLogDal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}
