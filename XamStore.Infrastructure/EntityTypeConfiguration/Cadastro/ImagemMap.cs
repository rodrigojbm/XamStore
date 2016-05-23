using System.Data.Entity.ModelConfiguration;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Cadastro
{
    public class ImagemMap : EntityTypeConfiguration<Imagem>
    {
        public ImagemMap()
        {
            ToTable("Imagem");
            HasKey(i => i.Id);
            Property(i => i.Id).HasColumnName("Id");
            Property(i => i.Nome).HasColumnName("Nome").IsRequired();
        }
    }
}
