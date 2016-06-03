using System.Web.Mvc;

namespace XamStore.Application.Controllers
{
    public class SobreController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Menus = Context.Menu;
            return View();
        }
    }
}