using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmsUtils;
using CmsEntity;

namespace DataFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new CmsEntities())
            {
                List<TB_BasicUser> list = new List<TB_BasicUser>();
                for (int i = 0; i < 1024; i++)
                {
                    TB_BasicUser bcModel = new TB_BasicUser();
                    bcModel.UserAccount = SecurityUtil.RandomCode(3, 6);
                    bcModel.UserName = SecurityUtil.RandomCode(2, 4);
                    bcModel.PasswordSalt = SecurityUtil.RandomCode(3, 10);
                    bcModel.UserPassword = SecurityUtil.Md5Encrypt64("123456" + bcModel.PasswordSalt);
                    bcModel.UserStatus = "1";
                    bcModel.UserType = "1";
                    bcModel.IsDeleted = 0;
                    list.Add(bcModel);
                }
                ctx.TB_BasicUser.AddRange(list);
                ctx.SaveChanges();
            }
        }
    }
}
