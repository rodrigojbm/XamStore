using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XamStore.Infrastructure.Context;

namespace XamStore.Application.Controllers
{
    public class JogoController : BaseController
    {
        private readonly Context _db = new Context();
        public ActionResult Index()
        {
            return View();
        }
    }
}