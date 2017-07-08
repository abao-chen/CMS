//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/23
// 文件说明：权限数据访问类
// 
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CmsEntity;
using CmsUtils;

namespace CmsDAL
{
    public class AuthorityDal : BaseDal<TB_Authority>
    {
        public AuthorityDal(CmsEntities ctx) : base(ctx)
        {
        }

        /// <summary>
        /// 查询用户权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<TB_Authority> SelectAuthorityByUserId(int id)
        {
            StringBuilder selectSql = new StringBuilder();
            selectSql.AppendLine("SELECT");
            selectSql.AppendLine("	a.*");
            selectSql.AppendLine("FROM");
            selectSql.AppendLine("	TB_Authority a");
            selectSql.AppendLine("LEFT JOIN TB_RoleAuthority ra ON a.ID = ra.AuthorityID");
            selectSql.AppendLine("LEFT JOIN TB_Role r ON r.ID = ra.RoleID");
            selectSql.AppendLine("AND r.IsDeleted = 0");
            selectSql.AppendLine("LEFT JOIN TB_UserRole ur ON ur.RoleID = r.ID");
            selectSql.AppendLine("LEFT JOIN TB_BasicUser u ON u.ID = ur.UserID");
            selectSql.AppendLine("AND u.IsDeleted = 0");
            selectSql.AppendLine("WHERE");
            selectSql.AppendLine("	u.ID = " + id);
            selectSql.AppendLine("AND a.IsDeleted = 0");
            return _ctx.Database.SqlQueryForDataTatable(selectSql.ToString()).ToList<TB_Authority>();
        }
    }
}
