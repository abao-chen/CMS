using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BaseEntity
    {
        public virtual tb_basicuser CreateBasicUser { get; set; }
        public virtual tb_basicuser UpdateBasicUser { get; set; }
    }
}
