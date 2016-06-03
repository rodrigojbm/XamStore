using System;
using System.Collections.Generic;
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
using static System.Convert;

namespace XamStore.Application.Controllers
{
    [RoutePrefix("Admin")]
    public class ProdutoAdminController : BaseAdminController
    {
        private readonly Context _db = new Context();

        [Route("ProdutoAdmin")]
        public async Task<ActionResult> Index()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var produto = _db.Produto;

            return View(await produto.ToListAsync());
        }

        [Route("ProdutoAdmin/Cadastrar")]
        public ActionResult Cadastrar()
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            ViewBag.IdCategoria = new SelectList(_db.Categoria, "Id", "Nome");
            ViewBag.IdJogo = new SelectList(_db.Jogo, "Id", "Nome");
            ViewBag.IdPlataforma = new SelectList(_db.Plataforma, "Id", "Nome");
            ViewBag.IdGenero = new SelectList(_db.Genero, "Id", "Nome");
            ViewBag.IdFabricante = new SelectList(_db.Fabricante, "Id", "Nome");
            ViewBag.IdConsole = new SelectList(_db.Console, "Id", "Nome");
            //ViewBag.Rede = new MultiSelectList(_db.Rede, "Id", "Descricao");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ProdutoAdmin/Cadastrar")]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Id, Nome, Peso, Descricao, Garantia, PesoString, Preco, PrecoString Estoque, IdCategoria")] Produto produto, List<HttpPostedFileBase> images)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            ViewBag.IdCategoria = new SelectList(_db.Categoria, "Id", "Nome");
            ViewBag.IdJogo = new SelectList(_db.Jogo, "Id", "Nome");
            ViewBag.IdPlataforma = new SelectList(_db.Plataforma, "Id", "Nome");
            ViewBag.IdGenero = new SelectList(_db.Genero, "Id", "Nome");
            ViewBag.IdFabricante = new SelectList(_db.Fabricante, "Id", "Nome");
            ViewBag.IdConsole = new SelectList(_db.Console, "Id", "Nome");

            ModelState.Remove("Preco");

            if (!ModelState.IsValid) return View();
            produto.Categoria = await _db.Categoria.FindAsync(produto.IdCategoria);
            produto.Jogo = await _db.Jogo.FindAsync(produto.IdJogo);

            produto.Preco = ToDouble($"{produto.PrecoString:N}", System.Globalization.CultureInfo.InvariantCulture);
            produto.Peso = ToDecimal($"{produto.PesoString:N}", System.Globalization.CultureInfo.InvariantCulture);

            var estoque = new ProdutoEstoque
            {
                Produto = produto,
                Quantidade = produto.Estoque
            };

            _db.ProdutoEstoque.Add(estoque);
            _db.Produto.Add(produto);

            await _db.SaveChangesAsync();

            if (images.Any())
            {
                foreach (var image in images)
                {
                    if (image == null)
                        continue;

                    var extensao = "";

                    switch (image.ContentType)
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


                    var imagem = new Imagem();
                    var imageCript = $"{image.FileName}{produto.Descricao}";
                    imageCript = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(imageCript)).Select(s => s.ToString("x2")));
                    var fullPath = $"~/Imagens/Produto/{imageCript}{extensao}";
                    image.SaveAs(Server.MapPath(fullPath));

                    imagem.Nome = $"{imageCript}{extensao}";
                    var produtoImagem = new ProdutoImagem
                    {
                        Produto = produto,
                        Imagem = imagem
                    };

                    _db.ProdutoImagem.Add(produtoImagem);
                }
            }

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

            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Produto cadastrado com sucesso!";

            return View("Index", await _db.Produto.ToListAsync());
        }

        [Route("ProdutoAdmin/Editar/{id}")]
        public async Task<ActionResult> Editar(int? id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var produto = await _db.Produto.FindAsync(id);

            if (produto == null)
                return HttpNotFound();

            produto.PrecoString = $"{produto.Preco}";
            produto.PesoString = $"{produto.Peso}";

            ViewBag.IdCategoria = new SelectList(_db.Categoria, "Id", "Nome");
            ViewBag.IdJogo = new SelectList(_db.Jogo, "Id", "Nome");
            //ViewBag.IdPlataforma = new SelectList(_db.Plataforma, "Id", "Nome");
            //ViewBag.IdGenero = new SelectList(_db.Genero, "Id", "Nome");
            //ViewBag.IdFabricante = new SelectList(_db.Fabricante, "Id", "Nome");
            //ViewBag.IdConsole = new SelectList(_db.Console, "Id", "Nome");

            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ProdutoAdmin/Editar")]
        public async Task<ActionResult> Editar([Bind(Include="Id, Nome, Descricao, Garantia, Peso, PesoString, PrecoString, IdCategoria, Estoque")]
        Produto produto, List<HttpPostedFileBase> images)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            ModelState.Remove("Preco");

            if (ModelState.IsValid)
            {
                if (images.Any())
                {
                    foreach (var image in images)
                    {
                        if (image == null)
                            continue;

                        var extensao = "";

                        switch (image.ContentType)
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

                        var imagem = new Imagem();

                        var imageCript = $"{image.FileName}{produto.Descricao}";
                        imageCript = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(imageCript)).Select(s => s.ToString("x2")));

                        var fullPath = $"~/Imagens/Produto/{imageCript}{extensao}";
                        image.SaveAs(Server.MapPath(fullPath));
                        imagem.Nome = $"{imageCript}{extensao}";

                        var produtoImagem = new ProdutoImagem
                        {
                            Produto = produto,
                            Imagem = imagem
                        };

                        _db.ProdutoImagem.Add(produtoImagem);
                    }
                }

                produto.Categoria = await _db.Categoria.FindAsync(produto.IdCategoria);
                produto.Jogo = await _db.Jogo.FindAsync(produto.IdJogo);
                //produto.Jogo.Plataforma = await _db.Plataforma.FindAsync(produto.Jogo.IdPlataforma);
                //produto.Jogo.Genero = await _db.Genero.FindAsync(produto.Jogo.IdGenero);
                //produto.Jogo.Fabricante = await _db.Fabricante.FindAsync(produto.Jogo.IdFabricante);
                //produto.Jogo.Console = await _db.Console.FindAsync(produto.Jogo.IdConsole);

                produto.Preco = ToDouble(produto.PrecoString, System.Globalization.CultureInfo.InvariantCulture);
                produto.Peso = ToDecimal(produto.PesoString, System.Globalization.CultureInfo.InvariantCulture);

                _db.Entry(produto).State = EntityState.Modified;
                await _db.SaveChangesAsync();

            }

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

            ViewBag.Mensagem = "Produto editado com sucesso!";

            return View("Index", await _db.Produto.ToListAsync());
        }

        [Route("ProdutoAdmin/Deletar/{id}")]
        public ActionResult Deletar(int? id)
        {
            try
            {
                if (!ChecarUsuarioAdminAutenticado())
                    return RedirectToAction("Index", "LoginAdmin");

                InitializeMenuAdmin();

                var produto = _db.Produto.Find(id);

                return View(produto);
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        [Route("ProdutoAdmin/Deletar")]
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletarConfirmacao(int id)
        {
            if (!ChecarUsuarioAdminAutenticado())
                return RedirectToAction("Index", "LoginAdmin");

            InitializeMenuAdmin();

            var produto = await _db.Produto.Where(x => x.Id == id).FirstOrDefaultAsync();

            var imagens = _db.ProdutoImagem.Where(x => x.IdProduto == id).ToList();
            var estoques = _db.ProdutoEstoque.Where(x => x.IdProduto == id).ToList();

            foreach (var imagem in imagens)
                _db.ProdutoImagem.Remove(imagem);

            foreach (var estoque in estoques)
                _db.ProdutoEstoque.Remove(estoque);

            _db.Produto.Remove(produto);

            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Produto deletado com sucesso!";

            return View("Index", await _db.Produto.ToListAsync());
        }

        public void DeletarImagem(int? id, int produtoId)
        {
            try
            {
                var produtoImagem = _db.ProdutoImagem.Find(id);
                var imagem = _db.Imagem.Find(produtoImagem.IdImagem);

                _db.ProdutoImagem.Remove(produtoImagem);
                _db.Imagem.Remove(imagem);

                _db.SaveChanges();

                Response.Redirect(Url.Action($"editar/{produtoId}", "Admin/Produto"));
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}