﻿using System;
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

namespace Cms.Web.Admin.API
{
    public abstract class BaseApi : Page, IRequiresSessionState
    {
        private List<string> KeysWordList => new List<string>(new[] { "length", "limit", "start", "page", "orderColunm", "orderDir", "keywords", "searchColunms" });

        /// <summary>
        /// 登录用户信息
        /// </summary>
        protected TB_BasicUser LoginUserInfo
        {
            get
            {
                if (SessionUtil.GetSession(Constants.SESSION_LOGIN_USERINFO) != null)
                {
                    return (TB_BasicUser)SessionUtil.GetSession(Constants.SESSION_LOGIN_USERINFO);
                }
                else
                {
                    return null;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string method = HttpContext.Current.Request.Form["method"];
            if (HttpContext.Current.Request.HttpMethod == "POST" && !string.IsNullOrEmpty(method))
            {
                Response.ContentType = "text/plain";
                Response.Charset = "utf-8";
                string[] url = HttpContext.Current.Request.RawUrl.Split(new string[] { "/" }, StringSplitOptions.None);
                Type classType = Assembly.Load(GlobalConfig.Assembly).GetType(GlobalConfig.ApiNamespace + url[url.Length - 1].Split(new string[] { "." }, StringSplitOptions.None)[0]);
                MethodInfo methodInfo = classType.GetMethod(method);
                object methodObj = Activator.CreateInstance(classType);
                object result = methodInfo.Invoke(methodObj, null);
                Response.Write(result.ToJson());
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
            if (!string.IsNullOrEmpty(formParams["searchColunms"]) && !string.IsNullOrEmpty(formParams["keywords"]))
            {
                List<string> searchColunms = formParams["searchColunms"].Split('|').ToList();
                for (int i = 0; i < searchColunms.Count(); i++)
                {
                    string col = searchColunms[i];
                    paramsModel.OrParamsDic.Add(col+"|LIKE|"+col+"_like", formParams["keywords"].Trim());
                }
            }
            foreach (string key in formParams.AllKeys)
            {
                if (!KeysWordList.Any(k => k.Equals(key)) && !string.IsNullOrEmpty(formParams[key].Trim()))
                {
                    paramsModel.AndParamsDic.Add(key, formParams[key].Trim());
                }
            }
            return paramsModel;
        }

        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetRequestParam(string key) {
            if (key.IsEmpty())
            {
                return string.Empty;
            }
            else
            {
                if (HttpContext.Current.Request.Form[key].IsNotEmpty())
                {
                    return HttpContext.Current.Request.Form[key];
                }
                else if (HttpContext.Current.Request.QueryString[key].IsNotEmpty())
                {
                    return HttpContext.Current.Request.QueryString[key];
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (SessionUtil.GetSession(Constants.SESSION_LOGIN_USERINFO) == null)
            {
                Response.ContentType = "text/plain";
                AjaxResultModel resultModel = new AjaxResultModel();
                resultModel.result = 3;
                resultModel.message = "登录已超时，请重新登录！";
                Response.Write(resultModel.ToJson());
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
            foreach (string key in searchModel.AndParamsDic.Keys)
            {
                string[] keys = key.Split(new string[] { "$" }, StringSplitOptions.None);
                if (keys.Length == 3)
                {
                    postParams.Add(keys[2].Replace("txt", string.Empty), searchModel.AndParamsDic[key]);
                }
                else
                {
                    postParams.Add(key, searchModel.AndParamsDic[key]);
                }
            }
        }

        public abstract AjaxResultModel GetPagerList();
        public abstract AjaxResultModel Download();
        public abstract AjaxResultModel DeleteByIds();

    }
}