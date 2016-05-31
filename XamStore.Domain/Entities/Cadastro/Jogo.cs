namespace XamStore.Domain.Entities.Cadastro
{
    public class Jogo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Multiplayer { get; set; }
        public int Jogadores { get; set; }
        public int Classificacao { get; set; }

        public int IdPlataforma { get; set; }
        public Plataforma Plataforma { get; set; }

        public int IdGenero { get; set; }
        public virtual Genero Genero { get; set; }

        public int IdConsole { get; set; }
        public virtual Console Console { get; set; }

        public int IdFabricante { get; set; }
        public virtual Fabricante Fabricante { get; set; }
    }
}
