//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/07/19
// 文件说明：字典类型业务逻辑类
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

    public class DicTypeBal : BaseBal<TB_DicType>
    {
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="resultModel"></param>
        /// <param name="searchModel"></param>
    public void DeleteByIds(AjaxResultModel resultModel, AjaxModel searchModel)
    {

        List<TB_DicType> list = new List<TB_DicType>();
        using (var ctx = new CmsEntities())
        {
            DicTypeDal dal = new DicTypeDal(ctx);
            string[] ids = searchModel.ParamsDic["Id"].Split(new string[] { "," }, StringSplitOptions.None);
            int id = 0;
            foreach (string i in ids)
            {
                if (int.TryParse(i, out id))
                {
                    TB_DicType entity = dal.SelectSingle(u => u.ID.Equals(id));
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
