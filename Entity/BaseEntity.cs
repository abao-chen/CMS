using CmsUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CmsEntity
{
    public class BaseEntity
    {
        private const string SessionKey_LoginUser = "LoginUserInfo";

        private TB_BasicUser LoginUserInfo
        {
            get
            {
                if (SessionUtil.GetSession(SessionKey_LoginUser) != null)
                {
                    return (TB_BasicUser)SessionUtil.GetSession(SessionKey_LoginUser);
                }
                else
                {
                    return null;
                }
            }
        }

        public BaseEntity()
        {
            this.IsDeleted = 0;
            this.CreateTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
            if (LoginUserInfo != null)
            {
                this.CreateUser = this.UpdateUser = LoginUserInfo.ID;
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
