using System.Data.Entity.ModelConfiguration;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Cadastro
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
