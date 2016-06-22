using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Rotativa;
using XamStore.Domain.Entities.Operacao;
using XamStore.Infrastructure.Context;
using SessionAutenticacaoAdmin = XamStore.Domain.Entities.Sistema.SessionAutenticacaoAdmin;

namespace XamStore.Application.Controllers
{
    [RoutePrefix("Admin")]
    public class RelatorioController : BaseAdminController
    {
        private readonly Context _db = new Context();

        [Route("Relatorio")]
        public async Task<ActionResult> Index()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            return View();
        }

        [Route("Relatorio/Venda")]
        public ActionResult Venda()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Relatorio/Venda")]
        public async Task<ActionResult> Venda([Bind(Include = "DataInicial, DataFinal")] Relatorio relatorio)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var pdf = new ViewAsPdf {ViewName = "VendaRelatorio"};

            var sessionAdmin = Session["AutenticacaoAdmin"] as SessionAutenticacaoAdmin;
            var usuario = _db.Usuario.FirstOrDefault(x => x.Id == sessionAdmin.Id);

            ViewBag.Filtros = relatorio;
            ViewBag.Usuario = usuario;

            var vendas = _db.Venda.Where(x => x.Data >= relatorio.DataInicial && x.Data <= relatorio.DataFinal).ToList();
            ViewBag.Vendas = vendas;

            return pdf;
        }

        [Route("Relatorio/Cliente")]
        public ActionResult Cliente()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            return View();
        }

        [Route("Relatorio/Cliente")]
        public async Task<ActionResult> Clientes()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var pdf = new ViewAsPdf {ViewName = "ClienteRelatorio"};

            var sessionAdmin = Session["AutenticacaoAdmin"] as SessionAutenticacaoAdmin;
            var usuario = _db.Usuario.FirstOrDefault(x => x.Id == sessionAdmin.Id);

            ViewBag.Usuario = usuario;

            var pessoas = _db.Pessoa.ToList();
            ViewBag.Pessoas = pessoas;

            return pdf;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}