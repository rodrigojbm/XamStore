using System.Data.Entity.ModelConfiguration;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Cadastro
{
    public class JogoMap : EntityTypeConfiguration<Jogo>
    {
        public JogoMap()
        {
            ToTable("Jogo");
            HasKey(j => j.Id);
            Property(j => j.Id).HasColumnName("Id");
            Property(j => j.Nome).HasColumnName("Nome");
            Property(j => j.Multiplayer).HasColumnName("Multiplayer").IsRequired();
            Property(j => j.Jogadores).HasColumnName("Jogadores").IsRequired();
            Property(j => j.Classificacao).HasColumnName("Classificacao").IsRequired();

            HasRequired(j => j.Genero)
                .WithMany()
                .HasForeignKey(j => j.IdGenero);

            HasRequired(j => j.Console)
                .WithMany()
                .HasForeignKey(j => j.IdConsole);

            HasRequired(j => j.Plataforma)
                .WithMany()
                .HasForeignKey(j => j.IdPlataforma);
        }
    }
}
