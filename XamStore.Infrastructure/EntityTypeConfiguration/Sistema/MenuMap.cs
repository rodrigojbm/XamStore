using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Sistema;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Sistema
{
    public class MenuMap : EntityTypeConfiguration<Menu>
    {
        public MenuMap()
        {
            ToTable("Menu");
            HasKey(m => m.Id);
            Property(m => m.Id).HasColumnName("Id");
            Property(m => m.Nome).HasColumnName("Nome").IsRequired();
            Property(m => m.Controller).HasColumnName("Controller").IsRequired();
            Property(m => m.Action).HasColumnName("Action").IsRequired();
            Property(m => m.Ativo).HasColumnName("Ativo").IsRequired();
        }
    }
}
