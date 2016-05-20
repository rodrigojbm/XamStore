using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration
{
    public class UsuarioNivelMap : EntityTypeConfiguration<UsuarioNivel>
    {
        public UsuarioNivelMap()
        {
            ToTable("UsuarioNivel");
            HasKey(u => u.Id);
            Property(u => u.Id).HasColumnName("Id");
            Property(u => u.Nome).HasColumnName("Nome").IsRequired();
        }
    }
}
