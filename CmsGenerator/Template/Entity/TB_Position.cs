//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/06/24
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
        public decimal ProvinceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal CityID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal CountyID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CountyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal TownID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TownName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal VillageID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string VillageName { get; set; }

    }
}

