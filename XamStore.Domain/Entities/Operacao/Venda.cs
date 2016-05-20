using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamStore.Domain.Entities.Operacao
{
    public class Venda
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public Pedido Pedido { get; set; }
        public DateTime Data { get; set; }
        public string CodigoRastreioCorreios { get; set; }
        public bool IsNova { get; set; }
    }
}
