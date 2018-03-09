//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/24
// 文件说明：用户角色关联业务逻辑类
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

    public class UserRoleBal : BaseBal<TB_UserRole>
    {
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="resultModel"></param>
        /// <param name="searchModel"></param>
        public void DeleteByIds(AjaxResultModel resultModel, AjaxModel searchModel)
        {

            List<TB_UserRole> list = new List<TB_UserRole>();
            using (var ctx = new CmsEntities())
            {
                UserRoleDal dal = new UserRoleDal(ctx);
                string[] ids = searchModel.AndParamsDic["Id"].Split(new string[] { "," }, StringSplitOptions.None);
                int id = 0;
                foreach (string i in ids)
                {
                    if (int.TryParse(i, out id))
                    {
                        TB_UserRole entity = dal.SelectSingle(u => u.ID.Equals(id));
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
        /// 根据用户ID删除角色关系表
        /// </summary>
        /// <param name="id"></param>
        public int DeleteListByUserId(int id)
        {
            using (var ctx = new CmsEntities())
            {
                UserRoleDal dal = new UserRoleDal(ctx);
                List<TB_UserRole> userRoleList = dal.SelectList(ur => ur.UserID == id);
                return dal.DeleteList(userRoleList);
            }
        }
    }
}
