using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsEntity
{
    public class AjaxModel
    {
        public AjaxModel()
        {
            AndParamsDic = new Dictionary<string, string>();
            OrParamsDic = new Dictionary<string, string>();
        }

        public string OrderColunm { get; set; }
        public string OrderDir { get; set; }
        public long Start { get; set; }
        public long Limit { get; set; }
        public long Page { get; set; }
        /// <summary>
        /// 且关系条件
        /// </summary>
        public Dictionary<string, string> AndParamsDic { get; set; }
        /// <summary>
        /// 或关系条件
        /// </summary>
        public Dictionary<string, string> OrParamsDic { get; set; }
    }
}
