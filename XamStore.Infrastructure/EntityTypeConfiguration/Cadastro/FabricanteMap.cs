using System.Data.Entity.ModelConfiguration;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Cadastro
{
    public class FabricanteMap : EntityTypeConfiguration<Fabricante>
    {
        public FabricanteMap()
        {
            ToTable("Fabricante");
            HasKey(f => f.Id);
            Property(f => f.Id).HasColumnName("Id");
            Property(f => f.Nome).HasColumnName("Nome").IsRequired();
        }
    }
}
