//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CmsEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_ContentType
    {
        public int ID { get; set; }
        public string TypeName { get; set; }
        public string TypeAlias { get; set; }
        public Nullable<int> IsUse { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<int> CreateUser { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> UpdateUser { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}
