//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/12/22
// 文件说明：角色权限关联实体类
// 
// 
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace CmsEntity
{
    using System;
    using System.Collections.Generic;

    public partial class TB_RoleAuthority : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int? RoleID { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public int? AuthorityID { get; set; }

    }
}

