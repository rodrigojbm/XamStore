using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XamStore.Application.Controllers
{
    [RoutePrefix("Admin")]
    public class JogoController : BaseAdminController
    {
        [Route("Jogo")]
        public ActionResult Index()
        {
            return View();
        }
    }
}