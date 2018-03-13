//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/24
// 文件说明：系统参数业务逻辑类
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

    public class SysParamsBal : BaseBal<TB_SysParams>
    {
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="resultModel"></param>
        /// <param name="searchModel"></param>
        public void DeleteByIds(AjaxResultModel resultModel, AjaxModel searchModel)
        {

            List<TB_SysParams> list = new List<TB_SysParams>();
            using (var ctx = new CmsEntities())
            {
                SysParamsDal dal = new SysParamsDal(ctx);
                string[] ids = searchModel.AndParamsDic["Id"].Split(new string[] { "," }, StringSplitOptions.None);
                int id = 0;
                foreach (string i in ids)
                {
                    if (int.TryParse(i, out id))
                    {
                        TB_SysParams entity = dal.SelectSingle(u => u.ID.Equals(id));
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
        /// 根据参数编码获取参数值
        /// </summary>
        /// <param name="paramCode"></param>
        /// <returns></returns>
        public string GetParamValue(string paramCode)
        {
            using (var ctx = new CmsEntities())
            {
                SysParamsDal dal = new SysParamsDal(ctx);
                TB_SysParams model = dal.SelectSingle(p => p.ParamName == paramCode);
                if (model != null)
                {
                    return model.ParamValue;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
