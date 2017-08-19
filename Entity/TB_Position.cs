//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/07/29
// 文件说明：省市县镇村数据实体类
// 
// 
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace CmsEntity
{
    using System;
    using System.Collections.Generic;

    public partial class TB_Position : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProvinceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CityID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CountyID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CountyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TownID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TownName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string VillageID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string VillageName { get; set; }

    }
}

