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
            SearchModel searchModel = GetPostParams();
            string sql = @"select * from tb_basicuser where isdeleted=0 ";
            new BasicUserBal().GetPagerList(resultModel, searchModel, sql);
            return resultModel;
        }

        public DataTablesResultModel<TB_BasicUser> DeleteUser()
        {
            DataTablesResultModel<TB_BasicUser> resultModel = new DataTablesResultModel<TB_BasicUser>();
            SearchModel searchModel = GetPostParams();
            new BasicUserBal().DeleteUser(resultModel, searchModel);
            return resultModel;
        }
    }
}