using System.Linq;
using System.Web.Mvc;
using XamStore.Infrastructure.Context;
using SessionAutenticacaoAdmin = XamStore.Domain.Entities.Sistema.SessionAutenticacaoAdmin;

namespace XamStore.Application.Controllers
{
    public class BaseAdminController : Controller
    {
        private readonly Context _db = new Context();

        public void InitializeMenuAdmin()
        {
            ViewBag.VendaCount = _db.Venda.Count(x => x.IsNova);

            var sessionAdmin = Session["AutenticacaoAdmin"] as SessionAutenticacaoAdmin;
            var usuario = _db.Usuario.FirstOrDefault(x => x.Id == sessionAdmin.Id);
            var menus = usuario?.UsuarioNivel.Id == 1 ? _db.MenuAdmin.ToList() : _db.MenuAdmin.Where(x => x.Tipo == 0).ToList();

            ViewBag.Menus = menus;
        }

        public bool ChecarUsuarioAdminAutenticado()
        {
            var sessionAdmin = (SessionAutenticacaoAdmin) Session?["AutenticacaoAdmin"];
            return sessionAdmin != null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}