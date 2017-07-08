using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsEntity
{
    public partial class TB_Role
    {
        public List<TB_BasicUser> BasicUserList { get; set; }

        public List<TB_Authority> AuthorityList { get; set; }

        public List<TB_RoleAuthority> RoleAuthorityList { get; set; }

    }
}
