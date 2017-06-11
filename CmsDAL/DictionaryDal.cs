using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;
using CmsUtils;

namespace CmsDAL
{
    public class DictionaryDal : BaseDal<TB_Dictionary>
    {
        public DictionaryDal(CmsEntities ctx) : base(ctx)
        {
        }

        /// <summary>
        /// 获取字典集合下拉框
        /// </summary>
        /// <param name="dicType"></param>
        /// <returns></returns>
        public List<ItemList> GetDictionaryList(string dicType)
        {
            List<TB_Dictionary> dicList = _ctx.TB_Dictionary.Where(d => d.IsDeleted == 0 && d.DicTypeCode.Equals(dicType)).ToList();
            List<ItemList> itemList = new List<ItemList>();
            foreach (TB_Dictionary model in dicList)
            {
                itemList.Add(new ItemList(model.DicName, model.DicCode));
            }
            return itemList;
        }
    }
}