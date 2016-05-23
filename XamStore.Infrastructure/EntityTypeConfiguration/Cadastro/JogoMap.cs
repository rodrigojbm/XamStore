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

            HasRequired(j => j.Genero)
                .WithMany(j => j.Jogos)
                .HasForeignKey(j => j.IdGenero);
        }
    }
}
