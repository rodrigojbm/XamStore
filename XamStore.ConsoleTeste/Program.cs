using System.Data.Entity.Migrations;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Infrastructure.Context;

namespace XamStore.ConsoleTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new Context();

            context.Jogo.AddOrUpdate(x => x.Nome, new Jogo()
            {
                Nome = "Algum",
                Jogadores = 2,
                Classificacao = 16,
                IdPlataforma = 1,
                IdGenero = 1,
                IdConsole = 1,
                IdFabricante = 1
            });

            context.SaveChanges();
        }
    }
}
