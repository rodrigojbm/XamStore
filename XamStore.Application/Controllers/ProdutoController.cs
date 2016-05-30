using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Infrastructure.Context;

namespace XamStore.Application.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly Context _db = new Context();

        public ActionResult Index(string registroPorPagina, string ordem, string pagina)
        {
            var produtos = _db.Produto.ToList();

            foreach (var key in Request.QueryString.Keys)
            {
                var value = Request.QueryString[key];

                switch (key)
                {
                    case "modelo":
                        produtos = produtos.Where(x => x.)
                }
            }

            return View();
        }
    }
}