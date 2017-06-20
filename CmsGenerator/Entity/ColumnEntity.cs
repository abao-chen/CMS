using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsGenerator
{
    public class ColumnEntity
    {
        public string COLUMN_NAME { get; set; }
        public string DATA_TYPE { get; set; }
        public string COLUMN_COMMENT { get; set; }
        public string IS_NULLABLE { get; set; }
    }
}
