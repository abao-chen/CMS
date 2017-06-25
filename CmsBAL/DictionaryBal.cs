using CmsEntity;
using System;
using CmsDAL;
using System.Collections.Generic;
using CmsUtils;

namespace CmsBAL
{
    public class DictionaryBal : BaseBal<TB_Dictionary>
    {
        public List<ItemList> GetDictionaryList(string dicType)
        {
            using (var ctx = new CmsEntities())
            {
                return new DictionaryDal(ctx).GetDictionaryList(dicType);
            }

        }

        public void DeleteByIds(AjaxResultModel resultModel, AjaxModel searchModel)
        {
            List<TB_Dictionary> userList = new List<TB_Dictionary>();
            using (var ctx = new CmsEntities())
            {
                DictionaryDal dal = new DictionaryDal(ctx);
                string[] ids = searchModel.ParamsDic["Id"].Split(new string[] { "," }, StringSplitOptions.None);
                int iId;
                foreach (string id in ids)
                {
                    if (int.TryParse(id, out iId))
                    {
                        TB_Dictionary user = dal.SelectSingle(u => u.ID.Equals(iId));
                        if (user != null)
                        {
                            user.IsDeleted = 1;
                            userList.Add(user);
                        }
                    }

                }
                int result = dal.UpdateList(userList);
                if (result > 0)
                {
                    resultModel.result = 1;
                }
            }
        }
    }
}