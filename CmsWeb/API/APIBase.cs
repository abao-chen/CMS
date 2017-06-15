using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using CmsCommon;
using CmsEntity;
using Newtonsoft.Json;

namespace CmsWeb.API
{
    public abstract class APIBase : BasePage
    {
        private List<string> KeysWordList => new List<string>(new[] { "length", "limit", "start", "page", "orderColunm", "orderDir" });

        protected void Page_Load(object sender, EventArgs e)
        {
            string method = HttpContext.Current.Request.Form["method"];
            if (HttpContext.Current.Request.HttpMethod == "POST" && !string.IsNullOrEmpty(method))
            {
                Response.ContentType = "text/plain";
                ContentApi api = new ContentApi();
                string[] url = HttpContext.Current.Request.RawUrl.Split(new string[] { "/" }, StringSplitOptions.None);
                Type classType = Assembly.Load(GlobalConfig.Assembly).GetType(GlobalConfig.ApiNamespace + url[url.Length - 1].Split(new string[] { "." }, StringSplitOptions.None)[0]);
                MethodInfo methodInfo = classType.GetMethod(method);
                object methodObj = Activator.CreateInstance(classType);
                object result = methodInfo.Invoke(methodObj, null);
                //object result = 
                Response.Write(JsonConvert.SerializeObject(result));
                Response.End();
            }
        }


        /// <summary>
        /// 获取前台Ajax的Post参数
        /// </summary>
        protected SearchModel GetPostParams()
        {
            NameValueCollection formParams = HttpContext.Current.Request.Form;
            SearchModel paramsModel = new SearchModel();
            paramsModel.OrderColunm = formParams["orderColunm"];
            paramsModel.OrderDir = formParams["orderDir"];
            paramsModel.Start = formParams["start"] == null ? 0 : long.Parse(formParams["start"]);
            paramsModel.Limit = formParams["limit"] == null ? 0 : long.Parse(formParams["limit"]);
            paramsModel.Page = formParams["page"] == null ? 0 : long.Parse(formParams["page"]); 
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