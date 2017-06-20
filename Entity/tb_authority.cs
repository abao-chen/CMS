//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/18
// 文件说明：权限实体类
// 
// 
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace CmsEntity
{
    using System;
    using System.Collections.Generic;

    public partial class TB_Authority : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 权限类型            1：模块，2：页面，3：按钮
        /// </summary>
        public int? AuthorType { get; set; }
        /// <summary>
        /// 父级权限ID
        /// </summary>
        public int? ParentID { get; set; }
        /// <summary>
        /// 权限全路径
        /// </summary>
        public string FullID { get; set; }

    }
}

