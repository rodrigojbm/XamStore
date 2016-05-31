namespace XamStore.Domain.Entities.Cadastro
{
    public class ProdutoEstoque
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public int IdProduto { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
