using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration
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
                .WithMany(s => s.Slides)
                .HasForeignKey(s => s.IdProduto);
        }
    }
}
