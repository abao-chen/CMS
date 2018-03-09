//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2018/03/09
// 文件说明：用户基础实体类
// 
// 
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace CmsEntity
{
    using System;
    using System.Collections.Generic;

    public partial class TB_BasicUser : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserAccount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PasswordSalt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserType { get; set; }

    }
}

