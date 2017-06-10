using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsBAL;
using Entity;

namespace CmsWeb.API
{
    public partial class SystemApi : APIBase
    {
        public DataTablesResultModel<tb_basicuser> GetUserPageList()
        {
            DataTablesResultModel<tb_basicuser> resultModel = new DataTablesResultModel<tb_basicuser>();
            SearchModel searchModel = GetSearchParams();
            BasicUserBal userBal = new BasicUserBal();
            string sql = @"select * from tb_basicuser ";
            userBal.GetPagerList(resultModel, searchModel, sql);
            return resultModel;
        }
    }
}