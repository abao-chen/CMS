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

namespace TestApplication.API
{
    public partial class Content : APIBase
    {
        private List<string> KeysWordList
        {
            get
            {
                return new List<string>(new[] { "length", "limit", "start", "page", "orderColunm", "orderDir" });
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Request.HttpMethod)
            {
                case "POST":
                    break;
                case "GET":
                    break;
                default:
                    break;

            }
            Response.ContentType = "text/plain";
            Response.Write(JsonConvert.SerializeObject(GetContentPageList()));
            Response.End();
        }

        private DataTablesResultModel<tb_basiccontent> GetContentList()
        {
            DataTablesResultModel<tb_basiccontent> resultModel = new DataTablesResultModel<tb_basiccontent>();
            BasicContentBal bcBal = new BasicContentBal();
            resultModel.data = bcBal.GetContentList();
            return resultModel;
        }

        private DataTablesResultModel<tb_basiccontent> GetContentPageList()
        {
            DataTablesResultModel<tb_basiccontent> resultModel = new DataTablesResultModel<tb_basiccontent>();
            SearchModel searchModel = GetSearchParams(Request.Form);
            BasicContentBal bcBal = new BasicContentBal();
            bcBal.GetContentPageList(resultModel, searchModel);
            return resultModel;
        }

        private string RequestPost()
        {
            string result = string.Empty;
            NameValueCollection formParams = Request.Form;
            SearchModel paramsModel = GetSearchParams(formParams);
            return result;
        }

        /// <summary>
        /// 获取DataTables默认参数
        /// </summary>
        private SearchModel GetSearchParams(NameValueCollection formParams)
        {
            SearchModel paramsModel = new SearchModel();
            paramsModel.OrderColunm = formParams["orderColunm"];
            paramsModel.OrderDir = formParams["orderDir"];
            paramsModel.Start = long.Parse(formParams["start"]);
            paramsModel.Limit = long.Parse(formParams["limit"]);
            paramsModel.Page = long.Parse(formParams["page"]);
            foreach (string key in formParams.AllKeys)
            {
                if (!KeysWordList.Any(k => k.Equals(key)))
                {
                    paramsModel.ParamsDic.Add(key, formParams[key].Trim());
                }
            }
            return paramsModel;
        }
    }
}