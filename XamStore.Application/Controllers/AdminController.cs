using System.Web.Mvc;

namespace XamStore.Application.Controllers
{
    public class AdminController : BaseAdminController
    {
        public ActionResult Index()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            return View("~/Views/Admin/Home/Home.cshtml");
        }
    }
}