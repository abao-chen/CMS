﻿//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/25
// 文件说明：省市县镇村数据实体类
// 
// 
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace CmsEntity
{
    using System;
    using System.Collections.Generic;

    public partial class TB_PositionVillage : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 镇id
        /// </summary>
        public decimal TownID { get; set; }
        /// <summary>
        /// 村id--唯一
        /// </summary>
        public decimal VillageID { get; set; }
        /// <summary>
        /// 村名称
        /// </summary>
        public string VillageName { get; set; }

    }
}
