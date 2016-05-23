using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Domain.Entities.Sistema
{
    public class SessionCarrinho
    {
        public ICollection<ProdutoCarrinho> ProdutosCarrinho = new List<ProdutoCarrinho>(); 
        public Endereco Endereco { get; set; }
        public decimal Frete { get; set; }
        public decimal TotalGeral { get; set; }
        public bool IsSedex { get; set; }
    }
}
