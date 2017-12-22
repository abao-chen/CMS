//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/12/22
// 文件说明：用户登录日志实体类
// 
// 
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace CmsEntity
{
    using System;
    using System.Collections.Generic;

    public partial class TB_LoginLog : BaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? UserID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LoginIP { get; set; }

    }
}

