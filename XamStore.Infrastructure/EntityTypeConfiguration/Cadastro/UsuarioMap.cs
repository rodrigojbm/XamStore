using System.Data.Entity.ModelConfiguration;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Cadastro
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
