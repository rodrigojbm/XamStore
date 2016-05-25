using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Cadastro
{
    public class PlataformaMap : EntityTypeConfiguration<Plataforma>
    {
        public PlataformaMap()
        {
            ToTable("Plataforma");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Nome).HasColumnName("Nome").IsRequired();
        }
    }
}
