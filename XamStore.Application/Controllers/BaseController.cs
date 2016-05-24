using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XamStore.Domain.Entities.Sistema;
using XamStore.Infrastructure.Context;

namespace XamStore.Application.Controllers
{
    public class BaseController : Controller
    {
        protected Context Context = new Context();

        public BaseController()
        {
        }

        public bool CheckUsuarioAutenticacao()
        {
            var session = Session["autenticacao"] as SessionAutenticacaoClient;
            return session != null;
        }
    }
}