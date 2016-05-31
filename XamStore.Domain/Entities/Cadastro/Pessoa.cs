using System;
using System.Collections.Generic;
using XamStore.Domain.Entities.Operacao;
using XamStore.Domain.Enums;

namespace XamStore.Domain.Entities.Cadastro
{
    
    public class Pessoa
    {
        public int Id { get; set; }
        public string FacebookId { get; set; }
        public string GoogleId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string EmailAutenticacao { get; set; }
        public SexoTipoEnum SexoTipo { get; set; }
        public string Senha { get; set; }
        public string ConfirmaSenha { get; set; }
        public PessoaTipoEnum PessoaTipo { get; set; }

        public virtual Endereco Endereco { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
