using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration
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
