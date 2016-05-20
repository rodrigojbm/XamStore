using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XamStore.Infrastructure.Context;

namespace XamStore.Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _db = new Context();

        [Route("index")]
        public ActionResult Index()
        {
            ViewBag.Pessoa = _db.Pessoa;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}