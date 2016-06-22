using System.Web.Mvc;
using XamStore.Infrastructure.Context;
using SessionAutenticacaoClient = XamStore.Domain.Entities.Sistema.SessionAutenticacaoClient;

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