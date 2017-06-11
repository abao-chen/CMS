using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsEntity
{
    public class DataTablesResultModel<T>
    {
        public List<T> data { get; set; }

        public long total { get; set; }

    }
}
