﻿//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/07/22
// 文件说明：角色权限关联业务逻辑类
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

    public class RoleAuthorityBal : BaseBal<TB_RoleAuthority>
    {
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="resultModel"></param>
        /// <param name="searchModel"></param>
    public void DeleteByIds(AjaxResultModel resultModel, AjaxModel searchModel)
    {

        List<TB_RoleAuthority> list = new List<TB_RoleAuthority>();
        using (var ctx = new CmsEntities())
        {
            RoleAuthorityDal dal = new RoleAuthorityDal(ctx);
            string[] ids = searchModel.ParamsDic["Id"].Split(new string[] { "," }, StringSplitOptions.None);
            int id = 0;
            foreach (string i in ids)
            {
                if (int.TryParse(i, out id))
                {
                    TB_RoleAuthority entity = dal.SelectSingle(u => u.ID.Equals(id));
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
}
}
