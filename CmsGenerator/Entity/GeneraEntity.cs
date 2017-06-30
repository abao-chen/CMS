using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CmsGenerator.Entity
{
    public class GeneraEntity
    {
        public string folderName { get; set; }

        public string tableName { get; set; }

        public string tableComment { get; set; }

        public List<GeneraColunm> columns { get; set; }
    }
}