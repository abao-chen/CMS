using System;
using System.Collections.Generic;
using System.Data;
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
                //List<TB_BasicUser> list = new List<TB_BasicUser>();
                //for (int i = 0; i < 1024; i++)
                //{
                //    TB_BasicContent bcModel = new TB_BasicContent();
                //    bcModel.ContentTitle = "Title" + i;
                //    bcModel.ContentSubTitle = "SubTitle" + i;
                //    list.Add(bcModel);
                //}
                //ctx.TB_BasicContent.AddRange(list);
                //ctx.SaveChanges();
                string sql = @"SELECT
	                            u.*, d1.DicName UserStatusName
                            FROM
	                            TB_BasicUser u
                            LEFT JOIN TB_Dictionary d1 ON d1.IsDeleted = 0
                            AND d1.DicTypeCode = 'U02000'
                            AND d1.DicCode = u.UserStatus
                            WHERE
	                            u.IsDeleted = 0";

                List<TB_BasicUser> list = ctx.Database.SqlQueryForDataTatable(sql).ToList<TB_BasicUser>();
            }
        }
    }
}
