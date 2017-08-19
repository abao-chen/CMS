//------------------------------------------------------------------------------
// 
// 制作人：ChenSheng  
// 制作日期：2017/07/29
// 文件说明：基础内容实体类
// 
// 
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace CmsEntity
{
    using System;
    using System.Collections.Generic;

    public partial class TB_BasicContent : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 内容类型
        /// </summary>
        public int? ContentType { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string ContentTitle { get; set; }
        /// <summary>
        /// 子标题
        /// </summary>
        public string ContentSubTitle { get; set; }
        /// <summary>
        /// 封面图
        /// </summary>
        public string CoverPictureUrl { get; set; }
        /// <summary>
        /// 有效开始时间
        /// </summary>
        public DateTime? ValidStartTime { get; set; }
        /// <summary>
        /// 有效结束时间
        /// </summary>
        public DateTime? ValidEndTime { get; set; }
        /// <summary>
        /// 展示顺序
        /// </summary>
        public int? OrderNO { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        public int? PageViewQua { get; set; }
        /// <summary>
        /// 转发量
        /// </summary>
        public int? ForwardQua { get; set; }
        /// <summary>
        /// 点赞量
        /// </summary>
        public int? PointQua { get; set; }
        /// <summary>
        /// 评论量
        /// </summary>
        public int? CommentQua { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public string AttachmentUrl { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

    }
}

