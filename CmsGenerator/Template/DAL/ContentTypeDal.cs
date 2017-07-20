//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/07/19
// 文件说明：内容类型数据访问类
// 
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class ContentTypeDal : BaseDal<TB_ContentType>
    {
        public ContentTypeDal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}
