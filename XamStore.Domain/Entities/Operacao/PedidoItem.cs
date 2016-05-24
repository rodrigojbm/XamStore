using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Domain.Entities.Operacao
{
    public class PedidoItem
    {
        public int Id { get; set; }
        public double Quantidade { get; set; }

        public int IdPedido { get; set; }
        public virtual Pedido Pedido { get; set; }

        public int IdProduto { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
