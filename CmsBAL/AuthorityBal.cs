//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/24
// 文件说明：权限业务逻辑类
// 
// 
//------------------------------------------------------------------------------

using CmsEntity;
using System;
using CmsDAL;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using CmsCommon;
using CmsUtils;

namespace CmsBAL
{

    public class AuthorityBal : BaseBal<TB_Authority>
    {
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="resultModel"></param>
        /// <param name="searchModel"></param>
        public void DeleteByIds(AjaxResultModel resultModel, AjaxModel searchModel)
        {

            List<TB_Authority> list = new List<TB_Authority>();
            using (var ctx = new CmsEntities())
            {
                AuthorityDal dal = new AuthorityDal(ctx);
                string[] ids = searchModel.ParamsDic["Id"].Split(new string[] { "," }, StringSplitOptions.None);
                int id = 0;
                foreach (string i in ids)
                {
                    if (int.TryParse(i, out id))
                    {
                        TB_Authority entity = dal.SelectSingle(u => u.ID.Equals(id));
                        if (entity != null)
                        {
                            entity.IsDeleted = 1;
                            list.Add(entity);
                        }
                    }

                }
                int result = dal.UpdateList(list);
                if (result > 0)
                {
                    resultModel.result = 1;
                }
            }
        }

        /// <summary>
        /// 获取权限菜单
        /// </summary>
        /// <returns></returns>
        public List<TB_Authority> GetMenuList()
        {
            StringBuilder selectSql = new StringBuilder();
            selectSql.AppendLine("SELECT");
            selectSql.AppendLine("	*");
            selectSql.AppendLine("FROM");
            selectSql.AppendLine("	TB_Authority a");
            selectSql.AppendLine("WHERE");
            selectSql.AppendLine("	a.IsDeleted = 0");
            selectSql.AppendLine("AND a.IsMenu = 1");
            selectSql.AppendLine("AND EXISTS (");
            selectSql.AppendLine("	SELECT");
            selectSql.AppendLine("		*");
            selectSql.AppendLine("	FROM");
            selectSql.AppendLine("		TB_BasicUser u");
            selectSql.AppendLine("	JOIN TB_UserRole ur ON u.ID = ur.UserID");
            selectSql.AppendLine("	AND ur.IsDeleted = 0");
            selectSql.AppendLine("	JOIN TB_Role r ON ur.RoleID = r.ID");
            selectSql.AppendLine("	AND r.IsDeleted = 0");
            selectSql.AppendLine("	JOIN TB_RoleAuthority ra ON ra.RoleID = r.ID");
            selectSql.AppendLine("	AND ra.IsDeleted = 0");
            selectSql.AppendLine("	WHERE");
            selectSql.AppendLine("		u.IsDeleted = 0");
            selectSql.AppendLine("	AND u.ID = " + LoginUserInfo.ID);
            selectSql.AppendLine("	AND ra.AuthorityID = a.ID");
            selectSql.AppendLine(")");
            return base.GetDataTable(selectSql.ToString()).ToList<TB_Authority>();
        }
    }
}
