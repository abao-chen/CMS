//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/07/19
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
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 权限类型            1：模块，2：页面，3：按钮
        /// </summary>
        public int? AuthorType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AuthorName { get; set; }
        /// <summary>
        /// 权限标识，为权限按钮使用
        /// </summary>
        public string AuthorFlag { get; set; }
        /// <summary>
        /// 父级权限ID
        /// </summary>
        public int? ParentID { get; set; }
        /// <summary>
        /// 权限全路径
        /// </summary>
        public string FullID { get; set; }
        /// <summary>
        /// 是否为菜单，1：是，0：否
        /// </summary>
        public int? IsMenu { get; set; }
        /// <summary>
        /// 页面URL
        /// </summary>
        public string PageUrl { get; set; }

    }
}

