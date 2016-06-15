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
    public class FabricanteController : BaseAdminController
    {
        private readonly Context _db = new Context();

        [Route("Fabricante")]
        public async Task<ActionResult> Index()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            return View(await _db.Fabricante.ToListAsync());
        }

        [Route("Fabricante/Cadastrar")]
        public ActionResult Cadastrar()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Fabricante/Cadastrar")]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Id, Nome, Sobre")] Fabricante fabricante)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (!ModelState.IsValid)
                return View(fabricante);

            _db.Fabricante.Add(fabricante);
            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Fabricante cadastrado com sucesso!";

            return View("Index", await _db.Fabricante.ToListAsync());
        }

        [Route("Fabricante/Editar/{id}")]
        public async Task<ActionResult> Editar(int? id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var fabricante = await _db.Fabricante.FindAsync(id);
            if (fabricante == null)
                return HttpNotFound();

            return View(fabricante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Fabricante/Editar/{id}")]
        public async Task<ActionResult> Editar([Bind(Include = "Id, Nome, Sobre")] Fabricante fabricante)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (!ModelState.IsValid)
                return View(fabricante);
            
            _db.Entry(fabricante).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Fabricante editado com sucesso!";

            return View("Index", await _db.Fabricante.ToListAsync());
        }

        [Route("Fabricante/Deletar/{id}")]
        public async Task<ActionResult> Deletar(int? id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var fabricante = await _db.Fabricante.FindAsync(id);

            ViewBag.Jogos = _db.Jogo.Where(x => x.IdFabricante == id).ToList();

            return View(fabricante);
        }

        [Route("Fabricante/Deletar")]
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletarConfirmacao(int id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var fabricante = await _db.Fabricante.FindAsync(id);

            _db.Fabricante.Remove(fabricante);
            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Fabricante deletado com sucesso!";

            return View("Index", await _db.Fabricante.ToListAsync());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}