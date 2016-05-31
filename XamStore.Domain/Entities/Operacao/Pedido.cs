using System;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Domain.Enums;

namespace XamStore.Domain.Entities.Operacao
{
    public class Pedido
    {
        public int Id { get; set; }
        public PedidoStatusEnum Status { get; set; }
        public string PagSeguroId { get; set; }
        public decimal Valor { get; set; }
        public decimal Frete { get; set; }
        public DateTime Data { get; set; }
        public bool IsNovo { get; set; }

        public int IdPessoa { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public int IdEndereco { get; set; }
        public virtual Endereco Endereco { get; set; }
        
        //public virtual ICollection<Venda> Venda { get; set; }
        //public virtual ICollection<PedidoItem> PedidoItens { get; set; }
    }
}
