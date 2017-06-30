//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/28
// 文件说明：省份数据库实体类
// 
// 
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace CmsEntity
{
    using System;
    using System.Collections.Generic;

    public partial class TB_PositionProvice : BaseEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 省份id、省份编号
        /// </summary>
        public string ProviceID { get; set; }
        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProviceName { get; set; }

    }
}

