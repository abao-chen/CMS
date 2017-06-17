using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsEntity
{
    public partial class TB_UserRole
    {
        public TB_BasicUser User { get; set; }
        public TB_Role Role { get; set; }
    }
}
