using System.Data.Entity.ModelConfiguration;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Cadastro
{
    public class GeneroMap : EntityTypeConfiguration<Genero>
    {
        public GeneroMap()
        {
            ToTable("Genero");
            HasKey(g => g.Id);
            Property(g => g.Id).HasColumnName("Id");
            Property(g => g.Nome).HasColumnName("Nome").IsRequired();
        }
    }
}
