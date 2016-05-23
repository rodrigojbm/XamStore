using System.Data.Entity.ModelConfiguration;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Cadastro
{
    public class ContatoMap : EntityTypeConfiguration<Contato>
    {
        public ContatoMap()
        {
            ToTable("Contato");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Nome).HasColumnName("Nome").IsRequired();
            Property(c => c.Email).HasColumnName("Email").IsRequired();
            Property(c => c.Telefone).HasColumnName("Telefone");
            Property(c => c.Mensagem).HasColumnName("Mensagem").IsRequired();
        }
    }
}
