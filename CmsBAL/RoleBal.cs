//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/24
// 文件说明：角色业务逻辑类
// 
// 
//------------------------------------------------------------------------------

using CmsEntity;
using System;
using CmsDAL;
using System.Collections.Generic;
using CmsCommon;
using CmsUtils;

namespace CmsBAL
{

    public class RoleBal : BaseBal<TB_Role>
    {
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="resultModel"></param>
        /// <param name="searchModel"></param>
        public void DeleteByIds(AjaxResultModel resultModel, AjaxModel searchModel)
        {

            List<TB_Role> list = new List<TB_Role>();
            using (var ctx = new CmsEntities())
            {
                RoleDal dal = new RoleDal(ctx);
                string[] ids = searchModel.AndParamsDic["Id"].Split(new string[] { "," }, StringSplitOptions.None);
                int id = 0;
                foreach (string i in ids)
                {
                    if (int.TryParse(i, out id))
                    {
                        TB_Role entity = dal.SelectSingle(u => u.ID.Equals(id));
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
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="roleAuthorities"></param>
        public void UpdateSingle(TB_Role entity, List<TB_RoleAuthority> roleAuthorities)
        {
            using (var ctx = new CmsEntities())
            {
                List<TB_RoleAuthority> orgAuthorities = new RoleAuthorityDal(ctx).SelectList(r => r.RoleID==entity.ID);
                new RoleAuthorityDal(ctx).DeleteList(orgAuthorities);
                new RoleDal(ctx).UpdateSingle(entity);
                new RoleAuthorityDal(ctx).InsertList(roleAuthorities);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="roleAuthorities"></param>
        public void InsertSingle(TB_Role entity, List<TB_RoleAuthority> roleAuthorities)
        {
            using (var ctx = new CmsEntities())
            {
                new RoleDal(ctx).InsertSingle(entity);
                foreach (TB_RoleAuthority roleAuthority in roleAuthorities)
                {
                    roleAuthority.RoleID = entity.ID;
                }
                new RoleAuthorityDal(ctx).InsertList(roleAuthorities);
            }
        }
    }
}
