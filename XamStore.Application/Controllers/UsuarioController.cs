using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Infrastructure.Context;

namespace XamStore.Application.Controllers
{
    [RoutePrefix("Admin")]
    public class UsuarioController : BaseAdminController
    {
        private readonly Context _db = new Context();

        [Route("Usuario")]
        public async Task<ActionResult> Index()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var usuario = _db.Usuario.Include(u => u.UsuarioNivel);

            return View(await usuario.ToListAsync());
        }

        [Route("Usuario/Cadastrar")]
        public ActionResult Cadastrar()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            ViewBag.IdUsuarioNivel = new SelectList(_db.UsuarioNivel, "Id", "Nome");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Usuario/Cadastrar")]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Id, Nome, Login, Senha, IdUsuarioNivel")] Usuario usuario)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (ModelState.IsValid)
            {
                _db.Usuario.Add(usuario);
                await _db.SaveChangesAsync();

                ViewBag.Mensagem = "Usuário cadastrado com sucesso!";

                return View("Index", await _db.Usuario.ToListAsync());
            }

            ViewBag.IdUsuarioNivel = new SelectList(_db.UsuarioNivel, "Id", "Nome", usuario.IdUsuarioNivel);
            return View(usuario);
        }

        [Route("Usuario/Editar/{id}")]
        public async Task<ActionResult> Editar(int? id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var usuario = await _db.Usuario.FindAsync(id);

            if (usuario == null)
                return HttpNotFound();

            ViewBag.IdUsuarioNivel = new SelectList(_db.UsuarioNivel, "Id", "Nome", usuario.IdUsuarioNivel);

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Usuario/Editar/{id}")]
        public async Task<ActionResult> Editar([Bind(Include = "Id, Nome, Login, Senha, IdUsuarioNivel")] Usuario usuario)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (ModelState.IsValid)
            {
                _db.Entry(usuario).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                ViewBag.Mensagem = "Usuário editado com sucesso!";

                return View("Index", await _db.Usuario.ToListAsync());
            }
            ViewBag.IdUsuarioNivel = new SelectList(_db.UsuarioNivel, "Id", "Nome", usuario.IdUsuarioNivel);

            return View(usuario);
        }

        [Route("Usuario/Deletar/{id}")]
        public async Task<ActionResult> Deletar(int? id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            return View(await _db.Usuario.FindAsync(id));
        }

        [Route("Usuario/Deletar")]
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletarConfirmacao(int id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var usuario = await _db.Usuario.FindAsync(id);
            _db.Usuario.Remove(usuario);
            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Usuário Deletado com sucesso!";

            return View("Index", await _db.Usuario.ToListAsync());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}