using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration
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
