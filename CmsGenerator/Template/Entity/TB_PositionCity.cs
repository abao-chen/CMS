//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/24
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
        public int ID { get; set; }
        /// <summary>
        /// 地级市id
        /// </summary>
        public decimal ProvinceId { get; set; }
        /// <summary>
        /// 县级市id
        /// </summary>
        public decimal CityId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CityName { get; set; }

    }
}

