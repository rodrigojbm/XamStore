using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Domain.Entities.Sistema;
using XamStore.Domain.Enums;
using XamStore.Infrastructure.Context;
using SessionAutenticacaoClient = XamStore.Domain.Entities.Sistema.SessionAutenticacaoClient;

namespace XamStore.Application.Controllers
{
    public class LoginController : BaseController
    {
        private readonly Context _db = new Context();

        public ActionResult Index(RouteValueDictionary routeValue = null)
        {
            ViewBag.Menus = Context.Menu;
            ViewBag.Pessoa = Context.Pessoa;

            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Entrar([Bind(Include = "Senha, EmailAutenticacao")] Pessoa pessoa, string actionRedirect, string controllerRedirect)
        {
            var pessoaResult = Context.Pessoa.FirstOrDefault(x => x.Senha == pessoa.Senha && x.Email == pessoa.EmailAutenticacao);
            if (pessoaResult == null)
                return Json(new {RedirectUrl = "", result = "Usuário ou senha inválidos."}, JsonRequestBehavior.AllowGet);

            var session = new SessionAutenticacaoClient
            {
                Id = pessoaResult.Id.ToString(),
                Email = pessoaResult.EmailAutenticacao,
                Nome = pessoaResult.Nome,
                AutenticacaoTipo = AutenticacaoTipoEnum.Sistema
            };

            Session.Add("Autenticacao", session);
            return Json( actionRedirect != null
                        ? new {RedirectUrl = Url.Action(actionRedirect, controllerRedirect), result = true}
                        : new {RedirectUrl = Url.Action("Index", "Home"), result = true}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Facebook(Pessoa pessoa)
        {
            try
            {
                ViewBag.Menus = Context.Menu;
                if (string.IsNullOrEmpty(pessoa.FacebookId))
                    return Json(new {RedirectUrl = Url.Action("Index", "Login")}, JsonRequestBehavior.AllowGet);
                if (Session["Autenticacao"] != null)
                    return Json(new {RedirectUrl = Url.Action("Index", "Home")}, JsonRequestBehavior.AllowGet);

                var session = new SessionAutenticacaoClient
                {
                    Id = pessoa.FacebookId,
                    Email = pessoa.Email,
                    Nome = pessoa.Nome,
                    AutenticacaoTipo = AutenticacaoTipoEnum.Facebook
                };

                var pessoaFacebook = _db.Pessoa.FirstOrDefault(x => x.FacebookId == session.Id);
                if (pessoaFacebook == null)
                {
                    pessoa.SexoTipo = SexoTipoEnum.Masculino;
                    pessoa.Senha = "senha_facebook";
                    pessoa.PessoaTipo = PessoaTipoEnum.Fisica;
                    pessoa.EmailAutenticacao = pessoa.Email;
                    pessoa.ConfirmaSenha = pessoa.Senha;

                    _db.Pessoa.Add(pessoa);
                    _db.SaveChanges();
                }

                Session.Add("Autenticacao", session);
                return Json(new {RedirectUrl = Url.Action("Index", "Home")}, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Google(Pessoa pessoa)
        {
            try
            {
                ViewBag.Menus = Context.Menu;

                if (string.IsNullOrEmpty(pessoa.GoogleId))
                    return Json(new { RedirectUrl = Url.Action("Index", "Login") }, JsonRequestBehavior.AllowGet);

                if (Session["Autenticacao"] != null)
                    return Json(new { RedirectUrl = Url.Action("Index", "Home") }, JsonRequestBehavior.AllowGet);

                var session = new SessionAutenticacaoClient
                {
                    Id = pessoa.GoogleId,
                    Email = pessoa.Email,
                    Nome = pessoa.Nome,
                    AutenticacaoTipo = AutenticacaoTipoEnum.Google
                };

                var pessoaGoogle = _db.Pessoa.FirstOrDefault(x => x.GoogleId == session.Id);

                if (pessoaGoogle == null)
                {
                    var saida = pessoa.Nome.Split(' ');
                    pessoa.Sobrenome = saida[1];

                    pessoa.SexoTipo = SexoTipoEnum.Masculino;
                    pessoa.Senha = "senha_google";
                    pessoa.PessoaTipo = PessoaTipoEnum.Fisica;
                    pessoa.EmailAutenticacao = pessoa.Email;
                    pessoa.ConfirmaSenha = pessoa.Senha;

                    _db.Pessoa.Add(pessoa);
                    _db.SaveChanges();
                }

                Session.Add("Autenticacao", session);

                return Json(new { RedirectUrl = Url.Action("Index", "Home") }, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException)
            {
                throw;
            }
        }

        public ActionResult Sair()
        {
            Session.Remove("Autenticacao");
            ViewBag.Pessoa = null;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registro()
        {
            ViewBag.Menus = Context.Menu;
            return View("Registrar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Registrar([Bind(Include = "Nome, Sobrenome, DataNascimento, Rg, Cpf, SexoTipo, Email, Senha, PessoaTipo")] Pessoa pessoa)
        {
            ViewBag.Menus = _db.Menu;
            ViewBag.Produtos = _db.Slide;

            ModelState.Remove("EmailAutenticacao");

            //if (!ModelState.IsValid)
            //    return Json(new { RedirectUrl = Url.Action("Registro", "Login") }, JsonRequestBehavior.AllowGet);

            pessoa.EmailAutenticacao = pessoa.Email;
            pessoa.DataNascimento = pessoa.DataNascimento.Date;

            _db.Pessoa.Add(pessoa);

            try
            {
                _db.SaveChanges();
            }
            catch (DbEntityValidationException)
            {
                throw;
            }

            var session = new SessionAutenticacaoClient
            {
                Id = pessoa.Id.ToString(),
                Email = pessoa.Email,
                Nome = pessoa.Nome,
                AutenticacaoTipo = AutenticacaoTipoEnum.Sistema
            };

            Session.Add("Autenticacao", session);

            return Json(new { RedirectUrl = Url.Action("Index", "Home") }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EmailExists(string email)
        {
            var pessoa = _db.Pessoa.FirstOrDefault(x => x.Email == email);

            return pessoa == null ? Json(true, JsonRequestBehavior.AllowGet) : Json(false, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}