using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PagedList;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Infrastructure.Context;
using static System.Convert;

namespace XamStore.Application.Controllers
{
    public class ProdutoController : BaseController
    {
        private readonly Context _db = new Context();

        public ActionResult Index(string registroPorPagina, string ordem, string pagina)
        {
            var produtos = _db.Produto.ToList();

            registroPorPagina = !string.IsNullOrEmpty(registroPorPagina) ? registroPorPagina : "6";
            ordem = !string.IsNullOrEmpty(ordem) ? ordem : "asc";
            pagina = !string.IsNullOrEmpty(pagina) ? pagina : "1";

            switch (ordem)
            {
                case "asc":
                    produtos = produtos.OrderBy(x => x.Id).ToList();
                    break;
                case "desc":
                    produtos = produtos.OrderByDescending(x => x.Id).ToList();
                    break;
                case "menorPreco":
                    produtos = produtos.OrderBy(x => x.Preco).ToList();
                    break;
                case "maiorPreco":
                    produtos = produtos.OrderByDescending(x => x.Preco).ToList();
                    break;
            }

            ViewBag.RegisterPerPage = registroPorPagina;
            ViewBag.Order = ordem;
            ViewBag.Page = registroPorPagina;

            decimal totalPages = produtos.Count / ToInt32(registroPorPagina);
            var resto = produtos.Count%ToInt32(registroPorPagina);
            ViewBag.TotalPages = resto > 0 ? totalPages + 1 : totalPages;
            produtos = produtos.ToPagedList(ToInt32(pagina), ToInt32(registroPorPagina)).ToList();

            var newProdutos = new List<Produto>();
            foreach (var produto in produtos)
            {
                produto.ProdutoImagens = _db.ProdutoImagem.Where(x => x.IdProduto == produto.Id).ToList();
                newProdutos.Add(produto);
            }

            ViewBag.Produtos = newProdutos;
            ViewBag.Menus = _db.Menu;
            ViewBag.Pessoa = _db.Pessoa;
            ViewBag.Fabricantes = _db.Fabricante;
            ViewBag.Genero = _db.Genero;
            ViewBag.Plataforma = _db.Plataforma;
            ViewBag.Console = _db.Console;
            ViewBag.Jogos = _db.Jogo;

            var countFabricanteDictionary = new Dictionary<Fabricante, int>();
            var countGeneroDictionary = new Dictionary<Genero, int>();
            var countPlataformaDictionary = new Dictionary<Plataforma, int>();
            var countConsoleDictionary = new Dictionary<Domain.Entities.Cadastro.Console, int>();
            var countJogoDictionary = new Dictionary<Jogo, int>();

            foreach (var fab in _db.Fabricante.ToList())
            {
                var count = _db.Produto.Count(x => x.Jogo.Fabricante.Id == fab.Id);
                if (count > 0)
                    countFabricanteDictionary.Add(fab, count);
            }

            foreach (var genero in _db.Genero.ToList())
            {
                var count = _db.Produto.Count(x => x.Jogo.Genero.Id == genero.Id);
                if (count > 0)
                    countGeneroDictionary.Add(genero, count);
            }

            foreach (var plataforma in _db.Plataforma.ToList())
            {
                var count = _db.Produto.Count(x => x.Jogo.Plataforma.Id == plataforma.Id);
                if (count > 0)
                    countPlataformaDictionary.Add(plataforma, count);
            }

            foreach (var console in _db.Console.ToList())
            {
                var count = _db.Produto.Count(x => x.Jogo.Console.Id == console.Id);
                if (count > 0)
                    countConsoleDictionary.Add(console, count);
            }

            foreach (var jogo in _db.Jogo.ToList())
            {
                var count = _db.Produto.Count(x => x.Jogo.Id == jogo.Id);
                if (count > 0)
                    countJogoDictionary.Add(jogo, count);
            }

            ViewBag.CountFabricantes = countFabricanteDictionary;
            ViewBag.CountGeneros = countGeneroDictionary;
            ViewBag.Plataformas = countPlataformaDictionary;
            ViewBag.Consoles = countConsoleDictionary;
            ViewBag.Jogos = countJogoDictionary;

            return View("Produto");
        }

        public ActionResult Detalhar(int id)
        {
            ViewBag.Menus = _db.Menu;

            var produto = _db.Produto.Find(id);
            produto.ProdutoImagens = _db.ProdutoImagem.Where(x => x.IdProduto == produto.Id).ToList();

            ViewBag.Produto = produto;

            var imagens = "{\"prod_1\":{ \"main\":{";

            var i = 0;
            foreach (var produtoImagem in produto.ProdutoImagens)
            {
                if (i == 0)
                {
                    imagens += "\"orig\": \"../../Imagens/Produto/" + produtoImagem.Imagem.Nome + "\",\"main\":\"../../Imagens/Produto/" + produtoImagem.Imagem.Nome + "\",\"thumb\":\"../../Imagens/Produto/" + produtoImagem.Imagem.Nome + "\",\"label\":\"\"},";
                    imagens += "\"gallery\":{";
                }

                imagens += "\"item_" + i + "\":{";
                imagens += "\"orig\":\"../../Imagens/Produto/" + produtoImagem.Imagem.Nome + "\",";
                imagens += "\"main\":\"../../Imagens/Produto/" + produtoImagem.Imagem.Nome + "\",";
                imagens += "\"thumb\":\"../../Imagens/Produto/" + produtoImagem.Imagem.Nome + "\",";
                imagens += "\"label\":\"\"}";

                if (i < (produto.ProdutoImagens.Count() - 1))
                    imagens += ",";

                i++;
            }

            imagens += "},\"type\":\"simple\",\"video\":false}}";

            ViewBag.Imagens = imagens;

            return View("Detalhe");
        }

        [HttpPost]
        public JsonResult RemoveQueryString(string filtro, string valor, string query)
        {
            var httpValues = new System.Collections.Specialized.NameValueCollection { HttpUtility.ParseQueryString(query) };
            httpValues.Remove(filtro);

            var routeValue = new RouteValueDictionary();

            foreach (var key in httpValues.AllKeys)
                routeValue.Add(key, httpValues[key]);

            return Json(new { RedirectUrl = Url.Action("Index", "Produto", routeValue) }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}