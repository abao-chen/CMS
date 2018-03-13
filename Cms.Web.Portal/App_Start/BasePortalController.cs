using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CmsBAL;
using CmsUtils;

namespace Cms.Web.Portal
{
    public class BasePortalController : Controller
    {
        protected override HttpNotFoundResult HttpNotFound(string statusDescription)
        {
            return base.HttpNotFound(statusDescription);
        }

        protected override void Initialize(RequestContext requestContext)
        {
            string title = new SysParamsBal().GetParamValue("SP_Seo_0001");
            string keyWords = new SysParamsBal().GetParamValue("SP_Seo_0002");
            string description = new SysParamsBal().GetParamValue("SP_Seo_0003");
            if (title.IsNotEmpty())
            {
                ViewData["Title"] = title;
            }
            if (title.IsNotEmpty())
            {
                ViewData["KeyWords"] = keyWords;
            }
            if (title.IsNotEmpty())
            {
                ViewData["Description"] = description;
            }
            base.Initialize(requestContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }
    }
}