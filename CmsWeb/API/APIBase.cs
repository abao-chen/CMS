using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using CmsCommon;
using CmsEntity;
using CmsUtils;
using Newtonsoft.Json;

namespace CmsWeb.API
{
    public abstract class APIBase : Page, IRequiresSessionState
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
                Response.Write(JsonConvert.SerializeObject(result));
                Response.End();
            }
        }

        /// <summary>
        /// 获取前台Ajax的Post参数
        /// </summary>
        protected AjaxModel GetPostParams()
        {
            NameValueCollection formParams = HttpContext.Current.Request.Form;
            AjaxModel paramsModel = new AjaxModel();
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

        protected override void OnInit(EventArgs e)
        {
            if (SessionUtil.GetSession(Constants.SESSION_LOGIN_USERINFO) == null)
            {
                Response.ContentType = "text/plain";
                AjaxResultModel resultModel = new AjaxResultModel();
                resultModel.result = 3;
                resultModel.message = "登录已超时，请重新登录！";
                Response.Write(JsonConvert.SerializeObject(resultModel));
                Response.End();
            }
        }


        /// <summary>
        /// 获取验证的参数
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="postParams"></param>
        protected static void GetValidateParams(AjaxModel searchModel, Dictionary<string, string> postParams)
        {
            foreach (string key in searchModel.ParamsDic.Keys)
            {
                string[] keys = key.Split(new string[] { "$" }, StringSplitOptions.None);
                if (keys.Length == 3)
                {
                    postParams.Add(keys[2].Replace("txt", string.Empty), searchModel.ParamsDic[key]);
                }
                else
                {
                    postParams.Add(key, searchModel.ParamsDic[key]);
                }
            }
        }
    }
}