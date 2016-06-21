using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    public class JogoAdminController : BaseAdminController
    {
        private readonly Context _db = new Context();

        [Route("JogoAdmin")]
        public async Task<ActionResult> Index()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var jogo = _db.Jogo;

            return View(await jogo.ToListAsync());
        }

        [Route("JogoAdmin/Cadastrar")]
        public ActionResult Cadastrar()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            ViewBag.IdPlataforma = new SelectList(_db.Plataforma, "Id", "Nome");
            ViewBag.IdGenero = new SelectList(_db.Genero, "Id", "Nome");
            ViewBag.IdFabricante = new SelectList(_db.Fabricante, "Id", "Nome");
            ViewBag.IdConsole = new SelectList(_db.Console, "Id", "Nome");
            //ViewBag.Rede = new MultiSelectList(_db.Rede, "Id", "Descricao");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("JogoAdmin/Cadastrar")]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Id, Nome, Multiplayer, Jogadores, Classificacao, IdPlataforma, IdGenero, IdConsole, IdFabricante")] Jogo jogo, List<HttpPostedFileBase> images)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            ViewBag.IdPlataforma = new SelectList(_db.Plataforma, "Id", "Nome");
            ViewBag.IdGenero = new SelectList(_db.Genero, "Id", "Nome");
            ViewBag.IdFabricante = new SelectList(_db.Fabricante, "Id", "Nome");
            ViewBag.IdConsole = new SelectList(_db.Console, "Id", "Nome");

            //ModelState.Remove("Preco");

            if (!ModelState.IsValid) return View();
            jogo.Plataforma = await _db.Plataforma.FindAsync(jogo.IdPlataforma);
            jogo.Genero = await _db.Genero.FindAsync(jogo.IdGenero);
            jogo.Fabricante = await _db.Fabricante.FindAsync(jogo.IdFabricante);
            jogo.Console = await _db.Console.FindAsync(jogo.IdConsole);

            //jogo.Preco = ToDouble($"{produto.PrecoString}", System.Globalization.CultureInfo.InvariantCulture);
            //jogo.Peso = ToDecimal($"{produto.PesoString}", System.Globalization.CultureInfo.InvariantCulture);

            //var estoque = new ProdutoEstoque
            //{
            //    Produto = produto,
            //    Quantidade = produto.Estoque
            //};

            //_db.ProdutoEstoque.Add(estoque);
            _db.Jogo.Add(jogo);

            await _db.SaveChangesAsync();

            //if (images.Any())
            //{
            //    foreach (var image in images)
            //    {
            //        if (image == null)
            //            continue;

            //        var extensao = "";

            //        switch (image.ContentType)
            //        {
            //            case "image/gif":
            //                extensao = ".gif";
            //                break;
            //            case "image/jpeg":
            //                extensao = ".jpg";
            //                break;
            //            case "image/pjpeg":
            //                extensao = ".jpg";
            //                break;
            //            default:
            //                extensao = ".png";
            //                break;
            //        }


            //        var imagem = new Imagem();
            //        var imageCript = $"{image.FileName}{jogo.Nome}";
            //        imageCript = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(imageCript)).Select(s => s.ToString("x2")));
            //        var fullPath = $"~/Imagens/Produto/{imageCript}{extensao}";
            //        image.SaveAs(Server.MapPath(fullPath));

            //        imagem.Nome = $"{imageCript}{extensao}";
            //        var produtoImagem = new ProdutoImagem
            //        {
            //            Produto = produto,
            //            Imagem = imagem
            //        };

                   // _db.ProdutoImagem.Add(produtoImagem);
            //    }
            //}

            //if (produto.IdCategoria != null)
            //{
            //    foreach (var categoria in produto.IdCategoria)
            //    {
            //        var produtoCategoria = new Categoria
            //        {
            //            Nome = categoria,
            //            Produto = produto
            //        };

            //        _db.Categoria.Add(produtoCategoria);
            //    }
            //}

            //await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Jogo cadastrado com sucesso!";

            return View("Index", await _db.Jogo.ToListAsync());
        }

        [Route("JogoAdmin/Editar/{id}")]
        public async Task<ActionResult> Editar(int? id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var produto = await _db.Jogo.FindAsync(id);

            if (produto == null)
                return HttpNotFound();

            //produto.PrecoString = $"{produto.Preco}";
            //produto.PesoString = $"{produto.Peso}";

            ViewBag.IdPlataforma = new SelectList(_db.Plataforma, "Id", "Nome");
            ViewBag.IdGenero = new SelectList(_db.Genero, "Id", "Nome");
            ViewBag.IdFabricante = new SelectList(_db.Fabricante, "Id", "Nome");
            ViewBag.IdConsole = new SelectList(_db.Console, "Id", "Nome");

            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("JogoAdmin/Editar")]
        public async Task<ActionResult> Editar([Bind(Include="Id, Nome, Multiplayer, Jogadores, Classificacao, IdPlataforma, IdGenero, IdConsole, IdFabricante")]
        Jogo jogo, List<HttpPostedFileBase> images)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            ModelState.Remove("Preco");

            //if (ModelState.IsValid)
            //{
            //    if (images.Any())
            //    {
            //        foreach (var image in images)
            //        {
            //            if (image == null)
            //                continue;

            //            var extensao = "";

            //            switch (image.ContentType)
            //            {
            //                case "image/gif":
            //                    extensao = ".gif";
            //                    break;
            //                case "image/jpeg":
            //                    extensao = ".jpg";
            //                    break;
            //                case "image/pjpeg":
            //                    extensao = ".jpg";
            //                    break;
            //                default:
            //                    extensao = ".png";
            //                    break;
            //            }

            //            var imagem = new Imagem();

            //            var imageCript = $"{image.FileName}{jogo.Nome}";
            //            imageCript = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(imageCript)).Select(s => s.ToString("x2")));

            //            var fullPath = $"~/Imagens/Produto/{imageCript}{extensao}";
            //            image.SaveAs(Server.MapPath(fullPath));
            //            imagem.Nome = $"{imageCript}{extensao}";

            //            var produtoImagem = new ProdutoImagem
            //            {
            //                Produto = produto,
            //                Imagem = imagem
            //            };

            //            _db.ProdutoImagem.Add(produtoImagem);
            //        }
            //    }

            jogo.Plataforma = await _db.Plataforma.FindAsync(jogo.IdPlataforma);
            jogo.Genero = await _db.Genero.FindAsync(jogo.IdGenero);
            jogo.Fabricante = await _db.Fabricante.FindAsync(jogo.IdFabricante);
            jogo.Console = await _db.Console.FindAsync(jogo.IdConsole);

            //produto.Preco = ToDouble(produto.PrecoString, System.Globalization.CultureInfo.InvariantCulture);
            //produto.Peso = ToDecimal(produto.PesoString, System.Globalization.CultureInfo.InvariantCulture);

            _db.Entry(jogo).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        //}

        //if (produto.IdCategoria != null)
        //{
        //    var categoriaDataList = await _db.Categoria.Where(x => x.Produto.Id == produto.Id).ToListAsync();

        //    foreach (var categoriaData in categoriaDataList)
        //        _db.Categoria.Remove(categoriaData);

        //    await _db.SaveChangesAsync();

        //    foreach (var categoria in produto.IdCategoria)
        //    {
        //        var produtoCategoria = new Categoria
        //        {
        //            Nome = categoria,
        //            Produto = produto
        //        };

        //        _db.Categoria.Add(produtoCategoria);
        //    }

        //    await _db.SaveChangesAsync();
        //}
        //else
        //{
        //    var categoriaDataList = await _db.Categoria.Where(x => x.Produto.Id == produto.Id).ToListAsync();

        //    foreach (var categoriaData in categoriaDataList)
        //        _db.Categoria.Remove(categoriaData);

        //    await _db.SaveChangesAsync();
        //}

        ViewBag.Mensagem = "Jogo editado com sucesso!";

            return View("Index", await _db.Jogo.ToListAsync());
        }

        [Route("JogoAdmin/Deletar/{id}")]
        public ActionResult Deletar(int? id)
        {
            try
            {
                if (!ChecarUsuarioAdminAutenticado())
                    return RedirectToAction("Index", "LoginAdmin");

                InitializeMenuAdmin();

                var produto = _db.Jogo.Find(id);

                ViewBag.Produtos = _db.Produto.Where(x => x.IdJogo == id).ToList();

                return View(produto);
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        [Route("JogoAdmin/Deletar")]
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletarConfirmacao(int id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var jogo = await _db.Jogo.Where(x => x.Id == id).FirstOrDefaultAsync();

            //var imagens = _db.ProdutoImagem.Where(x => x.IdProduto == id).ToList();
            //var estoques = _db.ProdutoEstoque.Where(x => x.IdProduto == id).ToList();

            //foreach (var imagem in imagens)
            //    _db.ProdutoImagem.Remove(imagem);

            //foreach (var estoque in estoques)
            //    _db.ProdutoEstoque.Remove(estoque);

            _db.Jogo.Remove(jogo);

            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Jogo deletado com sucesso!";

            return View("Index", await _db.Jogo.ToListAsync());
        }

        //public void DeletarImagem(int? id, int produtoId)
        //{
        //    try
        //    {
        //        var produtoImagem = _db.ProdutoImagem.Find(id);
        //        var imagem = _db.Imagem.Find(produtoImagem.IdImagem);

        //        _db.ProdutoImagem.Remove(produtoImagem);
        //        _db.Imagem.Remove(imagem);

        //        _db.SaveChanges();

        //        Response.Redirect(Url.Action($"editar/{produtoId}", "Admin/Produto"));
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}