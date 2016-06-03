using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Infrastructure.Context;

namespace XamStore.Application.Controllers
{
    [RoutePrefix("Admin")]
    public class CategoriaController : BaseAdminController
    {
        private readonly Context _db = new Context();

        [Route("Categoria")]
        public async Task<ActionResult> Index()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            return View(await _db.Categoria.ToListAsync());
        }

        [Route("Categoria/Cadastrar")]
        public ActionResult Cadastrar()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Categoria/Cadastrar")]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Id, Descricao")] Categoria categoria)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (!ModelState.IsValid)
                return View(categoria);

            _db.Categoria.Add(categoria);
            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Categoria cadastrada com sucesso!";

            return View("Index", await _db.Categoria.ToListAsync());
        }

        [Route("Categoria/Editar/{id}")]
        public async Task<ActionResult> Editar(int? id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var categoria = await _db.Categoria.FindAsync(id);
            if (categoria == null)
                return HttpNotFound();

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Categoria/Editar/{id}")]
        public async Task<ActionResult> Editar([Bind(Include = "Id, Descricao")] Categoria categoria)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (!ModelState.IsValid) return View(categoria);
            _db.Entry(categoria).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Categoria editada com sucesso!";

            return View("Index", await _db.Categoria.ToListAsync());
        }

        [Route("Categoria/Deletar/{id}")]
        public async Task<ActionResult> Deletar(int? id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var produtos = await _db.Produto.Where(x => x.IdCategoria == id).ToListAsync();

            ViewBag.ProdutoCount = produtos.Count;

            var categoria = await _db.Categoria.FindAsync(id);

            return View(categoria);
        }

        [Route("Categoria/Deletar")]
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletarConfirmacao(int id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var categoria = await _db.Categoria.FindAsync(id);
            _db.Categoria.Remove(categoria);
            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Categoria deletada com sucesso!";

            return View("Index", await _db.Categoria.ToListAsync());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}