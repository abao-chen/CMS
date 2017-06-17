//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/09/18
// 文件说明：字典实体类
// 
// 
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace CmsEntity
{
    using System;
    using System.Collections.Generic;

    public partial class TB_Dictionary : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 字典类型编码
        /// </summary>
        public string DicTypeCode { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        public string DicName { get; set; }
        /// <summary>
        /// 字典编码
        /// </summary>
        public string DicCode { get; set; }
        /// <summary>
        /// 是否启用            1：启用，0：未启用
        /// </summary>
        public int? IsUsing { get; set; }

    }
}

