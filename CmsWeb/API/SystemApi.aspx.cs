using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsBAL;
using CmsEntity;

namespace CmsWeb.API
{
    public partial class SystemApi : APIBase
    {
        public DataTablesResultModel<TB_BasicUser> GetPagerList()
        {
            DataTablesResultModel<TB_BasicUser> resultModel = new DataTablesResultModel<TB_BasicUser>();
            SearchModel searchModel = GetSearchParams();
            BasicUserBal userBal = new BasicUserBal();
            string sql = @"select * from tb_basicuser where isdeleted=0 ";
            userBal.GetPagerList(resultModel, searchModel, sql);
            return resultModel;
        }
    }
}