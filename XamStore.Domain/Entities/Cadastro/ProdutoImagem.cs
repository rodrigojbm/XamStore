using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamStore.Domain.Entities.Cadastro
{
    public class ProdutoImagem
    {
        public int Id { get; set; }

        public int IdProduto { get; set; }
        public Produto Produto { get; set; }

        public int IdImagem { get; set; }
        public virtual Imagem Imagem { get; set; }
    }
}
