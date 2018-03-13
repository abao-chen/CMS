using System.Web.Mvc;

namespace Cms.Web.Portal.Controllers
{
    public class HomeController : BasePortalController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
