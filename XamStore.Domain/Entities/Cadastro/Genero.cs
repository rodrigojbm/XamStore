namespace XamStore.Domain.Entities.Cadastro
{
    public class Genero
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual Jogo Jogo { get; set; }
    }
}
