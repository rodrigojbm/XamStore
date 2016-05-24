using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XamStore.Application.Controllers
{
    public class AdminController : BaseAdminController
    {
        public ActionResult Index()
        {
            var isAutenticado = ChecarUsuarioAdminAutenticado();
            if (!isAutenticado)
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            return View("~/Views/Admin/Home/Home.cshtml");
        }
    }
}