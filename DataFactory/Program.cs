using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace DataFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new CmsEntities())
            {
                List<tb_basiccontent> list =new List<tb_basiccontent>();
                for (int i = 1001; i <= 1006; i++)
                {
                    tb_basiccontent bcModel =  new tb_basiccontent();
                    bcModel.ContentTitle = "Title" + i;
                    bcModel.ContentSubTitle = "SubTitle" + i;
                    list.Add(bcModel);
                }
                ctx.tb_basiccontent.AddRange(list);
                ctx.SaveChanges();
            }
        }
    }
}
