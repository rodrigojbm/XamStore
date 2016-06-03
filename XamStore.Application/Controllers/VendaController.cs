using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XamStore.Domain.Entities.Operacao;
using XamStore.Domain.Enums;
using XamStore.Infrastructure.Context;

namespace XamStore.Application.Controllers
{
    [RoutePrefix("Admin")]
    public class VendaController : BaseAdminController
    {
        private readonly Context _db = new Context();

        [Route("Venda")]
        public async Task<ActionResult> Index()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var venda = _db.Venda.Include(v => v.Pedido);

            return View(await venda.ToListAsync());
        }

        [Route("Venda/Editar/{id}")]
        public async Task<ActionResult> Editar(int? id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var venda = await _db.Venda.FindAsync(id);

            if (venda == null)
                return HttpNotFound();

            ViewBag.PedidoItens = _db.PedidoItem.Include(a => a.Produto).Where(x => x.IdPedido == venda.IdPedido);

            return View(venda);
        }

        [HttpPost]
        [Route("Venda/Editar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar([Bind(Include = "Id, CodigoRastreioCorreios")] Venda venda)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index");

            InitializeMenuAdmin();

            ModelState.Remove("Data");
            if (!ModelState.IsValid)
                return View();

            var vend = await _db.Venda.FindAsync(venda.Id);
            vend.CodigoRastreioCorreios = venda.CodigoRastreioCorreios;
            vend.IsNova = false;
            vend.Data = DateTime.Now.Date;

            var pedido = vend.Pedido;
            pedido.Status = PedidoStatusEnum.Delivering;

            _db.Entry(pedido).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            _db.Entry(vend).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Deletar(int? id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var venda = await _db.Venda.FindAsync(id);
            if (venda == null)
                return HttpNotFound();

            return View(venda);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}