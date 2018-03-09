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
    public partial class ContentApi : BaseApi
    {
        public override AjaxResultModel DeleteByIds()
        {
            throw new NotImplementedException();
        }

        public override AjaxResultModel Download()
        {
            throw new NotImplementedException();
        }

        public override AjaxResultModel GetPagerList()
        {
            AjaxResultModel resultModel = new AjaxResultModel();
            AjaxModel searchModel = GetPostParams();
            BasicContentBal bcBal = new BasicContentBal();
            string sql = @"select * from tb_basiccontent where isdeleted=0 ";
            bcBal.GetPagerList(resultModel, searchModel, sql);
            return resultModel;
        }
    }
}