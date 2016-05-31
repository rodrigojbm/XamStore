using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Domain.Entities.Sistema
{
    public class ProdutoCarrinho
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public decimal Peso { get; set; }
        public string Imagem { get; set; }
    }
}
