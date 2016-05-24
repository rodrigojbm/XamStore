using System.Data.Entity.ModelConfiguration;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Cadastro
{
    public class SlideMap : EntityTypeConfiguration<Slide>
    {
        public SlideMap()
        {
            ToTable("Slide");
            HasKey(s => s.Id);
            Property(s => s.Id).HasColumnName("Id");
            Property(s => s.Imagem).HasColumnName("Imagem").IsRequired();

            HasRequired(s => s.Produto)
                .WithMany()
                .HasForeignKey(s => s.IdProduto);
        }
    }
}
