﻿using System;
using System.Collections.Generic;
using System.Linq;
using CmsEntity;

namespace CmsDAL
{
    public class BasicContentDal : BaseDal<TB_BasicContent>
    {
        public BasicContentDal(CmsEntities ctx) : base(ctx)
        {
        }
    }
}