//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/09/18
// 文件说明：县级市数据库实体类
// 
// 
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace CmsEntity
{
    using System;
    using System.Collections.Generic;

    public partial class TB_PositionCity : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 地级市id
        /// </summary>
        public int province_id { get; set; }
        /// <summary>
        /// 县级市id
        /// </summary>
        public long city_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string city_name { get; set; }

    }
}

