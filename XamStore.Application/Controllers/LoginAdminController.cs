using System.Linq;
using System.Web.Mvc;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Domain.Enums;
using XamStore.Infrastructure.Context;
using SessionAutenticacaoAdmin = XamStore.Domain.Entities.Sistema.SessionAutenticacaoAdmin;

namespace XamStore.Application.Controllers
{
    [RoutePrefix("Admin")]
    public class LoginAdminController : BaseAdminController
    {
        private readonly Context _db = new Context();

        [Route("Login")]
        public ActionResult Index()
        {
            return View("~/Views/Admin/Login/Index.cshtml");
        }

        public JsonResult Login(Usuario usuario)
        {
            var usuarioData = _db.Usuario.FirstOrDefault(x => x.Senha == usuario.Senha && x.Login == usuario.Login);

            if (usuarioData == null)
                return Json(new {RedirectUrl = "", message = "Usuário ou senha inválidos!"}, JsonRequestBehavior.AllowGet);

            var sessionAdmin = new SessionAutenticacaoAdmin
            {
                Id = usuarioData.Id,
                Nome = usuarioData.Nome,
                Nivel = (MenuAdminTipoEnum) usuarioData.UsuarioNivel.Id
            };

            Session.Add("autenticacaoAdmin", sessionAdmin);

            return Json(new {RedirectUrl = Url.Action("Index", "Admin"), result = true}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Sair()
        {
            Session.Remove("autenticacaoAdmin");
            return RedirectToAction("Index", "LoginAdmin");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}