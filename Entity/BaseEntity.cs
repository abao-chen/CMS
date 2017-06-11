using CmsUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmsCommon;

namespace CmsEntity
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            this.IsDeleted = 0;
            this.CreateTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
            if (SessionUtil.GetSession(Constants.SESSION_LOGIN_USERINFO) != null)
            {
                this.CreateUser = this.UpdateUser = ((TB_BasicUser)SessionUtil.GetSession(Constants.SESSION_LOGIN_USERINFO)).ID;
            }
            else
            {
                this.CreateUser = null;
                this.UpdateUser = null;
            }
        }

        public Nullable<int> IsDeleted { get; set; }
        public Nullable<int> CreateUser { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> UpdateUser { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }

        public virtual TB_BasicUser CreateBasicUser { get; set; }
        public virtual TB_BasicUser UpdateBasicUser { get; set; }
    }
}
