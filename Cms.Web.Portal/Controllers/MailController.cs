using System.Web.Mvc;

namespace Cms.Web.Portal.Controllers
{
    public class MailController : BasePortalController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SubmitUserInfo(string userName, string userEmail, string userFax, string message)
        {
            var model = new { id = 1, userName, userEmail, userFax, message };
            return new CmsJsonResult(model);
        }
    }
}