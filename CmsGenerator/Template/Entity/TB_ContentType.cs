//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/25
// 文件说明：内容类型实体类
// 
// 
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace CmsEntity
{
    using System;
    using System.Collections.Generic;

    public partial class TB_ContentType : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 类型别名
        /// </summary>
        public string TypeAlias { get; set; }
        /// <summary>
        /// 是否启用            1:启用,0不启用
        /// </summary>
        public int? IsUse { get; set; }

    }
}

