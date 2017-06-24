using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsEntity
{
    public class TreeModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public int pId { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }


        /// <summary>
        /// 是否展开
        /// </summary>
        public bool open { get; set; }
    }
}
