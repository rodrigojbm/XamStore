using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Infrastructure.Context;

namespace XamStore.Application.Controllers
{
    [RoutePrefix("Admin")]
    public class PlataformaController : BaseAdminController
    {
        private readonly Context _db = new Context();

        [Route("Plataforma")]
        public async Task<ActionResult> Index()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            return View(await _db.Plataforma.ToListAsync());
        }

        [Route("Plataforma/Cadastrar")]
        public ActionResult Cadastrar()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Plataforma/Cadastrar")]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Id, Nome")] Plataforma plataforma)
        { 
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (!ModelState.IsValid)
                return View(plataforma);

            _db.Plataforma.Add(plataforma);
            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Plataforma cadastrada com sucesso!";

            return View("Index", await _db.Plataforma.ToListAsync());
        }

        [Route("Plataforma/Editar/{id}")]
        public async Task<ActionResult> Editar(int? id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var plataforma = await _db.Plataforma.FindAsync(id);
            if (plataforma == null)
                return HttpNotFound();

            return View(plataforma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Plataforma/Editar/{id}")]
        public async Task<ActionResult> Editar([Bind(Include = "Id, Nome")] Plataforma plataforma)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (!ModelState.IsValid)
                return View(plataforma);

            _db.Entry(plataforma).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Plataforma editado com sucesso!";

            return View("Index", await _db.Plataforma.ToListAsync());
        }

        [Route("Plataforma/Deletar/{id}")]
        public async Task<ActionResult> Deletar(int? id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var plataforma = await _db.Plataforma.FindAsync(id);

            ViewBag.Produtos = _db.Produto.Where(x => x.IdPlataforma == id).ToList();

            return View(plataforma);
        }

        [Route("Plataforma/Deletar")]
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletarConfirmacao(int id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var plataforma = await _db.Plataforma.FindAsync(id);

            _db.Plataforma.Remove(plataforma);
            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Plataforma deletado com sucesso!";

            return View("Index", await _db.Plataforma.ToListAsync());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}