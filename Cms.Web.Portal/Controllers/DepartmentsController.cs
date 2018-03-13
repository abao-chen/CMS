using System.Web.Mvc;

namespace Cms.Web.Portal.Controllers
{
    public class DepartmentsController : BasePortalController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}