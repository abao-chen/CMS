//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/07/22
// 文件说明：广告实体类
// 
// 
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace CmsEntity
{
    using System;
    using System.Collections.Generic;

    public partial class TB_Advertisement : BaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 广告类型ID
        /// </summary>
        public int? AdTypeID { get; set; }
        /// <summary>
        /// 广告名称
        /// </summary>
        public string AdName { get; set; }
        /// <summary>
        /// 广告图片
        /// </summary>
        public string AdDescription { get; set; }
        /// <summary>
        /// 广告链接
        /// </summary>
        public string AdUrl { get; set; }
        /// <summary>
        /// 有效开始时间
        /// </summary>
        public DateTime? ValidStartTime { get; set; }
        /// <summary>
        /// 有效结束时间
        /// </summary>
        public DateTime? ValidEndTime { get; set; }
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

