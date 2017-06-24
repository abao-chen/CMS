
using System.ComponentModel.DataAnnotations.Schema;

namespace CmsEntity
{
    using System;
    using System.Collections.Generic;

    public partial class TB_BasicUser
    {
        public List<TB_Role> RoleList { get; set; }
        public string UserStatusName { get; set; }
        public string UserTypeName { get; set; }
    }
}