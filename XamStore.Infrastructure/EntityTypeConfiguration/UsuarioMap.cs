using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("Usuario");
            HasKey(u => u.Id);
            Property(u => u.Id).HasColumnName("Id");
            Property(u => u.Nome).HasColumnName("Nome").IsRequired();
            Property(u => u.Login).HasColumnName("Login").IsRequired();
            Property(u => u.Senha).HasColumnName("Senha").IsRequired();

            HasRequired(u => u.UsuarioNivel)
                .WithMany(u => u.Usuarios)
                .HasForeignKey(u => u.IdUsuarioNivel);
        }
    }
}
