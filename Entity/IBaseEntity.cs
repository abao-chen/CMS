using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsEntity
{
    interface IBaseEntity
    {
        Nullable<int> IsDeleted { get; set; }
        Nullable<int> CreateUser { get; set; }
        Nullable<System.DateTime> CreateTime { get; set; }
        Nullable<int> UpdateUser { get; set; }
        Nullable<System.DateTime> UpdateTime { get; set; }
    }
}
