using System.Web.Mvc;
using XamStore.Domain.Entities.Sistema;
using XamStore.Infrastructure.Context;

namespace XamStore.Application.Controllers
{
    public abstract class BaseController : Controller
    {
        protected Context Context = new Context();

        protected BaseController()
        {
        }

        public bool ChecarUsuarioAutenticado()
        {
            var session = Session["Autenticacao"] as SessionAutenticacaoClient;
            return session != null;
        }
    }
}