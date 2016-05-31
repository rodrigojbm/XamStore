namespace XamStore.Domain.Entities.Cadastro
{
    /// <summary>
    /// Identifica se o produto é um jogo, console, etc...
    /// </summary>
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        
        public virtual Produto Produto { get; set; }
    }
}
