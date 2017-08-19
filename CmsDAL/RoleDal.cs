//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/23
// 文件说明：角色数据访问类
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
    public class RoleDal : BaseDal<TB_Role>
    {
        public RoleDal(CmsEntities ctx) : base(ctx)
        {
        }

        /// <summary>
        /// 根据用户Id获取角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<TB_Role> SelectRoleByUserId(int id)
        {
            StringBuilder selectSql = new StringBuilder();
            selectSql.AppendLine("SELECT");
            selectSql.AppendLine("	r.*");
            selectSql.AppendLine("FROM");
            selectSql.AppendLine("	TB_Role r");
            selectSql.AppendLine("LEFT JOIN TB_UserRole ur ON r.ID = ur.RoleID");
            selectSql.AppendLine("LEFT JOIN TB_BasicUser u ON ur.UserID = u.ID");
            selectSql.AppendLine("AND u.IsDeleted = 0");
            selectSql.AppendLine("WHERE");
            selectSql.AppendLine("	u.ID = " + id);
            selectSql.AppendLine("AND r.IsDeleted = 0");
            return GetDataTable(selectSql.ToString()).ToList<TB_Role>();
        }
    }
}
