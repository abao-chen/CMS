using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmsUtils;
using CmsEntity;

namespace DataFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new CmsEntities())
            {
                List<TB_BasicContent> list = new List<TB_BasicContent>();
                for (int i = 0; i < 1024; i++)
                {
                    TB_BasicContent bcModel = new TB_BasicContent();
                    bcModel.ContentTitle = "Title" + i;
                    bcModel.ContentSubTitle = "SubTitle" + i;
                    list.Add(bcModel);
                }
                ctx.TB_BasicContent.AddRange(list);
                ctx.SaveChanges();
            }
        }
    }
}
