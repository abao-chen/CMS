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
    }
}