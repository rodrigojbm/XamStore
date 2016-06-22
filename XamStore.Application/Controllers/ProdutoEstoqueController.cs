using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Infrastructure.Context;

namespace XamStore.Application.Controllers
{
    [RoutePrefix("Admin")]
    public class ProdutoEstoqueController : BaseAdminController
    {
        private readonly Context _db = new Context();

        [Route("Estoque")]
        public async Task<ActionResult> Index()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var produtoEstoque = _db.ProdutoEstoque.Include(p => p.Produto);

            return View("~/Views/Estoque/Index.cshtml", await produtoEstoque.ToListAsync());
        }

        [Route("Estoque/Editar/{id}")]
        public async Task<ActionResult> Editar(int? id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var produtoEstoque = await _db.ProdutoEstoque.FindAsync(id);

            if (produtoEstoque == null)
                return HttpNotFound();

            ViewBag.IdProduto = new SelectList(_db.Produto, "Id", "Nome", produtoEstoque.IdProduto);

            return View("~/Views/Estoque/Editar.cshtml", produtoEstoque);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Estoque/Editar")]
        public async Task<ActionResult> Editar([Bind(Include = "Id, Quantidade, IdProduto")] ProdutoEstoque produtoEstoque)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (ModelState.IsValid)
            {
                _db.Entry(produtoEstoque).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdProduto = new SelectList(_db.Produto, "Id", "Nome", produtoEstoque.IdProduto);
            return View(produtoEstoque);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}