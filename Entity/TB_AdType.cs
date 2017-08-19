//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/07/29
// 文件说明：广告类型实体类
// 
// 
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace CmsEntity
{
    using System;
    using System.Collections.Generic;

    public partial class TB_AdType : BaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 广告位名称
        /// </summary>
        public string AdTypeName { get; set; }
        /// <summary>
        /// 广告位说明
        /// </summary>
        public string AdTypeDescription { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string AdTypeComment { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int? IsUsing { get; set; }

    }
}

