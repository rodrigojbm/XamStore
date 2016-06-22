using System;
using System.Collections.Generic;
using XamStore.Domain.Entities.Operacao;
using XamStore.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace XamStore.Domain.Entities.Cadastro
{
    
    public class Pessoa
    {
        public int Id { get; set; }
        public string FacebookId { get; set; }
        public string GoogleId { get; set; }
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Sobrenome é obrigatório")]
        public string Sobrenome { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        [Remote("EmailExists", "Login", ErrorMessage = "Email já cadastrado no sistema.")]
        [Required(ErrorMessage = "E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string EmailAutenticacao { get; set; }
        public SexoTipoEnum SexoTipo { get; set; }
        [Required(ErrorMessage = "Senha é obrigatório")]
        public string Senha { get; set; }
        [System.ComponentModel.DataAnnotations.Compare("Senha")]
        public string ConfirmaSenha { get; set; }
        public PessoaTipoEnum PessoaTipo { get; set; }

        public virtual Endereco Endereco { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
