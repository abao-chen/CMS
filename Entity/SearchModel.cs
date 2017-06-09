using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class SearchModel
    {
        public SearchModel()
        {
            ParamsDic = new Dictionary<string, string>();
        }

        public string OrderColunm { get; set; }
        public string OrderDir { get; set; }
        public long Start { get; set; }
        public long Limit { get; set; }
        public long Page { get; set; }
        public Dictionary<string, string> ParamsDic { get; set; }
    }
}
