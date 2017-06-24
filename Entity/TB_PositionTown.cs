﻿//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/23
// 文件说明：镇数据库实体类
// 
// 
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace CmsEntity
{
    using System;
    using System.Collections.Generic;

    public partial class TB_PositionTown : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 县级市id
        /// </summary>
        public decimal CountyID { get; set; }
        /// <summary>
        /// 镇id
        /// </summary>
        public decimal TownID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TownName { get; set; }

    }
}

