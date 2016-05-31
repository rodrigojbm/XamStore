using System;

namespace XamStore.Domain.Entities.Operacao
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string CodigoRastreioCorreios { get; set; }
        public bool IsNova { get; set; }

        public int IdPedido { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}
