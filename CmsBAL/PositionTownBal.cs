﻿//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/24
// 文件说明：镇数据库业务逻辑类
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

    public class PositionTownBal : BaseBal<TB_PositionTown>
    {
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="resultModel"></param>
        /// <param name="searchModel"></param>
    public void DeleteByIds(AjaxResultModel resultModel, AjaxModel searchModel)
    {

        List<TB_PositionTown> list = new List<TB_PositionTown>();
        using (var ctx = new CmsEntities())
        {
            PositionTownDal dal = new PositionTownDal(ctx);
            string[] ids = searchModel.AndParamsDic["Id"].Split(new string[] { "," }, StringSplitOptions.None);
            int id = 0;
            foreach (string i in ids)
            {
                if (int.TryParse(i, out id))
                {
                    TB_PositionTown entity = dal.SelectSingle(u => u.ID.Equals(id));
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
