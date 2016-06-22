using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Infrastructure.Context;

namespace XamStore.Application.Controllers
{
    [RoutePrefix("Admin")]
    public class SlideController : BaseAdminController
    {
        private readonly Context _db = new Context();

        [Route("Slide")]
        public async Task<ActionResult> Index()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var slide = _db.Slide.Include(s => s.Produto);

            return View(await slide.ToListAsync());
        }

        [Route("Slide/Cadastrar")]
        public ActionResult Cadastrar()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            ViewBag.IdProduto = new SelectList(_db.Produto, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Slide/Cadastrar")]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Id, Imagem, IdProduto, File")] Slide slide, HttpPostedFileBase file)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var imageTypes = new string[]{
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
                };

            if (slide.File == null || slide.File.ContentLength == 0)
                ModelState.AddModelError("File", "Campo é requerido");
            else if (!imageTypes.Contains(slide.File.ContentType))
                ModelState.AddModelError("File", "Formatos suportados : GIF, JPG or PNG.");

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var extensao = "";

                    switch (file.ContentType)
                    {
                        case "image/gif":
                            extensao = ".gif";
                            break;
                        case "image/jpeg":
                            extensao = ".jpg";
                            break;
                        case "image/pjpeg":
                            extensao = ".jpg";
                            break;
                        default:
                            extensao = ".png";
                            break;
                    }

                    var imageCript = file.FileName + "imagem-slide";

                    imageCript = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(imageCript)).Select(s => s.ToString("x2")));

                    var fullPath = "~/Imagens/Produto/" + imageCript + extensao;
                    file.SaveAs(Server.MapPath(fullPath));

                    slide.Imagem = imageCript + extensao;
                }

                _db.Slide.Add(slide);
                await _db.SaveChangesAsync();

                ViewBag.Mensagem = "Slide cadastrado com sucesso!";

                return View("Index", await _db.Slide.ToListAsync());
            }

            ViewBag.cProduto = new SelectList(_db.Produto, "Id", "Nome", slide.IdProduto);
            return View(slide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Slide/Editar")]
        public async Task<ActionResult> Editar(int? id, HttpPostedFile file)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var slide = await _db.Slide.FindAsync(id);
            if (slide == null)
                return HttpNotFound();

            ViewBag.IdProduto = new SelectList(_db.Produto, "Id", "Nome", slide.IdProduto);
            return View(slide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Slide/Editar")]
        public async Task<ActionResult> Editar([Bind(Include = "Id, Nome, IdProduto, Imagem, File")] Slide slide, HttpPostedFileBase file)
        {
            try
            {
                if (!ChecarUsuarioAdminAutenticado())
                    return RedirectToAction("Index", "LoginAdmin");

                InitializeMenuAdmin();

                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        var imageCript = $"{file.FileName}imagem-slide";
                        imageCript = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(imageCript)).Select(s => s.ToString("x2")));

                        var fullPath = "~/Imagens/Produto/" + imageCript + ".png";
                        file.SaveAs(Server.MapPath(fullPath));

                        slide.Imagem = $"{imageCript}.png";
                        _db.Entry(slide).State = EntityState.Modified;
                        await _db.SaveChangesAsync();
                    }

                    ViewBag.Mensagem = "Slide editado com sucesso!";
                    return View("Index", await _db.Slide.ToListAsync());
                }

                ViewBag.IdProduto = new SelectList(_db.Produto, "Id", "Nome", slide.IdProduto);
                return View(slide);
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        [Route("Slide/Deletar/{id}")]
        public ActionResult Deletar(int? id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var slide = _db.Slide.Find(id);

            return View(slide);
        }

        [Route("Slide/Deletar")]
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletarConfirmacao(int id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var slide = await _db.Slide.FindAsync(id);

            _db.Slide.Remove(slide);

            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Slide deletado com sucesso!";

            return View("Index", await _db.Slide.ToListAsync());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}