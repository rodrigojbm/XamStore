using System.Data.Entity.ModelConfiguration;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Cadastro
{
    public class CidadeMap : EntityTypeConfiguration<Cidade>
    {
        public CidadeMap()
        {
            ToTable("Cidade");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Nome).HasColumnName("Nome");

            //Mapping the Relationship
            HasRequired(c => c.Estado)
                .WithMany()
                .HasForeignKey(c => c.IdEstado);
        }
    }
}
