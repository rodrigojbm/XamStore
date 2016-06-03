using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Infrastructure.Context;
using Console = XamStore.Domain.Entities.Cadastro.Console;

namespace XamStore.Application.Controllers
{
    [RoutePrefix("Admin")]
    public class ConsoleController : BaseAdminController
    {
        private readonly Context _db = new Context();

        [Route("Console")]
        public async Task<ActionResult> Index()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            return View(await _db.Console.ToListAsync());
        }

        [Route("Console/Cadastrar")]
        public ActionResult Cadastrar()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Console/Cadastrar")]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Id, Nome")] Console console)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (!ModelState.IsValid)
                return View(console);

            _db.Console.Add(console);
            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Console cadastrada com sucesso!";

            return View("Index", await _db.Console.ToListAsync());
        }

        [Route("Console/Editar/{id}")]
        public async Task<ActionResult> Editar(int? id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var console = await _db.Console.FindAsync(id);
            if (console == null)
                return HttpNotFound();

            return View(console);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Console/Editar/{id}")]
        public async Task<ActionResult> Editar([Bind(Include = "Id, Nome")] Console console)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (!ModelState.IsValid)
                return View(console);

            _db.Entry(console).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Console editado com sucesso!";

            return View("Index", await _db.Console.ToListAsync());
        }

        [Route("Console/Deletar/{id}")]
        public async Task<ActionResult> Deletar(int? id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var console = await _db.Console.FindAsync(id);

            ViewBag.Jogos = _db.Jogo.Where(x => x.IdConsole == id).ToList();

            return View(console);
        }

        [Route("Console/Deletar")]
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletarConfirmacao(int id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var console = await _db.Console.FindAsync(id);

            _db.Console.Remove(console);
            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Console deletado com sucesso!";

            return View("Index", await _db.Console.ToListAsync());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}