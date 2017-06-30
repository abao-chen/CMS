using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CmsGenerator.Entity
{
    public class GeneraColunm
    {
        public string colCode { get; set; }
        public string colComment { get; set; }
        public string dicType { get; set; }
        public string validate { get; set; }
        public int controlType { get; set; }
        public int isEdit { get; set; }
        public int isSelect { get; set; }
        public int isShowList { get; set; }
    }
}