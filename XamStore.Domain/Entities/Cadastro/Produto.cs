using System.Collections.Generic;

namespace XamStore.Domain.Entities.Cadastro
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public string PrecoString { get; set; }
        public string Garantia { get; set; }
        public decimal Peso { get; set; }
        public string PesoString { get; set; }
        public int Estoque { get; set; }

        public int IdCategoria { get; set; }
        public virtual Categoria Categoria { get; set; }

        public int IdJogo { get; set; }
        public virtual Jogo Jogo { get; set; }



        public virtual ICollection<ProdutoImagem> ProdutoImagens { get; set; }
        //public virtual ICollection<Slide> Slides { get; set; }
        //public virtual ICollection<ProdutoEstoque> ProdutoEstoques { get; set; }
        //public virtual ICollection<PedidoItem> PedidoItens { get; set; }
    }
}
