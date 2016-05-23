using System.Data.Entity.ModelConfiguration;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Cadastro
{
    public class NewsletterMap : EntityTypeConfiguration<Newsletter>
    {
        public NewsletterMap()
        {
            ToTable("Newsletter");
            HasKey(n => n.Id);
            Property(n => n.Id).HasColumnName("Id");
            Property(n => n.Data).HasColumnName("Data").IsRequired();
            Property(n => n.Email).HasColumnName("Email").IsRequired();
            Property(n => n.Status).HasColumnName("Status");
        }
    }
}
