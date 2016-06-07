using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;
using XamStore.Application.br.com.correios.ws1;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Domain.Entities.Operacao;
using XamStore.Domain.Entities.Sistema;
using XamStore.Domain.Enums;
using XamStore.Infrastructure.Context;
using static System.Convert;
using SessionAutenticacaoClient = XamStore.Domain.Entities.Sistema.SessionAutenticacaoClient;
using SessionCarrinho = XamStore.Domain.Entities.Sistema.SessionCarrinho;

namespace XamStore.Application.Controllers
{
    public class CarrinhoController : BaseController
    {
        private readonly Context _db = new Context();
        private SessionCarrinho _sessionCarrinho;

        public ActionResult Index()
        {
            ViewBag.Menus = _db.Menu.ToList();
            var session = Session["Autenticacao"] as SessionAutenticacaoClient;

            if (session == null)
            {
                ViewBag.Enderecos = null;
                return View("Carrinho");
            }

            Pessoa pessoa = null;

            switch (session.AutenticacaoTipo)
            {
                case AutenticacaoTipoEnum.Facebook:
                    pessoa = Context.Pessoa.FirstOrDefault(x => x.FacebookId == session.Id.ToString());
                    break;
                case AutenticacaoTipoEnum.Google:
                    pessoa = Context.Pessoa.FirstOrDefault(x => x.GoogleId == session.Id.ToString());
                    break;
                case AutenticacaoTipoEnum.Sistema:
                    pessoa = Context.Pessoa.Find(ToInt32(session.Id));
                    break;
            }

            ViewBag.Enderecos = _db.Endereco.Where(x => x.Pessoa.Id == pessoa.Id).ToList();

            return View("Carrinho");
        }

        public async Task<int> Add(int id)
        {
            var produto = await _db.Produto.FindAsync(id);
            var produtoCarrinho = new ProdutoCarrinho
            {
                Produto = produto,
                Quantidade = 1,
                Imagem = _db.ProdutoImagem.FirstOrDefault(x => x.Produto.Id == produto.Id)?.Imagem.Nome
            };

            produtoCarrinho.Valor = (ToInt32(produtoCarrinho.Produto.Preco) * produtoCarrinho.Quantidade);
            produtoCarrinho.Peso = (ToInt32(produtoCarrinho.Produto.Peso) * produtoCarrinho.Quantidade);

            var aspSessionCarrinho = Session["Carrinho"] as SessionCarrinho;

            if (aspSessionCarrinho == null)
            {
                _sessionCarrinho = new SessionCarrinho();
                _sessionCarrinho.ProdutosCarrinho.Add(produtoCarrinho);

                Session.Add("Carrinho", _sessionCarrinho);

                return _sessionCarrinho.ProdutosCarrinho.Count;
            }

            if (aspSessionCarrinho.ProdutosCarrinho.Any(x => x.Produto.Id == id))
                return aspSessionCarrinho.ProdutosCarrinho.Count;

            aspSessionCarrinho.ProdutosCarrinho.Add(produtoCarrinho);

            Session["Carrinho"] = aspSessionCarrinho;

            return aspSessionCarrinho.ProdutosCarrinho.Count;
        }

        public JsonResult Remove(int id)
        {
            var sessionCarrinho = Session["Carrinho"] as SessionCarrinho;

            var newSessionCarrinho = new SessionCarrinho();

            if (sessionCarrinho != null)
                foreach (var produtoCarrinho in sessionCarrinho.ProdutosCarrinho.Where(produtoCarrinho => produtoCarrinho.Produto.Id != id))
                    newSessionCarrinho.ProdutosCarrinho.Add(produtoCarrinho);

            Session["Carrinho"] = newSessionCarrinho;

            return Json(new {RedirectUrl = Url.Action("Index", "Carrinho")}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CalculaFrete(string cep, string tipoFrete, string peso = "15")
        {
            try
            {
                if (Session["Carrinho"] == null || Session["Autenticacao"] == null)
                    return Json(new {RedirectUrl = Url.Action("Index", "Carrinho")}, JsonRequestBehavior.AllowGet);
                if (cep == null)
                    return Json(new {RedirectUrl = Url.Action("Index", "Carrinho")}, JsonRequestBehavior.AllowGet);
                var endereco = _db.Endereco.Find(int.Parse(cep)) as Endereco;

                var empresa = string.Empty;
                var senha = string.Empty;
                var codigoServico = tipoFrete == "pac" ? "41106" : "40010";
                var cepOrigem = "78075735";
                var cepDestino = endereco.Cep;
                var nCdFormato = 1;
                decimal nVlComprimento = 30;
                decimal nVlAltura = 10;
                decimal nVlLargura = 20;
                decimal nVlDiametro = 10;
                var sCdMaoPropria = "N";
                decimal nVlValorDeclarado = 0;
                var sCdAvisoRecebimento = "N";

                var webServiceCorreios = new CalcPrecoPrazoWS();
                var retornoCorreios = webServiceCorreios.CalcPrecoPrazo(empresa, senha, codigoServico, cepOrigem, cepDestino, peso, nCdFormato, nVlComprimento, nVlAltura, nVlLargura, nVlDiametro, sCdMaoPropria, nVlValorDeclarado, sCdAvisoRecebimento);
                var sessionCarrinho = Session["Carrinho"] as SessionCarrinho;

                if (retornoCorreios.Servicos.Length < 0)
                    return Json(new {RedirectUrl = Url.Action("Index", "Carrinho")}, JsonRequestBehavior.AllowGet);

                if (retornoCorreios.Servicos[0].Erro != "0")
                    return Json(new {RedirectUrl = Url.Action("Index", "Carrinho")}, JsonRequestBehavior.AllowGet);
                if (sessionCarrinho == null)
                    return Json(new {RedirectUrl = Url.Action("Index", "Carrinho")}, JsonRequestBehavior.AllowGet);

                sessionCarrinho.Endereco = _db.Endereco.Find(int.Parse(cep));
                sessionCarrinho.Frete = decimal.Parse(retornoCorreios.Servicos[0].Valor);
                sessionCarrinho.TotalGeral = sessionCarrinho.ProdutosCarrinho.Sum(x => x.Valor);
                sessionCarrinho.IsSedex = tipoFrete != "pac";

                Session["Carrinho"] = sessionCarrinho;

                return Json(new {RedirectUrl = Url.Action("Index", "Carrinho")}, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new {RedirectUrl = Url.Action("Index", "Carrinho")}, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Change(int id, int quantidade)
        {
            var sessionCarrinho = Session["Carrinho"] as SessionCarrinho;
            var newSessionCarrinho = new SessionCarrinho();

            foreach (var produtoCarrinho in sessionCarrinho?.ProdutosCarrinho)
            {
                if (produtoCarrinho.Produto.Id == id)
                {
                    produtoCarrinho.Quantidade = quantidade == 0 ? 1 : quantidade;
                    produtoCarrinho.Valor = ToInt32(produtoCarrinho.Produto.Preco) * produtoCarrinho.Quantidade;
                    produtoCarrinho.Peso = ToInt32(produtoCarrinho.Produto.Peso) * produtoCarrinho.Quantidade;
                }
                newSessionCarrinho.ProdutosCarrinho.Add(produtoCarrinho);
            }

            Session["Carrinho"] = newSessionCarrinho;

            return Json(new {RedirectUrl = Url.Action("Index", "Carrinho")}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FinalizarCompra(string pedidoId)
        {
            try
            {
                Response.AppendHeader("Access-Control-Allow-Origin", "https://sandbox.pagseguro.uol.com.br");

                var session = Session["Autenticacao"] as SessionAutenticacaoClient;
                var sessionCarrinho = Session["Carrinho"] as SessionCarrinho;

                if (session == null)
                    return RedirectToAction("Index", "Login", new { actionRedirect = "index", controllerRedirect = "Carrinho" });

                if (pedidoId.Length > 10)
                {
                    var cred = PagSeguroConfiguration.Credentials(true);
                    var transaction = TransactionSearchService.SearchByCode(cred, pedidoId);
                    var pedidoTransaction = _db.Pedido.Find(int.Parse(transaction.Reference));

                    pedidoTransaction.PagSeguroId = pedidoId;

                    _db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }

                var pedido = _db.Pedido.Find(int.Parse(pedidoId));
                var pedidoItens = _db.PedidoItem.Where(x => x.Pedido.Id == pedido.Id).ToList();

                var payment = new PaymentRequest { Currency = Currency.Brl };

                foreach (var carrinhoProduto in sessionCarrinho?.ProdutosCarrinho)
                {
                    payment.Items.Add(new Item(
                        carrinhoProduto.Produto.Id.ToString(),
                        carrinhoProduto.Produto.Descricao,
                        carrinhoProduto.Quantidade,
                        ToDecimal(carrinhoProduto.Produto.Preco)
                        ));
                }

                payment.Shipping = new Shipping { ShippingType = sessionCarrinho?.IsSedex == true ? ShippingType.Sedex : ShippingType.Pac };

                var cidade = _db.Cidade.FirstOrDefault(x => x.Id == sessionCarrinho.Endereco.IdCidade);
                var estado = _db.Estado.FirstOrDefault(x => x.Id == cidade.IdEstado);
                cidade.Estado = estado;

                sessionCarrinho.Endereco.Cidade = cidade;

                payment.Shipping.Address = new Address(
                    "BRA",
                    sessionCarrinho.Endereco.Cidade.Estado.Abreviacao,
                    sessionCarrinho.Endereco.Cidade.Nome,
                    sessionCarrinho.Endereco.Bairro,
                    sessionCarrinho.Endereco.Cep,
                    sessionCarrinho.Endereco.Logradouro,
                    sessionCarrinho.Endereco.Numero,
                    sessionCarrinho.Endereco.Complemento
                );

                payment.Reference = pedido.Id.ToString();

                var url = $"{Request.Url?.Scheme}://{Request.Url?.Authority}{Request.Url?.AbsolutePath}";

                payment.RedirectUri = new Uri(url);
                payment.MaxAge = 172800;
                payment.MaxUses = 15;
                payment.Shipping.Cost = sessionCarrinho.Frete;

                var credentials = PagSeguroConfiguration.Credentials(true);
                var paymentRedirectUri = payment.Register(credentials);

                Session.Remove("Carrinho");

                Response.Redirect(paymentRedirectUri.ToString());

                return View();
            }
            catch (PagSeguroServiceException)
            {
                throw;
            }
        }

        public ActionResult FinalizarPedido()
        {
            try
            {
                var sessaoCarrinho = Session["Carrinho"] as SessionCarrinho;
                var sessaoPessoa = Session["Autenticacao"] as SessionAutenticacaoClient;

                Pessoa pessoa = null;

                switch (sessaoPessoa?.AutenticacaoTipo)
                {
                    case AutenticacaoTipoEnum.Facebook:
                        pessoa = Context.Pessoa.FirstOrDefault(x => x.FacebookId == sessaoPessoa.Id.ToString());
                        break;
                    case AutenticacaoTipoEnum.Google:
                        pessoa = Context.Pessoa.FirstOrDefault(x => x.GoogleId == sessaoPessoa.Id.ToString());
                        break;
                    case AutenticacaoTipoEnum.Sistema:
                        pessoa = Context.Pessoa.Find(ToInt32(sessaoPessoa.Id));
                        break;
                }

                _db.Entry(pessoa).State = System.Data.Entity.EntityState.Modified;

                pessoa.EmailAutenticacao = pessoa.Email;
                pessoa.ConfirmaSenha = pessoa.Senha;

                var endereco = _db.Endereco.FirstOrDefault(x => x.Id == sessaoCarrinho.Endereco.Id);

                var pedido = new Pedido
                {
                    Endereco = endereco,
                    Pessoa = pessoa,
                    Data = DateTime.Now,
                    Status = PedidoStatusEnum.WaitingPayment,
                    Valor = sessaoCarrinho.TotalGeral,
                    Frete = sessaoCarrinho.Frete,
                    IsNovo = true
                };

                _db.Pedido.Add(pedido);
                _db.SaveChanges();

                var pedidoItems = new List<PedidoItem>();
                foreach (var produtoCarrinho in sessaoCarrinho.ProdutosCarrinho)
                {
                    var pedidoItem = new PedidoItem { Produto = _db.Produto.FirstOrDefault(x => x.Id == produtoCarrinho.Produto.Id) };
                    pedidoItem.Produto.ProdutoImagens = _db.ProdutoImagem.Where(x => x.IdProduto == produtoCarrinho.Produto.Id).ToList();
                    pedidoItem.Pedido = pedido;
                    pedidoItem.Quantidade = produtoCarrinho.Quantidade;

                    pedidoItems.Add(pedidoItem);
                    _db.PedidoItem.Add(pedidoItem);
                    _db.SaveChanges();
                }

                ViewBag.Menus = _db.Menu.ToList();
                ViewBag.Pedido = pedido;
                ViewBag.pedidoItens = pedidoItems;

                return View("PedidoConcluido");
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public void CheckStatusPagSeguro()
        {
            var session = Session["Autenticacao"] as SessionAutenticacaoClient;
            if (session == null)
                return;

            Pessoa pessoa = null;
            switch (session.AutenticacaoTipo)
            {
                    case AutenticacaoTipoEnum.Facebook:
                    pessoa = Context.Pessoa.FirstOrDefault(x => x.FacebookId == session.Id.ToString());
                    break;
                    case AutenticacaoTipoEnum.Google:
                    pessoa = Context.Pessoa.FirstOrDefault(x => x.GoogleId == session.Id.ToString());
                    break;
                    case AutenticacaoTipoEnum.Sistema:
                    pessoa = Context.Pessoa.Find(ToInt32(session.Id));
                    break;
            }

            var pedidos = _db.Pedido.Where(x => x.Pessoa.Id == pessoa.Id).ToList();
            if (pedidos.Count == 0)
                return;

            var cred = PagSeguroConfiguration.Credentials(true);
            foreach (var pedido in pedidos.Where(pedido => pedido.Status != PedidoStatusEnum.Delivered && pedido.Status != PedidoStatusEnum.Delivering).Where(pedido => pedido.PagSeguroId != null))
            {
                if (pedido.Status == PedidoStatusEnum.Cancelled)
                {
                    var pedidoItens = _db.PedidoItem.Where(x => x.Pedido.Id == pedido.Id).ToList();
                    foreach (var produtoEstoque in pedidoItens.Select(item => _db.ProdutoEstoque.FirstOrDefault(x => x.Produto.Id == item.Produto.Id)))
                    {
                        produtoEstoque.Quantidade++;
                        _db.Entry(produtoEstoque).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();
                    }
                }

                var transaction = TransactionSearchService.SearchByCode(cred, pedido.PagSeguroId);
                pedido.Status = (PedidoStatusEnum)transaction.TransactionStatus;
                _db.SaveChanges();

                if (pedido.Status != PedidoStatusEnum.Paid) continue;
                {
                    var hasVenda = _db.Venda.FirstOrDefault(x => x.IdPedido == pedido.Id);
                    if (hasVenda != null) continue;
                    {
                        var venda = new Venda
                        {
                            IdPedido = pedido.Id,
                            IsNova = true,
                            Data = DateTime.Now.Date
                        };

                        _db.Venda.Add(venda);

                        var pedidoItens = _db.PedidoItem.Where(x => x.IdPedido == pedido.Id).ToList();
                        foreach (var estoque in pedidoItens.Select(pedidoItem => _db.ProdutoEstoque.FirstOrDefault(x => x.IdProduto == pedidoItem.IdProduto)))
                        {
                            estoque.Quantidade = estoque.Quantidade - 1;
                            _db.Entry(estoque).State = System.Data.Entity.EntityState.Modified;
                            _db.SaveChanges();
                        }

                        _db.SaveChanges();
                    }
                }
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