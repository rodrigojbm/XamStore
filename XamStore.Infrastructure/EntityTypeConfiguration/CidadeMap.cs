using System.Data.Entity.ModelConfiguration;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration
{
    public class CidadeMap : EntityTypeConfiguration<Cidade>
    {
        public CidadeMap()
        {
            ToTable("Cidade");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Descricao).HasColumnName("Descricao");

            //Mapping the Relationship
            HasRequired(c => c.Estado)
                .WithMany(c => c.Cidades)
                .HasForeignKey(c => c.IdEstado);
        }
    }
}
