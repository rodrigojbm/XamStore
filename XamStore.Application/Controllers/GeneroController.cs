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

namespace XamStore.Application.Controllers
{
    [RoutePrefix("Admin")]
    public class GeneroController : BaseAdminController
    {
        private readonly Context _db = new Context();

        [Route("Genero")]
        public async Task<ActionResult> Index()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            return View(await _db.Genero.ToListAsync());
        }

        [Route("Genero/Cadastrar")]
        public ActionResult Cadastrar()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Genero/Cadastrar")]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Id, Nome")] Genero genero)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (!ModelState.IsValid)
                return View(genero);

            _db.Genero.Add(genero);
            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Gênero cadastrada com sucesso!";

            return View("Index", await _db.Genero.ToListAsync());
        }

        [Route("Genero/Editar/{id}")]
        public async Task<ActionResult> Editar(int? id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var genero = await _db.Genero.FindAsync(id);
            if (genero == null)
                return HttpNotFound();

            return View(genero);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Genero/Editar/{id}")]
        public async Task<ActionResult> Editar([Bind(Include = "Id, Nome")] Genero genero)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (!ModelState.IsValid)
                return View(genero);

            _db.Entry(genero).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Gênero editado com sucesso!";

            return View("Index", await _db.Genero.ToListAsync());
        }

        [Route("Genero/Deletar/{id}")]
        public async Task<ActionResult> Deletar(int? id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var genero = await _db.Genero.FindAsync(id);

            ViewBag.Jogos = _db.Jogo.Where(x => x.IdGenero == id).ToList();

            return View(genero);
        }

        [Route("Genero/Deletar")]
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletarConfirmacao(int id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var genero = await _db.Genero.FindAsync(id);

            _db.Genero.Remove(genero);
            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Genero deletado com sucesso!";

            return View("Index", await _db.Genero.ToListAsync());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}