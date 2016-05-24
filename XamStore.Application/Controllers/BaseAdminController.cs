using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XamStore.Domain.Entities.Sistema;
using XamStore.Infrastructure.Context;

namespace XamStore.Application.Controllers
{
    public class BaseAdminController : Controller
    {
        private readonly Context _db = new Context();

        public void InitializeMenuAdmin()
        {
            ViewBag.VendaCount = _db.Venda.Count(x => x.IsNova);

            var sessionAdmin = Session["autenticacaoAdmin"] as SessionAutenticacaoAdmin;
            var usuario = _db.Usuario.FirstOrDefault(x => x.Id == sessionAdmin.Id);
            var menu = usuario?.UsuarioNivel.Id == 1 ? _db.MenuAdmin.ToList() : _db.MenuAdmin.Where(x => x.Tipo == 0).ToList();

            ViewBag.Menu = menu;
        }

        public bool ChecarUsuarioAdminAutenticado()
        {
            var sessionAdmin = (SessionAutenticacaoAdmin) Session?["autenticacaoAdmin"];
            return sessionAdmin != null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}