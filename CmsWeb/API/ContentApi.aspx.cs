using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsBAL;
using CmsCommon;
using CmsEntity;
using Newtonsoft.Json;

namespace CmsWeb.API
{
    public partial class ContentApi : APIBase
    {

        public DataTablesResultModel<TB_BasicContent> GetContentPageList()
        {
            DataTablesResultModel<TB_BasicContent> resultModel = new DataTablesResultModel<TB_BasicContent>();
            SearchModel searchModel = GetSearchParams();
            BasicContentBal bcBal = new BasicContentBal();
            string sql = @"select * from tb_basiccontent where isdeleted=1 ";
            bcBal.GetPagerList(resultModel, searchModel, sql);
            return resultModel;
        }
        
    }
}