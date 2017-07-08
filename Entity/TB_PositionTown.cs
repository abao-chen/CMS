//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/07/08
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
        public string CountyID { get; set; }
        /// <summary>
        /// 镇id
        /// </summary>
        public string TownID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TownName { get; set; }

    }
}

