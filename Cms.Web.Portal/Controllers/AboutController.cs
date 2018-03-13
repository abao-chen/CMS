using System.Web.Mvc;

namespace Cms.Web.Portal.Controllers
{
    public class AboutController : BasePortalController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}