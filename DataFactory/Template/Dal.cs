//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：#Date#
// 文件说明：#CnFileName#数据访问类
// 
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class #ClassName#Dal : BaseDal<#TableName#>
    {
        public #ClassName#Dal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}