using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsEntity
{
    public class DataTablesResultModel<T>
    {
        /// <summary>
        /// 1:成功，2：失败
        /// </summary>
        public int result { get; set; }

        /// <summary>
        /// 列表当前页数据
        /// </summary>
        public List<T> data { get; set; }

        /// <summary>
        /// 列表总条数
        /// </summary>
        public long total { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string erroMsg { get; set; }

    }
}
