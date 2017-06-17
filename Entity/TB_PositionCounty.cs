﻿//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/09/18
// 文件说明：地区市数据库实体类
// 
// 
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace CmsEntity
{
    using System;
    using System.Collections.Generic;

    public partial class TB_PositionCounty : BaseEntity
    {
        /// <summary>
        /// 地级市主键ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 地级市id
        /// </summary>
        public long CityID { get; set; }
        /// <summary>
        /// 县级id
        /// </summary>
        public long CountyID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CountyName { get; set; }

    }
}

