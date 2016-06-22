using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Domain.Entities.Operacao;
using XamStore.Domain.Enums;
using XamStore.Infrastructure.Context;
using static System.Convert;
using SessionAutenticacaoClient = XamStore.Domain.Entities.Sistema.SessionAutenticacaoClient;

namespace XamStore.Application.Controllers
{
    public class PessoaController : BaseController
    {
        private readonly Context _db = new Context();

        public ActionResult Perfil(string mensagem = null, string mensagemPerfil = null)
        {
            if (mensagem != null)
                ViewBag.MensagemPerfil = mensagem;

            if (mensagemPerfil != null)
                ViewBag.MensagemPerfil = mensagemPerfil;

            if (!ChecarUsuarioAutenticado())
                return RedirectToAction("Index", "Login");

            var session = Session["Autenticacao"] as SessionAutenticacaoClient;

            Pessoa pessoa = null;

            switch (session?.AutenticacaoTipo)
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

            ViewBag.Pessoa = pessoa;
            ViewBag.Menus = _db.Menu;

            return View(pessoa);
        }

        public ActionResult Endereco()
        {
            if (!ChecarUsuarioAutenticado())
                return RedirectToAction("Index", "Login");

            var session = Session["Autenticacao"] as SessionAutenticacaoClient;

            Pessoa pessoa = null;
            ICollection<Endereco> enderecos = null;

            switch (session?.AutenticacaoTipo)
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

            enderecos = _db.Endereco.Where(x => x.Pessoa.Id == pessoa.Id).ToList();

            ViewBag.Menus = _db.Menu;
            ViewBag.Enderecos = enderecos;

            ViewBag.IdEstado = new SelectList(_db.Estado, "Id", "Nome");
            ViewBag.IdCidade = new SelectList(_db.Cidade, "Id", "Nome");

            //var selected = idCidade.Where(x => x.Value == "selectedValue").First();
            //selected.Selected = true;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarEndereco(Endereco endereco)
        {
            try
            {
                if (!ModelState.IsValid)
                    return RedirectToAction("Endereco");

                var session = Session["Autenticacao"] as SessionAutenticacaoClient;

                Pessoa pessoa = null;
                switch (session?.AutenticacaoTipo)
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

                if (pessoa != null)
                {
                    pessoa.ConfirmaSenha = pessoa.Senha;
                    pessoa.EmailAutenticacao = pessoa.Email;

                    endereco.IdPessoa = pessoa.Id;
                }

                _db.Endereco.Add(endereco);
                _db.SaveChanges();

                return RedirectToAction("Endereco");
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

        public async Task<ActionResult> ExcluirEndereco(int id)
        {
            try
            {
                var endereco = _db.Endereco.Find(id);

                _db.Endereco.Remove(endereco);
                _db.SaveChanges();

                return RedirectToAction("Endereco");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Pedido()
        {
            if (!ChecarUsuarioAutenticado())
                return RedirectToAction("Index", "Login");

            var session = Session["Autenticacao"] as SessionAutenticacaoClient;

            Pessoa pessoa = null;
            switch (session?.AutenticacaoTipo)
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

            var pedidos = _db.Pedido.Where(x => x.IdPessoa == pessoa.Id).ToList();
            var vendas = _db.Venda;

            ViewBag.Menus = _db.Menu;
            ViewBag.Pedidos = pedidos;
            ViewBag.Vendas = vendas;

            var pedidoItens = _db.PedidoItem.ToList();
            var newPedidoList = new List<PedidoItem>();

            foreach (var pedidoItem in pedidoItens)
            {
                pedidoItem.Produto.ProdutoImagens = _db.ProdutoImagem.Where(x => x.Produto.Id == pedidoItem.IdProduto).ToList();
                newPedidoList.Add(pedidoItem);
            }

            ViewBag.PedidoItens = newPedidoList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlterarPerfil([Bind(Include = "Nome, Sobrenome, Senha, SexoTipo, Email, Id, Cpf, Rg, DataNascimento")] Pessoa pessoa)
        {
            ModelState.Remove("EmailAutenticacao");
            ModelState.Remove("ConfirmaSenha");

            try
            {
                if (ModelState.IsValid)
                {
                    pessoa.EmailAutenticacao = pessoa.Email;
                    pessoa.ConfirmaSenha = pessoa.Senha;

                    var session = Session["Autenticacao"] as SessionAutenticacaoClient;

                    if (session?.AutenticacaoTipo == AutenticacaoTipoEnum.Facebook)
                        pessoa.FacebookId = session.Id;

                    if (session?.AutenticacaoTipo == AutenticacaoTipoEnum.Google)
                        pessoa.GoogleId = session.Id;

                    _db.Entry(pessoa).State = EntityState.Modified;
                    _db.SaveChanges();

                    var route = new RouteValueDictionary { { "mensagemPerfil", "Perfil alterado com sucesso!" } };

                    return RedirectToAction("perfil", route);
                }
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            return RedirectToAction("perfil");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RedefinirSenha([Bind(Include = "Nome, Sobrenome, Senha, Sexo, Email, Id, Cpf, Rg, DataNascimento, ConfirmaSenha")] Pessoa pessoa)
        {
            try
            {
                var usuario = _db.Pessoa.Find(pessoa.Id);
                usuario.Senha = pessoa.Senha;
                usuario.ConfirmaSenha = pessoa.Senha;
                usuario.EmailAutenticacao = usuario.Email;

                _db.Entry(usuario).State = EntityState.Modified;
                _db.SaveChanges();

                var route = new RouteValueDictionary { { "mensagem", "Senha alterada com sucesso!" } };

                return RedirectToAction("Perfil", route);
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
    }
}