using System.Web.Mvc;

namespace Cms.Web.Portal.Controllers
{
    public class GalleryController : BasePortalController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}