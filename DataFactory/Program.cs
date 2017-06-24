using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CmsUtils;
using CmsEntity;

namespace DataFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateData();
        }

        private static void CreateData()
        {
            using (var ctx = new CmsEntities())
            {
                List<TB_SysParams> list = new List<TB_SysParams>();
                for (int i = 0; i < 100; i++)
                {
                    TB_SysParams bcModel = new TB_SysParams();
                    bcModel.ParamName = "Name" + i;
                    bcModel.ParamValue = "Value" + i;
                    bcModel.ParamDesc = "Desc" + i;
                    list.Add(bcModel);
                }
                ctx.TB_SysParams.AddRange(list);
                ctx.SaveChanges();
            }
        }
    }
}
