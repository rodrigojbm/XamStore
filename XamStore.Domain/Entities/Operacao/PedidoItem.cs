using System;
using System.Collections.Generic;
using System.Linq;
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
        public Pedido Pedido { get; set; }

        public int IdProduto { get; set; }
        public Produto Produto { get; set; }
    }
}
