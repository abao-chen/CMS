//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/09/18
// 文件说明：组织架构实体类
// 
// 
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace CmsEntity
{
    using System;
    using System.Collections.Generic;

    public partial class TB_Organization : BaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 父级组织ID
        /// </summary>
        public int? ParentID { get; set; }
        /// <summary>
        /// 组织全路径
        /// </summary>
        public string FullID { get; set; }

    }
}

