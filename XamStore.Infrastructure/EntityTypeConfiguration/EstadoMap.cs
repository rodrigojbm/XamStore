using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration
{
    public class EstadoMap : EntityTypeConfiguration<Estado>
    {
        public EstadoMap()
        {
            ToTable("Estado");
            HasKey(e => e.Id);
            Property(e => e.Id).HasColumnName("Id");
            Property(e => e.Nome).HasColumnName("Descricao");
            Property(e => e.Abreviacao).HasColumnName("Abreviacao").IsRequired();
        }
    }
}
