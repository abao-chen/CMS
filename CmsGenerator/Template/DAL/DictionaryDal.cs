//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/07/19
// 文件说明：字典数据访问类
// 
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class DictionaryDal : BaseDal<TB_Dictionary>
    {
        public DictionaryDal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}
