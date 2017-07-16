using CmsUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int? IsDeleted { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? UpdateUser { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
