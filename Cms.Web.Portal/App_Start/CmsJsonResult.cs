using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CmsUtils;

namespace Cms.Web.Portal
{
    public class CmsJsonResult : JsonResult
    {
        public CmsJsonResult(object data)
        {
            base.Data = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (string.IsNullOrEmpty(this.ContentType))
            {
                response.ContentType = this.ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }

            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }
            else
            {
                response.ContentEncoding = Encoding.UTF8;
            }

            if (Data != null)
            {
                response.Write(Data.ToJson());
            }
        }
    }
}
