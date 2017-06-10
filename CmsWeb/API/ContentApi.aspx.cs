using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsBAL;
using CmsCommon;
using Entity;
using Newtonsoft.Json;

namespace CmsWeb.API
{
    public partial class ContentApi : APIBase
    {

        public DataTablesResultModel<tb_basiccontent> GetContentPageList()
        {
            DataTablesResultModel<tb_basiccontent> resultModel = new DataTablesResultModel<tb_basiccontent>();
            SearchModel searchModel = GetSearchParams();
            BasicContentBal bcBal = new BasicContentBal();
            string sql = @"select * from tb_basiccontent ";
            bcBal.GetPagerList(resultModel, searchModel, sql);
            return resultModel;
        }
        
    }
}