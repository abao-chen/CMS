using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsCommon;

namespace TestApplication.API
{
    public partial class Content : APIBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Request.HttpMethod)
            {
                case "POST":
                    RequestPost();
                    break;
                case "GET":
                    break;
                default:
                    break;

            }
        }

        private string RequestPost()
        {
            string result = string.Empty;
            NameValueCollection formParams = Request.Form;

            FormParamsModel paramsModel = new FormParamsModel();
            GetDataTablesDefaultParams(paramsModel, formParams);

            foreach (string key in formParams.AllKeys)
            {
                if (!paramsModel.KeysWordList.Any(k => k.StartsWith(key)))
                {
                    paramsModel.paramsDic.Add(key, formParams[key]);
                }
            }


            return result;
        }

        /// <summary>
        /// 获取DataTables默认参数
        /// </summary>
        private void GetDataTablesDefaultParams(FormParamsModel paramsModel, NameValueCollection formParams)
        {
            int colunmIndex = 0;
            paramsModel.ColunmList = new List<ColunmModel>();
            while (formParams["columns[" + colunmIndex + "][data]"] != null)
            {
                ColunmModel colunmModel = new ColunmModel();
                colunmModel.Data = formParams["columns[" + colunmIndex + "][data]"];
                colunmModel.Name = formParams["columns[" + colunmIndex + "][name]"];
                colunmModel.SearchAble = formParams["columns[" + colunmIndex + "][searchable]"];
                colunmModel.OrderAble = formParams["columns[" + colunmIndex + "][orderable]"];
                colunmModel.SearchValue = formParams["columns[" + colunmIndex + "][search][value]"];
                colunmModel.SearchRegex = formParams["columns[" + colunmIndex + "][search][regex]"];
                paramsModel.ColunmList.Add(colunmModel);
            }
            paramsModel.Draw = formParams["draw"];
            paramsModel.OrderColunm = formParams["order[0][colunm]"];
            paramsModel.OrderDir = formParams["order[0][dir]"];
            paramsModel.Start = long.Parse(formParams["start"]);
            paramsModel.Length = long.Parse(formParams["length"]);
        }
    }

    public class FormParamsModel
    {
        public FormParamsModel()
        {
            KeysWordList = new List<string>();
            KeysWordList.Add("draw");
            KeysWordList.Add("colunm[");
            KeysWordList.Add("start");
            KeysWordList.Add("length");
            KeysWordList.Add("method");
            KeysWordList.Add("search[");
        }

        public readonly List<string> KeysWordList;

        public List<ColunmModel> ColunmList { get; set; }
        public string Draw;
        public string OrderColunm { get; set; }
        public string OrderDir { get; set; }
        public string Method { get; set; }
        public long Start { get; set; }
        public long Length { get; set; }
        public Dictionary<string, string> paramsDic { get; set; }
    }

    public class ColunmModel
    {
        public string Name { get; set; }
        public string Data { get; set; }
        public string SearchAble { get; set; }
        public string OrderAble { get; set; }
        public string SearchValue { get; set; }
        public string SearchRegex { get; set; }
    }
}